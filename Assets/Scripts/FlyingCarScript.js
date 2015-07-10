#pragma strict
var startPos : Vector3;
var endPos : Vector3;
private var rb : Rigidbody;
//var exhaust : GameObject;
var exhaust : ParticleSystem;
var force : float = 15;

function Start () {
	startPos.x = Random.Range(-75.0, 75);
	startPos.y = 10;
	startPos.z = Random.Range(-30, 115);
	transform.position = startPos;
	endPos.x = startPos.x + Random.Range(-10, 10);
	endPos.y = Random.Range(10, 25);
	endPos.z = startPos.z + Random.Range(-10, 10);
	//endPos.y = 1000;
	rb = GetComponent.<Rigidbody>();
	//exhaust.SetActive (false);
	exhaust.Stop();
}

function Update () {
	if (Vector3.Distance(transform.position, endPos) > 5) {
		
		// move car Y-axis
		if (transform.position.y < endPos.y) {
			rb.AddRelativeForce(transform.up * force);
			if (exhaust.isPlaying == false) {
				exhaust.Play();
			}
		} else {
			if (exhaust.isPlaying == true) {
				exhaust.Stop();
				rb.AddRelativeForce(transform.up * force * 0.5);
			}
		}
		
		// move car X-axis
		if (transform.position.x > endPos.x) {
			rb.AddForce(transform.right * -force * 0.1);
		} else {
			rb.AddForce(transform.right * force * 0.1);
		}
		
		// move car Z-axis
		if (transform.position.z > endPos.z) {
			rb.AddForce(transform.forward * -force * 0.1);
		} else {
			rb.AddForce(transform.forward * force * 0.1);
		}
	} else {
		Debug.Log("flying car finds new endPos");
		endPos.x = Random.Range(-75.0, 75);
		endPos.y = Random.Range(5, 25);
		endPos.z = Random.Range(-30,115);
	}
	
}