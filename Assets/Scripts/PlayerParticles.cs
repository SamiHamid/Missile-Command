using UnityEngine;
using System.Collections;

public class PlayerParticles : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		ParticleSystem ParticleS = gameObject.GetComponent<ParticleSystem>();
		ParticleS.enableEmission = true;
	}
	
	
	void Update () {
	
	}
	
	public void ToggleActive()
	{
		ParticleSystem ParticleS = gameObject.GetComponent<ParticleSystem>();
		ParticleS.enableEmission = false;
		Debug.Log("Called");
	}
}
