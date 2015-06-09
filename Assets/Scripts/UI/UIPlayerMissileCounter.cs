using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIPlayerMissileCounter : MonoBehaviour 
{
	private int UserMissile;
	
	void Awake()
	{
		UserMissile = 0;
	}
	
	void Update()
	{
		GetComponent<TextMesh>().text = "USER MISSILE COUNT: " + UserMissile;
	}
	
	public void AddUserMissile()
	{
		UserMissile++;
		Debug.Log("User Missile Added");
	}
	
	public void SubtractUserMissile()
	{
		UserMissile--;
		Debug.Log("User Missile Subtracted");
	}
}
