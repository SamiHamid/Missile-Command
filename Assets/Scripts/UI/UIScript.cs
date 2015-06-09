using UnityEngine;
using System.Collections;

public class UIScript : MonoBehaviour 

{
	public GameObject StartUI;
	public GameObject HowToUI;
	
	private StartPanelScript StartScript;
	private HowToUIScript HowToScript;
	
	void Awake()
	{
		StartScript = StartUI.GetComponent<StartPanelScript>();
		HowToScript = HowToUI.GetComponent<HowToUIScript>();
		
		
		GetComponent<AudioSource>().Play();
	}
	
	void Start () 
	{
	
	}
	
	// Start Game
	void Update () 
	{                                       // deny the player from spamming Spacebar
		if (Input.GetButtonDown("Jump") && !gridMakerScript.InitializationStarted)
		{
			StartScript.Disable();
			HowToScript.Activate();
		}
	}
	
}
