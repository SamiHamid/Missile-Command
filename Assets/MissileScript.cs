using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class MissileScript : MonoBehaviour {
 

    private Rigidbody rb;   

    // initial Vy & rotation difference between horizontal angle (270)
    private float Vyi, rotDif;
    private float planeAngle;

    // unity functions
	void Awake ()
	{
	
	}
	
	void Start ()
	{
	    rb = GetComponent<Rigidbody>();
	    rotDif = Mathf.Abs(270 - transform.rotation.eulerAngles.x);
        Vyi = rb.velocity.y;
	}
	
	void FixedUpdate ()
	{
        // rotate the missile on X axis based on its Vy: 
        // This way, the missile becomes parallel to the ground when Vy = 0
        transform.rotation = Quaternion.Euler(270 + (rb.velocity.y/Vyi) * rotDif, transform.rotation.y, 0f);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {
            Destroy(gameObject);
            
        }
    }
	
}