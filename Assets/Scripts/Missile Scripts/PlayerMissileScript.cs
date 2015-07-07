using UnityEngine;
using System.Collections;

public class PlayerMissileScript : MonoBehaviour 
{

    //======================================
    // Variable Declarations
	
	// handles
	[SerializeField] private GameObject _impactPoint;


    private Rigidbody rb;

    // editor variables


    //======================================
    // Function Definitions

    // getters & setters

    // unity functions	
	void Start ()
	{
	    rb = transform.GetComponent<Rigidbody>();
	}
	
	void Update ()
	{
	    transform.rotation = Quaternion.LookRotation(rb.velocity);

        if (Input.GetMouseButtonDown(1))
        {
            Detonate();
        }
	}


    private void Detonate()
    {
		Instantiate(_impactPoint, transform.position , Quaternion.identity);
        GetComponent<SphereCollider>().enabled = true;
        Destroy(gameObject, 0.2f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy Missile")
        {
            //Debug.Log("COLLIDING WITH ENEMY MISSILE");
            other.GetComponent<EnemyMissileScript>().CalculateScore();
            other.GetComponent<EnemyMissileScript>().Detonate();
        }
    }
}
