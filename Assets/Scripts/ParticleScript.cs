using UnityEngine;
using System.Collections;

public class ParticleScript : MonoBehaviour {

	
	void Start () 
	{
		ParticleSystem ParticleS = gameObject.GetComponentInChildren<ParticleSystem>();
		ParticleS.enableEmission = false;
	}
	
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			ToggleActive();
		}
	}
	
	public void ToggleActive()
	{
		ParticleSystem ParticleS = gameObject.GetComponentInChildren<ParticleSystem>();
		
		if (ParticleS.enableEmission == false)
		{
		ParticleS.enableEmission = true;
		}
		else
		{
			ParticleS.enableEmission = false;
		}
		
	}
	
}
