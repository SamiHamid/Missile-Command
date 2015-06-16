using UnityEngine;
using System.Collections;

public class StartUpScript : MonoBehaviour {

	// StartUp Menu
	private Animator anim;
	private int StartUpCount = 0;
	
	// How To Menu 
	public GameObject HowToMiddleObj;
	public GameObject HowToLeftObj;
	public GameObject HowToRightObj;
	private HowtoScript MiddleScript;
	private HowToRight RightScript;
	private HowToLeft LeftScript;
	
	// Start Game
	public GameObject Grid;
	public GameObject EnvironmentUI;
	private gridMakerScript GridScript;
	private EnvironmentUIScript EnvScript;
	
	void Start () 
	{
		anim = GetComponent<Animator>();
		MiddleScript = HowToMiddleObj.GetComponent<HowtoScript>();
		LeftScript = HowToLeftObj.GetComponent<HowToLeft>();
		RightScript = HowToRightObj.GetComponent<HowToRight>();
		GridScript = Grid.GetComponent<gridMakerScript>();
		EnvScript = EnvironmentUI.GetComponent<EnvironmentUIScript>();
	}
	
	void Update () 
	{
		if (Input.GetMouseButtonDown(0))
		{	
			// Start In
			if (StartUpCount == 0)
			{
				anim.SetTrigger ("FadeIn");
				StartUpCount = 1;
			}
			// Start Fade & HowTo In
			else if (StartUpCount == 1)
			{
				anim.SetTrigger ("FadeOut");
				StartUpCount  = 2;
				MiddleScript.FadeIn();
				LeftScript.FadeIn();
				RightScript.FadeIn();
				StartUpCount = 2;
			}
			// HowTo Out
			else if (StartUpCount == 2)
			{
				MiddleScript.FadeOut();
				LeftScript.FadeOut();
				RightScript.FadeOut();
				Invoke ("GameOn", 5);
			}
		}
		//SKIP STARTUP MENU
		if (Input.GetButtonDown("Jump"))
		{
			GameOn();
			HowToMiddleObj.SetActive(false);
			HowToLeftObj.SetActive(false);
			HowToRightObj.SetActive(false);	
		}
		
		if (Input.GetButton("Cancel"))
		{
			Application.Quit();
		}
	}
	
	// Game Begins
	void GameOn()
	{
		if (!gridMakerScript.InitializationStarted && !GameManager.GameStarted)
		{
		GridScript.GameBegin();
		EnvScript.Activate();
		}
	}
}
