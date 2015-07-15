using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour
{
	private List<HighScore> _scores;

	private string _highScoreName;
	private int _highScoreValue;
	private Transform _highScoreRow;
	
	private bool isSaved;

    void Start ()
	{
		//ResetScores();
		_highScoreName = "";

		// allocate the high score list
		_scores = new List<HighScore> ();

        // if the scores are not set, set them in PlayerPrefs
	    if (PlayerPrefs.GetInt("scoresSet") == 0)
	    {
	        PutDummyScores();
            Debug.Log("Scores set to Dummy Scores");
        }

		// read scores into the list from PlayerPrefs
		ReadScores ();
    }

	private void ReadScores()
	{
		for(int i=0; i<5; i++)
		{
			int score = PlayerPrefs.GetInt("Score" + (i+1));
			string name = PlayerPrefs.GetString("Name" + (i+1));

			_scores.Add(new HighScore(score, name));
		}
	}

    private void PutDummyScores()
    {
		PlayerPrefs.SetInt("Score1", 3000);
		PlayerPrefs.SetInt("Score2", 2750);
		PlayerPrefs.SetInt("Score3", 2500);
		PlayerPrefs.SetInt("Score4", 2250);
		PlayerPrefs.SetInt("Score5", 2000);
		
		PlayerPrefs.SetString("Name1", "VOL");
		PlayerPrefs.SetString("Name2", "SAM");
		PlayerPrefs.SetString("Name3", "ADW");
		PlayerPrefs.SetString("Name4", "KBY");
		PlayerPrefs.SetString("Name5", "ZAD");


		PlayerPrefs.SetInt("scoresSet", 1);
    }


    public void ResetScores()
    {   
		// call this when one wants to reset the scores, perhaps from a UI interaction
        Debug.Log("Scores Reset!");
		PutDummyScores (); 
    }

    public bool IsHighScore(int score)
    {
		Debug.Log ("Comparing score " + score + " to " + _scores[4].Score + "\tResult: " + (score > _scores[4].Score));
		PrintScores ();
        return score > _scores[4].Score;

    }

	public void UpdateScoresUI()
	{
		GameObject _scoresUI = GameObject.FindGameObjectWithTag ("UIHighScores");

		// Update Text Fields of ScoresUI
		int index = 0;

		foreach(Transform entry in _scoresUI.transform.FindChild("ScorePosition"))
		{
			entry.FindChild("ScoreText").GetComponent<TextMesh>().text = _scores[index].Score.ToString();
			entry.FindChild("Name").GetComponent<TextMesh>().text = _scores[index].Name;

			index++;
		}
	}

	public void HighLightNewScore(int newScore)
	{
		_highScoreValue = newScore;
	
		// find the index of the new score
		int index = 4;
		for (int i=0; i<5; i++)
		{
			if(_scores[i].Score < newScore)
			{
				index = i;
				break;
			}
		}

		// update the list				// insert the new score
		_scores.Insert (index, new HighScore(newScore, "XXX"));
		_scores.RemoveAt (5);	// remove the old score

		PrintScores();
		UpdateScoresUI ();

		// highlight the new score in the updated highscore UI
		GameObject _scoresUI = GameObject.FindGameObjectWithTag ("UIHighScores");

		// iterate through the score entires in the highscore table
		foreach(Transform entry in _scoresUI.transform.FindChild("ScorePosition"))
		{
			// if this entry is our new score
			if(entry.FindChild("ScoreText").GetComponent<TextMesh>().text == newScore.ToString())
			{
				// highlight it white
				foreach(TextMesh text in entry.GetComponentsInChildren<TextMesh>())
				{
					text.color = Color.white;
				}

				// break the loop after the score is found
				_highScoreRow = entry;
				break;
			}
		}

	}

	void PrintScores()
	{
		string scoresStr = "";
		for (int i=0; i<5; i++)
		{
			scoresStr += _scores[i].Score + "\n";
		}

		Debug.Log (scoresStr);
	}

	public void TypeLetter(string letter)
	{
	
		if(_highScoreName.Length < 3)
			_highScoreName += letter;
			
		Debug.Log ("Type letter " + letter + "\t" + _highScoreName);
		RefreshLetterOnUI();
	}
	
	public void DeleteLetter()
	{
		// DELETE DOESNT WORK???
		if(!isSaved)
		{
			if(_highScoreName.Length != 0)
			{
				//Debug.Log ("WAT?");
				//_highScoreName.Remove(_highScoreName.Length-1);
				_highScoreName = "";
			}	
		
			Debug.Log ("Delete Letter" + "\t" + _highScoreName);
			
			RefreshLetterOnUI();
		}
	}

	public void Submit()
	{
		if(!isSaved){
			Debug.Log ("Submit Score" + "\t" + _highScoreName + " " + _highScoreValue);
			
			// update the name of the new highscore
			for(int i=0; i<5; i++)
			{
				if(_scores[i].Score == _highScoreValue)
				{
					_scores[i].Name = _highScoreName;
			 	} 
			}
			
			// update the values in the database
			for(int i=0; i<5; i++)
			{
				PlayerPrefs.SetInt("Score" + (i+1), _scores[i].Score);
				PlayerPrefs.SetString("Name" + (i+1), _scores[i].Name);	
			}
			
			
			isSaved = true;
			
			// TODO: make main menu go green
			 
		}
	}
	
	void RefreshLetterOnUI()
	{
		_highScoreRow.FindChild("Name").GetComponent<TextMesh>().text = _highScoreName;
	}

}

public class HighScore
{
	int _score;
	string _name;

	public int Score { 
		get { return _score; } 
		set { _score = value; }
	}
	public string Name { 
		get { return _name; } 
		set { _name = value; } 
	}

	public HighScore(int sc, string nm) { _score = sc; _name = nm; }
}
