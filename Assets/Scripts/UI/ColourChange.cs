using UnityEngine;
using System.Collections;

public class ColourChange : MonoBehaviour 
{
	public Color col;
	private Color startColor;
	private Color currentColor;
	private float blendValue;
	private bool GoBool;

	private float nextFire = 0.0f;
	private float fireRate = 1f;

	void Start()
	{
		startColor = transform.GetComponent<TextMesh>().color;
		currentColor = transform.GetComponent<TextMesh> ().color = Color.Lerp (startColor, col, blendValue);
		blendValue = 1;
	}

	void Update()
	{
		if (blendValue > 0 && GoBool == true) 
		{
			currentColor = transform.GetComponent<TextMesh> ().color = Color.Lerp (col, startColor, blendValue);
			blendValue -= Time.deltaTime * 0.5f;
			Debug.Log(blendValue);

		} else if (blendValue < 1 && GoBool == false)
		{
			transform.GetComponent<TextMesh>().color = Color.Lerp (currentColor, startColor, blendValue);
			blendValue += Time.deltaTime * 0.5f;
			Debug.Log(blendValue);
		}

		// Colour Changer
		transform.GetComponent<TextMesh>().color = Color.Lerp (currentColor, startColor, blendValue);

		if (Time.time > nextFire)
		{
			DeActivate();
			nextFire = Time.time + fireRate; 
			//Debug.Log(nextFire);
		}
	}

	public void Activate()
	{
		GoBool = true;
		nextFire += 0.1f;
	}

	public void DeActivate()
	{
		GoBool = false;
	}

}

