using UnityEngine;
using System.Collections;


public class PlayerScript : MonoBehaviour
{
    //======================================
    // Variable Declarations
	
	// handles
	[SerializeField] private GameObject _playerMissile;
    [SerializeField] private Transform _playerMissileShooter;
    [SerializeField] private Managers _managers;

    // editor variables
    [SerializeField] private float force;
    
    //UI Missile
    public GameObject UIMissile;


    private int _missileCount;

    //======================================
    // Function Definitions

    // getters & setters
    public int MissileCount
    {
        get { return _missileCount; }
        set { _missileCount = value; }
    }

    // unity functions	
	void Start () 
    {

	}
	
	void Update () 
    {
	    if (GameManager.GameStarted && Input.GetMouseButtonDown(0) && _missileCount > 0)
	    {
	        ShootMissile();
	    }
	}


    public void ShootMissile()
    {
        GameObject missile =
            Instantiate(_playerMissile, _playerMissileShooter.position, Quaternion.identity) as GameObject;

        missile.GetComponent<Rigidbody>().velocity = _playerMissileShooter.forward * force;

        _missileCount--;
        _managers.UI.UpdatePlayerMissileUI(_missileCount);
    }
    
    public void ShootMissileUI()
    {
		GameObject missile =
			Instantiate(UIMissile, _playerMissileShooter.position, Quaternion.identity) as GameObject;
		
		missile.GetComponent<Rigidbody>().velocity = _playerMissileShooter.forward * force;
    }


}
