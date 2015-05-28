using UnityEngine;
using System.Collections;

public class gridMakerScript : MonoBehaviour 

{	
	// Field Bars
	public GameObject FieldBar;
	public Transform StartingPosition;
	public float StartDistance;
	public int NumberOfLines;
	public float LineSpacing;
	public float WaitLength;
	
	//Background
	public GameObject Background;
	
	
	void Start ()
	{
	}
	
	
	public void gameBegin()
	{
		StartCoroutine(GenerateGrid());
		Invoke("BackgroundActivated", WaitLength * NumberOfLines + 0.5f);
	}
	
	// Field Bars
	IEnumerator GenerateGrid()
	{
		for (int i=0; i<NumberOfLines; i++)
		{
			float zLoc = StartingPosition.position.z + i * LineSpacing; 
			Instantiate(FieldBar, new Vector3 (0, 1, zLoc), StartingPosition.rotation);
			GetComponent<AudioSource>().Play();
			yield return new WaitForSeconds(WaitLength);	
		}
		
	}
	
	// Background
	void BackgroundActivated()
	{
		Background.SetActive(true);
		Background.GetComponent<AudioSource>().Play();
	}
	
}
