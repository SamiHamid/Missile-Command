using UnityEngine;
using System.Collections;

public class gridMakerScript : MonoBehaviour

{
    // static variables
    public static bool GameStarted = false;     // missiles start to launch after GameStarted = true;
    public static bool InitializationStarted = false;   // Makes sure world initialization code is called only once
                                                        // otherwise player can spam "Spacebar" and instantiate multiple lines
	// Field Bars
	public GameObject FieldBar;
	public int NumberOfLines;
    public float WaitLength;

    public Transform GameFieldPlane;
    public GameObject Background;

    public HouseSpawner HouseSpawner;

    private Vector3 _startingPosition;
    private float _lineSpacing;
	

	
	
	void Start ()
	{
	    _startingPosition = GameFieldPlane.position - new Vector3(0, 0, 5 * GameFieldPlane.localScale.z);
	    _lineSpacing = GameFieldPlane.localScale.z*10/NumberOfLines;
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
            float zLoc = _startingPosition.z + i * _lineSpacing;     // Adjust Z for spacing on each iteration
			GameObject instance = Instantiate(FieldBar, new Vector3 (0, 0.2f, zLoc), Quaternion.Euler(new Vector3(0, 0, 90))) as GameObject;

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

        HouseSpawner.StartSpawningHouses();
	    
	}
	
}
