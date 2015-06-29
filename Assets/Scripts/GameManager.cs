using System;
using UnityEngine;
using System.Collections;
using System.IO;

public class GameManager : MonoBehaviour
{

    public static bool GameStarted = false;     // missiles start to launch after GameStarted = true;

    [SerializeField] private UIManager _UI;
    [SerializeField] private HouseSpawner _houseSpawner;
    [SerializeField] private EnemyMissileLauncher _missileLauncher;
    [SerializeField] private PlayerScript _player;

    [SerializeField] private gridMakerScript _grid;
    
    //GUI
    [SerializeField] private UIElements _UIElements;
   
    
    [SerializeField] private int _currentLevel;    // 1 - 10

    private LevelData[] LData;

	// Use this for initialization
	void Start ()
	{
	    ReadLevelData();
	    UpdateLevelVariables(_currentLevel);
	}

    private void ReadLevelData()
    {
        // open the data file
        StreamReader file = new StreamReader(File.OpenRead(@"Assets/Data/levels.csv"));

        // initialize LevelData Array
        int levelCount = File.ReadAllLines(@"Assets/Data/levels.csv").Length - 1;
        LData = new LevelData[levelCount];

        // read the first line (get rid of the header). Data Line-up is as follows:
        // Level, #EMissileCount, EMissilePeriod, EMissileSpeed, #EnemyBugs, #PMissiles, #SupplyCrate, #Building, %Building2Win
        var line = file.ReadLine();

        int i = 0;

        // read the actual data
        while (!file.EndOfStream)
        {
            line = file.ReadLine();
            var values = line.Split(',');

            int level = Int32.Parse(values[0]);
            int EMissileCount = Int32.Parse(values[1]);
            float EMissilePeriod = float.Parse(values[2]);
            float EMissileSpeed = float.Parse(values[3]);
            int BugCount = Int32.Parse(values[4]);
            int PMissileCount = Int32.Parse(values[5]);
            int SupplyCrateCount = Int32.Parse(values[6]);
            int BuildingCount = Int32.Parse(values[7]);
            float BuildingPct = float.Parse(values[8]);

            LData[i++] = new LevelData(level, EMissileCount, EMissilePeriod, EMissileSpeed, BugCount, PMissileCount, SupplyCrateCount, BuildingCount, BuildingPct);
            //LData[i-1].Print();
        }

        file.Close();
    }

    private void UpdateLevelVariables(int level)
    {
        int i = level - 1;
        Debug.Log("Updated Level Variables for Level " + LData[i].Level);

        _houseSpawner.HowManyHouses = LData[i].BuildingCount;
        _missileLauncher.MissileCount = LData[i].EnemyMissileCount;
        _missileLauncher.ShootingPeriod = LData[i].EnemyMissileDelay;
        _player.MissileCount = LData[i].PlayerMissileCount;

        LData[i].Print();
        UpdateUI();
    }

    private void UpdateUI()
    {
        _UI.UpdateLevelText(_currentLevel);
        _UI.UpdateEnemyMissileUI(_missileLauncher.MissileCount);
        _UI.UpdatePlayerMissileUI(_player.MissileCount);
        _UI.UpdateBuildingCountText(_houseSpawner.HowManyHouses);
        // TODO: Score Text Update Function
    }

    // Update is called once per frame
	void Update () 
    {
	
	}


    public void LevelFinished()
    {
		float buildingPct = 100 * ((float)_houseSpawner.transform.childCount / (float)LData[_currentLevel-1].BuildingCount);
		Debug.Log ("Buildings Left: " + _houseSpawner.transform.childCount + "\tInitiallty: " + LData[_currentLevel-1].BuildingCount + "\nbuildings left %: " + buildingPct);

        StartCoroutine(CleanUp());  // start the cleanup
        if (buildingPct > LData[_currentLevel - 1].PctToWin)
        {
            Debug.Log("LEVEL FINISHED: GAME WON!");
			Instantiate(_UIElements.WinUI, new Vector3 (0.0f, 26.6f, 39.6f), Quaternion.identity);

            StartCoroutine(AdvanceNextLevel());
        }

        else
        {
			Instantiate(_UIElements.LoseUI, new Vector3 (0.05f, 26.8f, 45.4f), Quaternion.identity);
			Instantiate (_UIElements.RestartUI, new Vector3 (0.0f, 3.6f, -46f), Quaternion.identity);
            Debug.Log("LEVEL FINISHED: GAME LOST!");

            // TODO: what happens when current level is lost?
        }

	}

