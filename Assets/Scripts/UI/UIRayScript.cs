using UnityEngine;
using System.Collections;

public class UIRayScript : MonoBehaviour 
{
	public GameObject AimObject;
	public GameObject StartUpUI;
	private StartUpScript UIScript;
	
	public float nextFire = 0.0f;
	public float fireRate = 0.5f;
	
	// UI
	private float StartButton = 0;
	private float OptionButton = 0;
	private float QuitButton = 0;
	private float GameOnButton = 0;
	
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
				StartButton +=1;
				if (StartButton >= 5)
				{
					Debug.Log("START ACTIVATED");
					UIScript.UIStart();
					StartButton = 0;
				}
				OptionButton = 0;
				QuitButton = 0;
			}
			
			if (hitInfo.collider.CompareTag("UIOptions"))
			{
				OptionButton +=1;
				if (OptionButton >= 5)
				{
					Debug.Log("OPTIONS ACTIVATED");
				}
				QuitButton = 0;
				QuitButton = 0;
				
			}
			
			if (hitInfo.collider.CompareTag("UIQuit"))
			{
				QuitButton +=1;
				if (QuitButton >= 5)
				{
					Debug.Log("QUIT ACTIVATED");
					UIScript.UIStart();
					QuitButton = 0;
					
				}
				OptionButton = 0;
				QuitButton = 0;
				
			}
			
			if (hitInfo.collider.CompareTag("UIGameOn"))
			{
				GameOnButton +=1;
				if (GameOnButton >= 5)
				{
					Debug.Log("GAMEON ACTIVATED");
					UIScript.UIStart();
				}
			}
			else
			{
				GameOnButton = 0;
			}
		}
	}
}