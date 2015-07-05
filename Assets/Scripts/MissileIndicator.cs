using UnityEngine;
using System.Collections;

public class MissileIndicator : MonoBehaviour 
{

	[SerializeField] private Material blue, red;

	[SerializeField] private GameObject Bar;

	void Start()
	{
	}

	void OnTriggerEnter(Collider other)
	{
		//Debug.Log("Trigger Enter!");
		if(other.tag == "Enemy Missile")
			;//Bar.GetComponent<Renderer>().material.shader.
	}
	
	void OnTriggerExit(Collider other)
	{
		//Debug.Log("Trigger Exit!");
		if(other.tag == "Enemy Missile")
			;//Bar.GetComponent<Renderer>().material.color = blue;
			
	}
}
