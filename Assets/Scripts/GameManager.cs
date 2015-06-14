﻿using System;
using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor.AnimatedValues;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _startingLevel;

    private LevelData[] LData;

	// Use this for initialization
	void Start ()
	{
	    ReadLevelData();
	}

    // Update is called once per frame
	void Update () 
    {
	
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

            int level               = Int32.Parse(values[0]);
            int EMissileCount       = Int32.Parse(values[1]);
            float EMissilePeriod    = float.Parse(values[2]);
            float EMissileSpeed     = float.Parse(values[3]);
            int BugCount            = Int32.Parse(values[4]);
            int PMissileCount       = Int32.Parse(values[5]);
            int SupplyCrateCount    = Int32.Parse(values[6]);
            int BuildingCount       = Int32.Parse(values[7]);
            float BuildingPct       = float.Parse(values[8]);

            LData[i++] = new LevelData(level, EMissileCount, EMissilePeriod, EMissileSpeed, BugCount, PMissileCount, SupplyCrateCount, BuildingCount, BuildingPct);
            LData[i-1].Print();
        }

        file.Close();
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
