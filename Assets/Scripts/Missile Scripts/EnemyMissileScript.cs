using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class EnemyMissileScript : MonoBehaviour 
{
    [SerializeField] private GameObject _impactPoint;

    private Vector2 _source, _destination;
    private float _score;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // handle trajectory rotation -- rotate in accordance with the velocity
        // for more explanation, see Section 4: Trajectory Rotation 
        // @ http://vilbeyli.github.io/Simple-Trajectory-Motion-Example-Unity3D/

        transform.rotation = Quaternion.LookRotation(rb.velocity);
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "House")
        {
            //SpawnSharpnel(other.transform.position);
            Detonate();
            SwapHouses(other.transform.gameObject);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Player")
        {
            Debug.Log("Enemy missile hits the explosion bubble");
            CalculateScore();
            Detonate();
        }

        if (other.transform.tag == "Ground")
        {
            Detonate();
        }
        
    }

    public void CalculateScore()
    {
        Vector2 currentPos = new Vector2(transform.position.x, transform.position.z);

        float initialDistance = Vector2.Distance(_source, _destination);
        float traveledDistance = Vector2.Distance(currentPos, _source);

        _score = (initialDistance/traveledDistance)*100;
        GameObject.FindObjectOfType<GameManager>().AccumulateLevelScore(_score);

        Debug.Log("Score Yielded: " + _score + "\nInitial Distance: " + initialDistance + "\nTraveled Distance: " + traveledDistance);
    }

    private void SwapHouses(GameObject house)
    {
        // Length of "(Clone)" is 7, this the substring is [0, Length-7]
        String asset = "Destructible " + house.name.Substring(0, house.name.Length - 7);

        // instantiate the destructible asset in place of normal house
        Instantiate(Resources.Load(asset, typeof (GameObject)), house.transform.position, Quaternion.identity);

        // cleanup the regular house
        Destroy(house);

        // update UI
        GameObject.FindObjectOfType<UIManager>().DestroyABuilding();
    }

    private void SpawnSharpnel(Vector3 housePos)
    {
        // shorthands
        Vector3 MPos = transform.position;      // missile position
        Vector3 HPos = housePos;                // house position

        // ignore the height difference in displacement
        Vector3 displacement = new Vector3(MPos.x - HPos.x, 0f, MPos.z - HPos.z);

        // normalize the displacement vector
        displacement.Normalize();
        float distance = Random.Range(1f, 3f);
        displacement *= distance;      // 1-3 meters away from the house

        // sharpnel spawn position          // add some height to sharpnel pos
        // Vector3 pos = HPos + displacement + new Vector3(0f, 2f, 0f);
        //Instantiate(_impactPoint, pos, Quaternion.identity);
    }   // NOT USED ANYMORE

    public void Detonate()
    {
		Instantiate(_impactPoint, transform.position, Quaternion.identity);
		Destroy(gameObject);
    }

    public void SetScoreData(Vector3 source, Vector3 destination)
    {
        _source = new Vector2(source.x, source.z);
        _destination = new Vector2(destination.x, destination.z);
    }
	
}
