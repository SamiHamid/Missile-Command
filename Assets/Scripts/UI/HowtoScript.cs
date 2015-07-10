using UnityEngine;
using System.Collections;

public class HowtoScript : MonoBehaviour {

	private Animator anim;
	
	//Change Text
	public TextMesh Text;
	public GameObject MissileUI;
	public bool BoolFlip = true;
	
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
	
	void Update()
	{
		if (MissileUI == null)
		{
			if (BoolFlip == true)
			{
			ChangeText();
			Debug.Log("IM BEING CALLED");
			BoolFlip = false;
			}
		}
		
	}
	
	public void ChangeText()
	{
		Text.text = ("You Are Ready!");
		Text.color = Color.green;
	}
}