    IEnumerator AdvanceNextLevel()
    {
        if (_currentLevel != 10)
        {
            // Wait for preparations
            Debug.Log("Loading next level in 10...");
            yield return new WaitForSeconds(10f);

            // remove "Level Complete" UI
            Destroy(GameObject.Find("LevelComplete(Clone)"));

            // level up!
            _currentLevel++; 
            UpdateLevelVariables(_currentLevel);

            _grid.GameBegin();
        }
        else
        {
            // if last level is finished, USE GAME OVER SCREEN
            // TODO: Game Over Code here
            Debug.Log("END OF LEVELS, GAME OVER!");
        }
    }

    IEnumerator CleanUp()
    {
        // intended to take at most 10 seconds
        // NextLevel() function is simply called after 10 secs
        yield return new WaitForSeconds(2f);
        StartCoroutine(RemoveHouses());
        StartCoroutine(RemoveGrid());
        MoveBullsEye();
    }

    private void MoveBullsEye()
    {
        // move to initial position - behind the eye
        GameObject.FindGameObjectWithTag("BullsEye").transform.position = new Vector3(0f, 1000f, -500f);
    }

    IEnumerator RemoveGrid()
    {
        float wait = 0.25f;
        _grid.DeactivateBackground();

        for (int i= _grid.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(_grid.transform.GetChild(i).gameObject);
            yield return new WaitForSeconds(wait);
        }


    }

    IEnumerator RemoveHouses()
    {

        float wait = 0.1f;

        // remove destructable house parts
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("DHouse"))
        {
            Destroy(obj);
            yield return new WaitForSeconds(wait);
        }

        // remove remaining houses
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("House"))
        {
            Destroy(obj);
            yield return new WaitForSeconds(wait);
        }
    }




}

[Serializable]
public class UIElements
{
    [SerializeField] private GameObject _loseUI;
    [SerializeField] private GameObject _restartUI;
    [SerializeField] private GameObject _winUI;

    public GameObject LoseUI
    {
        get { return _loseUI; }
    }

    public GameObject RestartUI
    {
        get { return _restartUI; }
    }

    public GameObject WinUI
    {
        get { return _winUI; }
    }
}

public class LevelData
{
    private readonly int _level;
    private readonly int _enemyMissileCount;
    private readonly float _enemyMissileDelay;
    private readonly float _enemyMissileSpeed;
    private readonly int _enemyBugCount;
    private readonly int _playerMissileCount;
    private readonly int _supplyCrateCount;

    public int Level
    {
        get { return _level; }
    }

    public int EnemyMissileCount
    {
        get { return _enemyMissileCount; }
    }

    public float EnemyMissileDelay
    {
        get { return _enemyMissileDelay; }
    }

    public float EnemyMissileSpeed
    {
        get { return _enemyMissileSpeed; }
    }

    public int EnemyBugCount
    {
        get { return _enemyBugCount; }
    }

    public int PlayerMissileCount
    {
        get { return _playerMissileCount; }
    }

    public int SupplyCrateCount
    {
        get { return _supplyCrateCount; }
    }

    public int BuildingCount
    {
        get { return _buildingCount; }
    }

    public float PctToWin
    {
        get { return _pctToWin; }
    }

    private int _buildingCount;
    private float _pctToWin;

    public LevelData(int level, int enemyMissileCount, float enemyMissileDelay, float enemyMissileSpeed, int enemyBugCount, int playerMissileCount, int supplyCrateCount, int buildingCount, float pctToWin)
    {
        _level = level;
        _enemyMissileCount = enemyMissileCount;
        _enemyMissileDelay = enemyMissileDelay;
        _enemyMissileSpeed = enemyMissileSpeed;
        _enemyBugCount = enemyBugCount;
        _playerMissileCount = playerMissileCount;
        _supplyCrateCount = supplyCrateCount;
        _buildingCount = buildingCount;
        _pctToWin = pctToWin;
    }

    public void Print()
    {
        String msg = "";


        msg += "Level: " + Level + "\n";
        msg += "Enemy Missile Count: " + _enemyMissileCount + "\n";
        msg += "Enemy Missile Delay: " + _enemyMissileDelay + "\n";
        msg += "Enemy Missile Speed: " + _enemyMissileSpeed + "\n";
        msg += "Enemy Bug Count: " + _enemyBugCount + "\n";
        msg += "Player Missile Count: " + _playerMissileCount + "\n";
        msg += "Supply Crate Count: " + _supplyCrateCount + "\n";
        msg += "Buildings to be Spawned: " + _buildingCount + "\n";
        msg += "Percentage of Buildings to Win: " + _pctToWin + "%\n";

        Debug.Log(msg);
    }
}
