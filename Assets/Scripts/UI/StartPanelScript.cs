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
	private HowtoScript HowScript;
	
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
                if (!gridMakerScript.InitializationStarted && !GameManager.GameStarted)
				{
				    //Destroy(GameObject.Find("UI Elements"));
					Invoke("GameStarts", 5);
					FadeToGame();
				}
			}
			
			else
			{
				FadeStart ();
                GameObject parent = new GameObject();
			    parent.name = "UI Elements";

				GameObject instance = Instantiate (HowToMiddleObject, gameObject.transform.position, Quaternion.identity) as GameObject;
			    instance.transform.parent = parent.transform;
                instance = Instantiate(HowToLeftObject, gameObject.transform.position, Quaternion.Euler(0.0f, -50f, 0.0f)) as GameObject;
                instance.transform.parent = parent.transform;
                instance = Instantiate(HowToRightObject, gameObject.transform.position, Quaternion.Euler(0.0f, 50f, 0.0f)) as GameObject;
                instance.transform.parent = parent.transform;
                instance = Instantiate(HowToButton, gameObject.transform.position, Quaternion.identity) as GameObject;
                instance.transform.parent = parent.transform;
				GameCanStart = true;
			}
		}
		
	}
	
	// Fade StartMenu
	public void FadeStart()
	{
		anim.SetBool ("Fade",true);
	}
	
	public void FadeToGame()
	{
		HowScript = HowToMiddleObject.GetComponent<HowtoScript>();
		HowScript.HowToFade();
	}
	
	// Starts Game
	void GameStarts()
	{
		GridScript.GameBegin();
		EnvScript.Activate();
	}
}
