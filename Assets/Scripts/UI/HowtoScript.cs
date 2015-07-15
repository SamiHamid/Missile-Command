using UnityEngine;
using System.Collections;

public class HowtoScript : MonoBehaviour {

	private Animator anim;
	
	//Change Text

	public TextMesh FirstText;
	public TextMesh SecondText;
	public TextMesh ThirdText;
	public GameObject MissileUI;
	public bool BoolFlip = true;
	private bool CanTextChange = false;
	
	void Awake () 
	{
		anim = GetComponent<Animator>();
	}
	
	public void FadeIn()
	{
		anim.SetTrigger ("FadeIn");
		CanTextChange = true;
		Debug.Log("IM ACTIVE");
	}
	
	public void FadeOut()
	{
		anim.SetTrigger ("FadeOut");
		CanTextChange = false;
	}
	
	void Update()
	{
		if (MissileUI == null)
		{
			if (BoolFlip == true)
			{
			ChangeThirdText();
			Debug.Log("IM BEING CALLED");
			BoolFlip = false;
			}
		}
		
		if (Input.GetMouseButtonDown(0))
		{
			if (CanTextChange == true)
			{
			ChangeFirstTText();
			}
		}
		
		if (Input.GetMouseButtonDown(1))
		{
			if (CanTextChange == true)
			{
			ChangeSecondText();
			}
		}
		
	}
	
	public void ChangeFirstTText()
	{
		FirstText.text = ("LEFT MOUSE BUTTON = FIRE - OK");
		FirstText.color = Color.green;
	}
	
	public void ChangeSecondText()
	{
		SecondText.text = ("RIGHT MOUSE BUTTON = DETONATE - OK");
		SecondText.color = Color.green;
	}
	
	public void ChangeThirdText()
	{
		ThirdText.text = ("USER IS READY TO PROCEED TO GAMES");
		ThirdText.color = Color.green;
	}
}
