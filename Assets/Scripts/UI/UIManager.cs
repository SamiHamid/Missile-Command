﻿using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour
{

    [SerializeField] private TextMesh EnemyMissileCountText;
    [SerializeField] private TextMesh PlayerMissileCountText;
    [SerializeField] private TextMesh LevelText;
    [SerializeField] private TextMesh ScoreText;
    [SerializeField] private TextMesh BuildingCountText;

    private int _buildingsLeft;

    public void UpdateEnemyMissileUI(int count)
    {
        EnemyMissileCountText.text = "ENEMY MISSILES: " + count;
    }

    public void UpdatePlayerMissileUI(int count)
    {
        PlayerMissileCountText.text = "PLAYER MISSILES: " + count;
    }

    public void UpdateLevelText(int lvl)
    {
        LevelText.text = "LEVEL: " + lvl;
    }

    public void UpdateBuildingCountText(int count)
    {
        BuildingCountText.text = "CITIES LEFT: " + count;
        _buildingsLeft = count;
    }

    public void DestroyABuilding()
    {
        _buildingsLeft--;
        BuildingCountText.text = "CITIES LEFT: " + _buildingsLeft;
    }

    public void UpdateScore(float score)
    {
        ScoreText.text = "SCORE: " + ((int) score).ToString();
    }
}
