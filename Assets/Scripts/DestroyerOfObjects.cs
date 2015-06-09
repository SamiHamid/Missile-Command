using UnityEngine;
using System.Collections;
 
public class DestroyerOfObjects : MonoBehaviour {
 
    [SerializeField] private float _countdown;

	void Start () 
    {
	    Destroy(gameObject, _countdown);
	}
}