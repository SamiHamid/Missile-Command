using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIScoreCounter : MonoBehaviour 
{
	private int Score;
	
	void Awake()
	{
		Score = 0;
	}
	
	void Update()
	{
		GetComponent<TextMesh>().text = "SCORE: " + Score;
	}
	
	public void AddScore()
	{
		Score++;
		Debug.Log("Score Added");
	}
	
	public void SubtractScore()
	{
		Score--;
		Debug.Log("Score Subtracted");
	}
}
