﻿using UnityEngine;
using System.Collections;

public class gridMakerScript : MonoBehaviour

{
    // static variables
    public static bool InitializationStarted = false;   // Makes sure world initialization code is called only once
                                                        // otherwise player can spam "Spacebar" and instantiate multiple lines
	// Field Bars
	public GameObject FieldBar;
	public int NumberOfLines;
    public float WaitLength;

    public Transform GameFieldPlane;
    public GameObject Background;
    public GameObject BackgroundParticles;

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
        StartCoroutine(GenerateGridZ());
    }
	
	// Field Bars
	IEnumerator GenerateGrid()
	{
		for (int i=0; i<NumberOfLines+1; i++)
		{
            float zLoc = _startingPosition.z + i * _lineSpacing;     // Adjust Z for spacing on each iteration
			GameObject instance = Instantiate(FieldBar, new Vector3 (0, 0.2f, zLoc), Quaternion.Euler(new Vector3(0, 0, 90))) as GameObject;

            if (instance != null) instance.transform.parent = transform;    // move instantiated objects under the gridMaker object

            GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(WaitLength);
		}
		
        BackgroundActivated();
        HouseSpawner.StartSpawningHouses();
    }

    IEnumerator GenerateGridZ()
    {   // wait for half the waitLength for the opposite phase
        // so that the grid generation goes X, Z, X, Z...
        yield return new WaitForSeconds(0.05f); 

        GameObject GridZ = GameObject.Find("GridZ");

        Vector3 startingPos = GameFieldPlane.position - new Vector3(5 * GameFieldPlane.localScale.x, 0, 0);

        for (int i = 0; i < NumberOfLines + 1; i++)
        {
            float xLoc = startingPos.x + i * _lineSpacing;     // Adjust X for spacing on each iteration
            GameObject instance = Instantiate(FieldBar, new Vector3(xLoc, 0.2f, 40f), Quaternion.Euler(new Vector3(0, 90, 90))) as GameObject;

            if (instance != null) instance.transform.parent = GridZ.transform;    // move instantiated objects under the gridMaker object

            GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(WaitLength);
        }
    }
	
	// Background
	void BackgroundActivated()
	{
		Background.SetActive(true);
		GameObject BackgroundParticles = GameObject.Find("BackgroundParticles");
		ParticleScript particleScript = BackgroundParticles.GetComponent<ParticleScript>();
		particleScript.ToggleActive();
		Background.GetComponent<AudioSource>().Play();
	}

    public void DeactivateBackground()
    {
        Background.GetComponent<AudioSource>().Play();
        Background.SetActive(false);
    }
	
}
