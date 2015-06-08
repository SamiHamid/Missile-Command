using UnityEngine;
using System.Collections;

public class HouseSpawner : MonoBehaviour 
{

    //======================================
    // Variable Declarations
	
	// handles
	

    // editor variables
    [SerializeField] private GameObject[] _housePrefabs;
    [SerializeField] private Transform _gameField;
    [SerializeField] private int _howManyHouses;
    [Range(2f, 20f)] public float _minHouseDistance;
    [SerializeField] private float _spawnDelay;

    // an offset value of 0.1 means that 90% of the playing field
    // will be used for spawning houses. We don't want hosues to spawn
    // at the edges of the playing field.
    [Range(0f, 0.2f)] public float XOffset;
    [Range(0f, 0.6f)] public float ZOffset;

    //======================================
    // Function Definitions
    public float XMin
    {
        get { return _gameField.transform.position.x - 5 * _gameField.localScale.x * (1 - XOffset); }
    }

    public float XMax
    {
        get { return _gameField.transform.position.x + 5 * _gameField.localScale.x * (1 - XOffset); }
    }

    public float ZMin
    {
        get { return _gameField.transform.position.z - 5 * _gameField.localScale.z * (1 - ZOffset); }
    }

    public float Zmax
    {
        get { return _gameField.transform.position.z + 5 * _gameField.localScale.z * (1 - ZOffset); }
    }

    // unity functions	
	void Start () 
    {
	
	}
	
	void Update () 
    {
	
	}


    public void StartSpawningHouses()
    {
        StartCoroutine(SpawnHouses());
    }

    IEnumerator SpawnHouses()
    {
        for (int i = 0; i < _howManyHouses; i++)
        {
            Vector3 pos = RandomPosition();

            GameObject house = Instantiate(_housePrefabs[Random.Range(0, 3)], pos, Quaternion.identity) as GameObject;
            house.transform.parent = transform;             // move the instance to the parent object
            yield return new WaitForSeconds(_spawnDelay);
        }

        gridMakerScript.GameStarted = true;     // after background is shown, enable the GameStarted flag, so missiles can start launching
    }

    Vector3 RandomPosition()
    {
        Vector3 pos = new Vector3(Random.Range(XMin, XMax), 0.001f, Random.Range(ZMin, Zmax));
        int layerMask = 1 << 8;     // use this layer to check collision of houses

        int numOfTries = 0;
        while (Physics.CheckSphere(pos, _minHouseDistance, layerMask) && numOfTries < 10)
        {
            //Debug.Log("Position " + pos + " too close to a house!");
            pos = new Vector3(Random.Range(XMin, XMax), 0.001f, Random.Range(ZMin, Zmax));
            //Debug.Log("New position acquired: " + pos);
            numOfTries++;
        }

        if (numOfTries == 10)
        {
            Debug.Log("FAILED TO FIND A POSITION WITHOUT OVERLAP | pos:" + pos);
        }

        return pos;
    }
}
