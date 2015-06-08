using UnityEngine;
using System.Collections;

public class GarbagePlane : MonoBehaviour 
{

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
	
	}
	
	void Update () 
    {
	
	}

    void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
