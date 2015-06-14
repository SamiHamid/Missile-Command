using UnityEngine;
using System.Collections;

public class UICityCounter : MonoBehaviour 
{

	private int _cityCount;
	
	void Awake()
	{
		_cityCount = 0;
	}
	
	void Update()
	{
		GetComponent<TextMesh>().text = "CITIES REMAINING: " + _cityCount;
	}
	
	public void AddCity()
	{
		_cityCount++;
		Debug.Log("City Added");
	}
	
	public void SubtractCity()
	{
		_cityCount--;
		Debug.Log("City Subtracted");
	}
}
