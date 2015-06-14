using System;
using UnityEngine;
using System.Collections;

public class EnemyMissileScript : MonoBehaviour 
{
    [SerializeField] private ParticleSystem _explosion;
    [SerializeField] private GameObject _sharpnel;
    [SerializeField] private float _forceMagnitude;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // handle trajectory rotation -- rotate in accordance with the velocity
        transform.rotation = Quaternion.LookRotation(rb.velocity);
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "House")
        {
            SpawnSharpnel(other.transform.position);
            Detonate();
            SwapHouses(other.transform.gameObject);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Ground")
        {
            Detonate();
        }
    }

    private void SwapHouses(GameObject house)
    {
        // Length of "(Clone)" is 7, this the substring is [0, Length-7]
        String asset = "Destructible " + house.name.Substring(0, house.name.Length - 7);

        // instantiate the destructible asset in place of normal house
        Instantiate(Resources.Load(asset, typeof (GameObject)), house.transform.position, Quaternion.identity);

        // cleanup the regular house
        Destroy(house);

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
        displacement *= 5;      // 5 meters away from the house

        // sharpnel spawn position          // add some height to sharpnel pos
        Vector3 pos = HPos + displacement + new Vector3(0f, 2f, 0f);
        GameObject sharpnel = Instantiate(_sharpnel, pos, Quaternion.identity) as GameObject;

        // add the negative displacement force to sharpnel so it moves towards the house
        sharpnel.GetComponent<Rigidbody>().AddForce(-displacement * _forceMagnitude);
    }

    public void Detonate()
    {
        Instantiate(_explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
	
}
