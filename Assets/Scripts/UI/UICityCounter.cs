using UnityEngine;
using System.Collections;

public class UICityCounter : MonoBehaviour 
{

	private int CityCount;
	
	void Awake()
	{
		CityCount = 0;
	}
	
	void Update()
	{
		GetComponent<TextMesh>().text = "CITIES REMAINING: " + CityCount;
	}
	
	public void AddCity()
	{
		CityCount++;
		Debug.Log("City Added");
	}
	
	public void SubtractCity()
	{
		CityCount--;
		Debug.Log("City Subtracted");
	}
}
