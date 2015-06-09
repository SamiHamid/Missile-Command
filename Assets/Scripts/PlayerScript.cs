using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private GameObject _playerMissile;
    [SerializeField] private Transform _playerMissileShooter;

    public float force;

    //======================================
    // Variable Declarations
	
	// handles
	
    // editor variables


    //======================================
    // Function Definitions

    // getters & setters

    // unity functions	
	void Start () 
    {
	    if (_playerMissileShooter.transform)
	    {
	        Debug.Log("OVR Null, switching missile shooter to normal cam");
            _playerMissileShooter = GameObject.Find("PlayerMissileLauncher_Normal").transform;
	    }
	}
	
	void Update () 
    {
	    if (gridMakerScript.GameStarted && Input.GetMouseButtonDown(0))
	    {
	        ShootMissile();
	    }
	}


    private void ShootMissile()
    {
        GameObject missile =
            Instantiate(_playerMissile, _playerMissileShooter.position, Quaternion.identity) as GameObject;

        missile.GetComponent<Rigidbody>().velocity = _playerMissileShooter.forward * force;
    }


}
