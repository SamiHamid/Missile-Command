using UnityEngine;
using System.Collections;

public class StartPanelScript : MonoBehaviour {
	
	// StartMenu
	private Animator anim;
	
	// HowTo Menu
	public GameObject HowToMiddleObject;
	public GameObject HowToLeftObject;
	public GameObject HowToRightObject;
	public GameObject HowToButton;
	
	// Starting Game
	public GameObject Grid;
	public GameObject EnvironmentUI;
	public GameObject Background;
	
	private gridMakerScript GridScript;
	private EnvironmentUIScript EnvScript;
	
	private bool GameCanStart = false;
	
	void Start () 
	{
		anim = GetComponent<Animator>();
		GridScript = Grid.GetComponent<gridMakerScript>();
		EnvScript = EnvironmentUI.GetComponent<EnvironmentUIScript>();
		
		
	}
	
	void Update()
	{
		if(Input.GetButtonDown("Jump"))
		{
			if(GameCanStart)
			{
				if(!gridMakerScript.InitializationStarted && !gridMakerScript.GameStarted)
				{
					GridScript.GameBegin();
					EnvScript.Activate();
				}
			}
			
			else
			{
				Fade ();
				Instantiate (HowToMiddleObject, gameObject.transform.position, Quaternion.identity);
				Instantiate (HowToLeftObject, gameObject.transform.position, Quaternion.Euler (0.0f, -50f, 0.0f));
				Instantiate (HowToRightObject, gameObject.transform.position, Quaternion.Euler (0.0f, 50f, 0.0f));
				Instantiate (HowToButton, gameObject.transform.position, Quaternion.identity);
				GameCanStart = true;
			}
		}
		
	}
	
	public void Fade()
	{
		anim.SetBool ("Fade",true);
	}
	
	public void HowToFade()
	{
		
	}
}
