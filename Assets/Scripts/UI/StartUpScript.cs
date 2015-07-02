using UnityEngine;
using System.Collections;

public class StartUpScript : MonoBehaviour {

	// StartUp Menu
	private Animator anim;
	private float UIInitializeCounter = 0;
	public GameObject GUI;
	
	// How To Menu 
	public GameObject HowToMiddleObj;
	public GameObject HowToLeftObj;
	public GameObject HowToRightObj;
	private HowtoScript MiddleScript;
	private HowToRight RightScript;
	private HowToLeft LeftScript;
	
	// Credits
	public GameObject Credits;
	private CreditsScript CreditsScript;
	
	// Start Game
	public GameObject Grid;
	public GameObject EnvironmentUI;
	private gridMakerScript GridScript;
	private EnvironmentUIScript EnvScript;

	// Particle System
	public GameObject ParticleSystemObj;
	
	// Recticle
	public GameObject Recticle;
	
	void Start () 
	{
		anim = GetComponent<Animator>();
		MiddleScript = HowToMiddleObj.GetComponent<HowtoScript>();
		LeftScript = HowToLeftObj.GetComponent<HowToLeft>();
		RightScript = HowToRightObj.GetComponent<HowToRight>();
		GridScript = Grid.GetComponent<gridMakerScript>();
		EnvScript = EnvironmentUI.GetComponent<EnvironmentUIScript>();
		CreditsScript = Credits.GetComponent<CreditsScript>();
	}
	
	void Update () 
	{
		//SKIP STARTUP MENU
		if (Input.GetButtonDown("Jump"))
		{
			GameOn();
			GUI.SetActive(false);
		}
		
		// Game GUI Initialization
		if (Input.GetMouseButtonDown(0) && UIInitializeCounter == 0)
		{
			UIInitialize();
			UIInitializeCounter = 1;
		}
	}

	public void UIInitialize()
	{
		anim.SetTrigger ("FadeIn");
	}

	public void UIStart()
	{
		anim.SetTrigger ("FadeOut");
		MiddleScript.FadeIn ();
		LeftScript.FadeIn ();
		RightScript.FadeIn ();
	}
	
	public void UICredits()
	{
		CreditsScript.FadeIn();
		anim.SetTrigger ("FadeOut");
	}
	
	public void UICreditsBack()
	{
		anim.SetTrigger ("FadeIn");
		CreditsScript.FadeOut();
	}
	
	public void UIQuit()
	{
		Application.Quit();
	}
	
	public void UIHowTo()
	{
		MiddleScript.FadeOut();
		LeftScript.FadeOut();
		RightScript.FadeOut();
		Invoke ("GameOn", 5);
		Invoke ("DeactivateParticleRecticle", 2);
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

	// Deactivate ParticleSystem
	void DeactivateParticleRecticle()
	{
		ParticleSystemObj.SetActive (false);
		Recticle.SetActive (false);
	}
}
