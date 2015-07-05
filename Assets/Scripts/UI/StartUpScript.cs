using UnityEngine;
using System.Collections;

public class StartUpScript : MonoBehaviour {

	// StartUp Menu
	private Animator anim;
	private int UIInitializeCounter = 0;
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
	public GameObject GamePlane;

	// Particle System
	public GameObject ParticleSystemObj;
	public GameObject PlayerParticleSystem;
	private PlayerParticles playerParticles;
	
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
		if (Input.GetMouseButtonDown(0))
		{
		    switch (UIInitializeCounter)
		    {
                case 0:
                    UIInitialize(); // Menu comes to face
                    break;

                case 1:
		            UIStart();  // "How to" comes to face
                    break;

                case 2:
		            UIHowTo();  // "how to" goes away and game starts
                    break;
		    }
		}
	}

	public void UIInitialize()
	{
		anim.SetTrigger ("FadeIn");
        UIInitializeCounter = 1;
    }

	public void UIStart()
	{
	   anim.SetTrigger ("FadeOut");
	   MiddleScript.FadeIn ();
	   LeftScript.FadeIn ();
	   RightScript.FadeIn ();
	   UIInitializeCounter = 2;
	   GetComponent<AudioSource>().Play();
	}
	
	public void UICredits()
	{
		CreditsScript.FadeIn();
		anim.SetTrigger ("FadeOut");
		GetComponent<AudioSource>().Play();
	}
	
	public void UICreditsBack()
	{
		anim.SetTrigger ("FadeIn");
		CreditsScript.FadeOut();
		GetComponent<AudioSource>().Play();
	}
	
	public void UIQuit()
	{
		Application.Quit();
		GetComponent<AudioSource>().Play();
	}
	
	public void UIHowTo()
	{
		MiddleScript.FadeOut();
		LeftScript.FadeOut();
		RightScript.FadeOut();
		Invoke ("GameOn", 8);
		Invoke ("DeactivateParticleRecticle", 3.5f);
	    UIInitializeCounter = 3;
		GetComponent<AudioSource>().Play();
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
		PlayerParticles playerParticles = PlayerParticleSystem.GetComponent<PlayerParticles>();
		playerParticles.ToggleActive();
		ParticleSystemObj.SetActive (false);
		Recticle.SetActive (false);
		GamePlane.SetActive (true);
		StartCoroutine ( VolumeFadeUp());
		playerParticles.ToggleActive();
	}
	
	IEnumerator VolumeFadeUp()
	{
		for ( int i = 0; i <= 5; i ++)
		{
			GamePlane.GetComponent<AudioSource>().volume = 0.1f * i; 
			yield return new WaitForSeconds(0.5f);
		}
	}
}
