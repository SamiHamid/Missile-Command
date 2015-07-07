using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour
{

    [SerializeField] private TextMesh EnemyMissileCountText;
    [SerializeField] private TextMesh PlayerMissileCountText;
    [SerializeField] private TextMesh LevelText;
    [SerializeField] private TextMesh ScoreText;
    [SerializeField] private TextMesh BuildingCountText;

    private int _buildingsLeft;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void UpdateEnemyMissileUI(int count)
    {
        EnemyMissileCountText.text = "ENEMY MISSILE COUNT: " + count;
    }

    public void UpdatePlayerMissileUI(int count)
    {
        PlayerMissileCountText.text = "PLAYER MISSILE COUNT: " + count;
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
