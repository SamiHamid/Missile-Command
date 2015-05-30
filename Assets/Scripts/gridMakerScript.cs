using UnityEngine;
using System.Collections;

public class gridMakerScript : MonoBehaviour

{
    public static bool GameStarted = false;     // missiles start to launch after GameStarted = true;
    public static bool InitializationStarted = false;   // Makes sure world initialization code is called only once
                                                        // otherwise player can spam "Spacebar" and instantiate multiple lines
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
	
	
	public void GameBegin()
	{
	    InitializationStarted = true;       // condition is checked in caller function of gameBegin()
		StartCoroutine(GenerateGrid());
		Invoke("BackgroundActivated", WaitLength * NumberOfLines + 0.5f);
	}
	
	// Field Bars
	IEnumerator GenerateGrid()
	{
		for (int i=0; i<NumberOfLines; i++)
		{
			float zLoc = StartingPosition.position.z + i * LineSpacing;     // adjust Z value
			GameObject instance = Instantiate(FieldBar, new Vector3 (0, 1, zLoc), StartingPosition.rotation) as GameObject;
            if (instance != null) instance.transform.parent = transform;    // move instantiated objects under the gridMaker object

            GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(WaitLength);
		}
		
	}
	
	// Background
	void BackgroundActivated()
	{
		Background.SetActive(true);
		Background.GetComponent<AudioSource>().Play();
	    GameStarted = true;     // after background is shown, enable the GameStarted flag, so missiles can start launching
	}
	
}
