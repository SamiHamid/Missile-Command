using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class MissileScript : MonoBehaviour
{

    private Rigidbody rb;   

	void Start ()
	{
	    rb = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate ()
	{
        // handle trajectory rotation -- rotate in accordance with the velocity
	    transform.rotation = Quaternion.LookRotation(rb.velocity);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {
            Destroy(gameObject);
            
        }
    }
	
}