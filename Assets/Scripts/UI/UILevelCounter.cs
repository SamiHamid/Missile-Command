using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UILevelCounter : MonoBehaviour 
{
	private int Level;
	
	void Awake()
	{
		Level = 0;
	}
	
	void Update()
	{
		GetComponent<TextMesh>().text = "LEVEL: " + Level;
	}
	
	public void AddLevel()
	{
		Level++;
		Debug.Log("Level Added");
	}
}
