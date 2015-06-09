using UnityEngine;
using System.Collections;

public class HowToUIScript : MonoBehaviour {

	public GameObject EnvironmentUI; 
	public GameObject Grid;
	
	private EnvironmentUIScript EnvScript;
	private gridMakerScript GridScript;
	
	void Start () 
	{
		EnvScript = EnvironmentUI.GetComponent<EnvironmentUIScript>();
		GridScript = Grid.GetComponent<gridMakerScript>();
	}

	void Update () 
	{                                       // deny the player from spamming Spacebar
		if (Input.GetButtonDown("Jump") && !gridMakerScript.InitializationStarted)
		{
			GridScript.GameBegin();
			EnvScript.Activate();
			Disable ();
		}
	}	

	public void Activate()
	{
		gameObject.SetActive(true);
	}
	
	public void Disable()
	{
		gameObject.SetActive(false);
		
	}
}
