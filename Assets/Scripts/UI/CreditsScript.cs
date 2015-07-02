using UnityEngine;
using System.Collections;

public class CreditsScript : MonoBehaviour 
{
	private Animator anim;
	
	void Awake () 
	{
		
		anim = GetComponent<Animator>();
	}
	
	public void FadeIn()
	{
		anim.SetTrigger ("FadeIn");
	}
	public void FadeOut()
	{
		anim.SetTrigger ("FadeOut");
	}
}

