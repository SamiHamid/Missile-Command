using UnityEngine;
using System.Collections;

public class StartUpScript : MonoBehaviour {

	// StartUp Menu
	private Animator anim;
	public GameObject GUI;
	private bool DisableStart;
	
	// How To Menu 
	public GameObject HowToMiddleObj;
	public GameObject HowToLeftObj;
	public GameObject HowToRightObj;
	private HowtoScript MiddleScript;
	private HowToRight RightScript;
	private HowToLeft LeftScript;
	private bool DemoFire;
	
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
	
	// Shoot Missiles
	public GameObject Player;
	private PlayerScript PlayerScriptHere;
	
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
		PlayerScriptHere = Player.GetComponent<PlayerScript>();
	}
	
	void Update () 
	{
		//SKIP STARTUP MENU
		//if (Input.GetButtonDown("Jump"))
		//{
		//	GameOn();

		//}
		
		// Game GUI Initialization
		if (Input.GetMouseButtonDown(0) && DisableStart == false)
		{
			if (DemoFire == false)
			{
				UIInitialize();
				DisableStart = true;
			}
		}
		
		if (Input.GetMouseButtonDown(0) && DemoFire == true)
		{
			PlayerScriptHere.ShootMissileUI();
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
	    Invoke ("DemoFireTimer", 2);
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
		DemoFire = false;
		GetComponent<AudioSource>().Play();
	}
	
	
	// Game Begins
	void GameOn()
	{
		if (!gridMakerScript.InitializationStarted && !GameManager.GameStarted)
        {
            GamePlane.SetActive(true);
            GridScript.GameBegin();
		    EnvScript.Activate();
			GUI.SetActive(false);
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
	
	void DemoFireTimer()
	{
		DemoFire = true;
	}
}
