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
		
		// how to use layer masks: http://answers.unity3d.com/questions/8715/how-do-i-use-layermasks.html
		int layerMask = 1 << 5;	// UI is the 5th layer
		if (Physics.Raycast(rayOrigin, out hitInfo, 100, layerMask))
		{
			Debug.Log("Hit Something");
			Debug.DrawRay(transform.position, transform.forward, Color.green, 1);
			
			ColourChange ColourScript = hitInfo.collider.gameObject.GetComponentInChildren<ColourChange>();
			ColourScript.Activate();
			
			if (hitInfo.collider.CompareTag("UIStart"))
			{
				StartButton +=1;
				if (StartButton >= 5)
				{
					Debug.Log("START ACTIVATED");
					UIScript.UIStart();
					StartButton = 0;
				}
			}
			
			if (hitInfo.collider.CompareTag("UICredits"))
			{
				CreditButton +=1;
				if (CreditButton >= 5)
				{
					Debug.Log("CREDITS ACTIVATED");
					UIScript.UICredits();
					CreditButton =0;
				}
			}
			
			if (hitInfo.collider.CompareTag("UICreditsBack"))
			{
				CreditBackButton+=1;
				if (CreditBackButton >=5)
				{
					Debug.Log("BACK ACTIVATED");
					UIScript.UICreditsBack();
					CreditBackButton =0;
				}
			}
			
			if (hitInfo.collider.CompareTag("UIQuit"))
			{
				QuitButton +=1;
				if (QuitButton >= 5)
				{
					Debug.Log("QUIT ACTIVATED");
					UIScript.UIQuit();
					QuitButton = 0;
				}
			}
			
			if (hitInfo.collider.CompareTag("UIGameOn"))
			{
				GameOnButton +=1;
				if (GameOnButton >= 5)
				{
					Debug.Log("GAMEON ACTIVATED");
					UIScript.UIHowTo();
				}
			}
			
			ResetOthers(hitInfo.collider.tag);

		}
	}
	
	void ResetOthers(string exclude)
	{
		switch (exclude) 
		{
		case "UIStart":
			CreditButton = 0;
			CreditBackButton = 0;
			QuitButton = 0;
			GameOnButton = 0;
			break;
		case "UICredits":
			StartButton = 0;
			CreditBackButton = 0;
			QuitButton = 0;
			GameOnButton = 0;
			break;
		case "UICreditsBack":
			StartButton = 0;
			CreditButton = 0;
			QuitButton = 0;
			GameOnButton = 0;
			break;
		case "UIQuit":
			StartButton = 0;
			CreditButton = 0;
			CreditBackButton = 0;
			GameOnButton = 0;
			break;
		case "UIGameOn":
			StartButton = 0;
			CreditButton = 0;
			CreditBackButton = 0;
			QuitButton = 0;
			break;
		}
	}
}


















