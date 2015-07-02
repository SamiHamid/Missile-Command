using UnityEngine;
using System.Collections;

public class UIRayScript : MonoBehaviour 
{
	public GameObject AimObject;
	public GameObject StartUpUI;
	private StartUpScript UIScript;
	
	public float nextFire = 0.0f;
	public float fireRate = 0.25f;
	
	// UI
	private float StartButton = 0;
	private float CreditButton = 0;
	private float CreditBackButton = 0;
	private float QuitButton = 0;
	private float GameOnButton = 0;
	
	//Colour Change Effect
	public GameObject StartText;
	public GameObject CreditsText;
	public GameObject CreditsBackText;
	public GameObject QuitText;
	public GameObject GameOnText;
	
	void Start()
	{
		UIScript = StartUpUI.GetComponent<StartUpScript>();
	}
	
	void Update()
	{
		if (Time.time > nextFire)
		{
			RayCasting();
			nextFire = Time.time + fireRate; 
		}
	}
	
	// UI RAYCASTING
	public void RayCasting()
	{
		Ray rayOrigin = new Ray (AimObject.transform.position, AimObject.transform.forward);
		RaycastHit hitInfo;
		Debug.Log("Firing");
		
		if (Physics.Raycast(rayOrigin, out hitInfo))
		{
			Debug.Log("Hit Something");
			Debug.DrawRay(transform.position, transform.forward, Color.green, 1);
			
			if (hitInfo.collider.CompareTag("UIStart"))
			{
				ColourChange ColourScript = StartText.GetComponent<ColourChange>();
				ColourScript.Activate();
				
				StartButton +=1;
				if (StartButton >= 10)
				{
					Debug.Log("START ACTIVATED");
					UIScript.UIStart();
					StartButton = 0;
				}
				CreditButton = 0;
				QuitButton = 0;
			}
			
			if (hitInfo.collider.CompareTag("UICredits"))
			{
				ColourChange ColourScript = CreditsText.GetComponent<ColourChange>();
				ColourScript.Activate();
				CreditButton +=1;
				if (CreditButton >= 10)
				{
					Debug.Log("CREDITS ACTIVATED");
					UIScript.UICredits();
					CreditButton =0;
				}
				StartButton = 0;
				QuitButton = 0;
				
			}
			
			if (hitInfo.collider.CompareTag("UICreditsBack"))
			{
				ColourChange ColourScript = CreditsBackText.GetComponent<ColourChange>();
				ColourScript.Activate();
				CreditBackButton+=1;
				if (CreditBackButton >=10)
				{
					Debug.Log("BACK ACTIVATED");
					UIScript.UICreditsBack();
					CreditBackButton =0;
				}
				StartButton = 0;
				QuitButton = 0;
				CreditButton = 0;
			}
			
			if (hitInfo.collider.CompareTag("UIQuit"))
			{
				ColourChange ColourScript = QuitText.GetComponent<ColourChange>();
				ColourScript.Activate();
				QuitButton +=1;
				if (QuitButton >= 10)
				{
					Debug.Log("QUIT ACTIVATED");
					UIScript.UIQuit();
					QuitButton = 0;
					
				}
				CreditButton = 0;
				QuitButton = 0;
				
			}
			
			if (hitInfo.collider.CompareTag("UIGameOn"))
			{
				ColourChange ColourScript = GameOnText.GetComponent<ColourChange>();
				ColourScript.Activate ();
				GameOnButton +=1;
				if (GameOnButton >= 5)
				{
					Debug.Log("GAMEON ACTIVATED");
					UIScript.UIHowTo();
				}
			}
			else
			{
				GameOnButton = 0;
			}
		}
	}
}