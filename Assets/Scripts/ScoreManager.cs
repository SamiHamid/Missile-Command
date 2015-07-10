using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour
{
	private List<HighScore> _scores;


    void Start ()
	{
		Time.timeScale = 8;	// TODO: REMOVE THIS SHIT \\ also hide player score from editor later

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
		int baseScore = 3000;

		for(int i=0; i<5; i++)
		{
			int offset = 250*i;
			PlayerPrefs.SetInt("Score" + (i+1), baseScore - offset);  
			PlayerPrefs.SetString("Name" + (i+1), "DUM");
		}

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
		_scores.Insert (index, new HighScore(newScore, "..."));
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

				// break the loop
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
