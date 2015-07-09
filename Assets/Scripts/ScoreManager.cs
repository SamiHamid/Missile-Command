using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour
{

    private int[] _scores;

    public int[] Scores
    {
        get { return _scores; }
    }

    void Start ()
	{
	    _scores = new int[5];

        // if the scores are not set
	    if (PlayerPrefs.GetInt("scoresSet") == 0)
	    {
	        PutDummyScores();
            Debug.Log("Scores set to Dummy Scores");
        }

        // read the scores data
	    _scores[0] = PlayerPrefs.GetInt("Score1");
        _scores[1] = PlayerPrefs.GetInt("Score2");
        _scores[2] = PlayerPrefs.GetInt("Score3");
        _scores[3] = PlayerPrefs.GetInt("Score4");
        _scores[4] = PlayerPrefs.GetInt("Score5");

    }

    private void PutDummyScores()
    {
        // its just 5 of them so no loop :>
        PlayerPrefs.SetInt("Score1", 3500);
        PlayerPrefs.SetInt("Score2", 3200);
        PlayerPrefs.SetInt("Score3", 2800);
        PlayerPrefs.SetInt("Score4", 2500);
        PlayerPrefs.SetInt("Score5", 2000);
        PlayerPrefs.SetInt("scoresSet", 1);
    }


    public void ResetScores()
    {   // call this when one wants to reset the scores, perhaps from a UI interaction
        PlayerPrefs.SetInt("Score1", 0);
        PlayerPrefs.SetInt("Score2", 0);
        PlayerPrefs.SetInt("Score3", 0);
        PlayerPrefs.SetInt("Score4", 0);
        PlayerPrefs.SetInt("Score5", 0);
        PlayerPrefs.SetInt("scoresSet" , 0);
        Debug.Log("Scores Reset!");
        
    }

    public bool IsHighScore(int score)
    {
        return score > _scores[4];
    }
}
