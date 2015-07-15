using UnityEngine;
using System.Collections;

public class AlphaUI : MonoBehaviour 
{
	private int Counter;
	public GameObject HighScore;
	
	[SerializeField] private int _lookThreshold;
	
	private ScoreManager _scoreManager;
	
	void Start () 
	{
		_scoreManager = GameObject.Find("Player").GetComponent<ScoreManager>();
		
	}
	
	void ResetOthers()
	{
		foreach(GameObject alph in GameObject.FindGameObjectsWithTag("Alphabet"))
		{
			if(alph.name != this.name)
			{
				alph.GetComponent<AlphaUI>().Reset();
			}
		}
	}
	
	public void IncrementCounter()
	{
		// increment the counter and reset others' counter
		Counter +=1;
		ResetOthers();
	
		// if the threshold is met, type a letter
		if (Counter >= _lookThreshold)
		{
			//Debug.Log("TYPE " + GetComponent<TextMesh>().text);
			
			HighScore.GetComponent<AudioSource>().Play();
					
			// Type a letter
			if(GetComponent<TextMesh>().text.Length == 1)
			{
				_scoreManager.TypeLetter(GetComponent<TextMesh>().text);
			}
			else
			{
				// delete the last typed character
				if(GetComponent<TextMesh>().text == "DEL")
				{
					_scoreManager.DeleteLetter();
				}
				
				// update the score database
				else
				{
					_scoreManager.Submit();
				}
				
			}
			
			Reset ();
		}
	}
	
	public void Reset()
	{
		Counter = 0;
	}
}
