using UnityEngine;
using System.Collections;

public class UIEnemyMissileCounter : MonoBehaviour {

	private int EnemyMissile;
	
	void Awake()
	{
		EnemyMissile = 0;
	}
	
	void Update()
	{
		GetComponent<TextMesh>().text = "ENEMY MISSILE COUNT: " + EnemyMissile;
	}
	
	public void AddEnemyMissile()
	{
		EnemyMissile++;
		Debug.Log("Enemy Missile Added");
	}
	
	public void SubtractEnemyMissile()
	{
		EnemyMissile--;
		Debug.Log("Enemy Missile Subtracted");
	}
}
