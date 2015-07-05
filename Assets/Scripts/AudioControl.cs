using UnityEngine;
using System.Collections;

public class AudioControl : MonoBehaviour {

	
	void Start () 
	{
		Invoke ("StartUpAudio", 5);
	}
	
	
	void Update () 
	{
	
	}
	
	void StartUpAudio()
	{
		GetComponent<AudioSource>().Play();
	}
}
