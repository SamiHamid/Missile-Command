using UnityEngine;
using System.Collections;

public class UIMIssileDeath : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		Invoke("Death", 2);
	}

	
	void Death()
	{
		Destroy(gameObject);
	}
}
