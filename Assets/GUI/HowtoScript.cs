using UnityEngine;
using System.Collections;

public class HowtoScript : MonoBehaviour {

	private Animator anim;
	
	void Start () 
	{
		anim = GetComponent<Animator>();
	}
	

	public void HowToFade()
	{
		anim.SetBool ("Fade",true);
		Debug.Log("Fading Start Menu");
	}
}
