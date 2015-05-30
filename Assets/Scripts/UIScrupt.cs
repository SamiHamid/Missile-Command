﻿using UnityEngine;
using System.Collections;

public class UIScrupt : MonoBehaviour 

{
	public GameObject Grid;
	private gridMakerScript GridScript;
	
	void Awake()
	{
		GridScript = Grid.GetComponent<gridMakerScript>();
	}
	
	void Start () 
	{
	}
	
	// Start Game
	void Update () 
	{                                       // deny the player from spamming Spacebar
		if (Input.GetButtonDown("Jump") && !gridMakerScript.InitializationStarted)
		{
			GridScript.GameBegin();
			Invoke("DropDestroyUI", 2);
		}
	}
	
	
	// Destroy UI
	void DropDestroyUI()
	{
		GetComponent<Rigidbody>().useGravity = true;
		Destroy(gameObject, 10);
	}
}
