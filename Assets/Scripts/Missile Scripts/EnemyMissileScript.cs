using UnityEngine;
using System.Collections;

public class EnemyMissileScript : MonoBehaviour 
{
    [SerializeField]
    private ParticleSystem _explosion;

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
        if (other.tag == "Ground")
        {
            Detonate();
        }
    }

    public void Detonate()
    {
        Instantiate(_explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
	
}
