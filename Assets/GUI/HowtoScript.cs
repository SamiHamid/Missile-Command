using UnityEngine;
using System.Collections;

public class HowtoScript : MonoBehaviour {

	private Animator animator;
	
	void Awake () 
	{
		animator = GetComponent<Animator>();
		Debug.Log("Im Being Called");
	}
	

	public void HowToFade()
	{
		animator.SetBool ("Fading",true);
		Debug.Log("Fading Start Menu");
	}
}
