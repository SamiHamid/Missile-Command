using UnityEngine;
using System.Collections;

public class UIRotatorScript : MonoBehaviour {

	public float YrotationsPerMinute;
	public float XrotationsPerMinute;
	public float ZrotationsPerMinute;
	
	void Start () 
	{
	
	}
	
	
	void Update () 
	{
		float X = 5.0f * XrotationsPerMinute * Time.deltaTime;
		float Y = 5.0f * YrotationsPerMinute * Time.deltaTime;
		float Z = 5.0f * ZrotationsPerMinute * Time.deltaTime;
		transform.Rotate(X,Y,Z);
	}

	

}
