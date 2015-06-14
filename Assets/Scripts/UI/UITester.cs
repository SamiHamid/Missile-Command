using UnityEngine;
using System.Collections;

public class UITester : MonoBehaviour {

	// UI DEBUGGER
	// PRESS Q TO ADD LEVELS
	// PRESS W TO ADD SCORE
	// PRESS E TO ADD ENEMY MISSILES AND D TO SUBTRACT ENEMY MISSILES
	// PRESS R TO ADD USER MISSILES AND F TO SUBTRACT USER MISSILES
	// PRESS T TO ADD CITIES AND G TO SUBTRACT CITIES
	
	public GameObject Score;
	public GameObject Level;
	public GameObject EnemyMissile;
	public GameObject UserMissile; 
	public GameObject CitiesRemaining;
	
	private UILevelCounter LevelScript;
	private UIScoreCounter ScoreScript;
	private UIEnemyMissileCounter EnemyMissileScript;
	private UIPlayerMissileCounter UserMissileScript;
	private UICityCounter CitiesCounterScript;
	
	void Start () 
	{
		LevelScript = Level.GetComponent<UILevelCounter>();	
		ScoreScript = Score.GetComponent<UIScoreCounter>();
		EnemyMissileScript = EnemyMissile.GetComponent<UIEnemyMissileCounter>();
		UserMissileScript = UserMissile.GetComponent<UIPlayerMissileCounter>();
		CitiesCounterScript = CitiesRemaining.GetComponent<UICityCounter>(); 
	}
	
	
	void Update ()
	{
	    DebugKeyStrokes();

	    UpdateStats();
	}

    private void UpdateStats()
    {
        
    }

    private void DebugKeyStrokes()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            LevelScript.AddLevel();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            ScoreScript.AddScore();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            EnemyMissileScript.AddEnemyMissile();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            EnemyMissileScript.SubtractEnemyMissile();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            UserMissileScript.AddUserMissile();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            UserMissileScript.SubtractUserMissile();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            CitiesCounterScript.AddCity();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            CitiesCounterScript.SubtractCity();
        }

    }
}
