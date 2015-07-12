#pragma strict
var startPos : Vector3;
var endPos : Vector3;
private var rb : Rigidbody;
//var exhaust : GameObject;
var exhaust : ParticleSystem;
var force : float = 15;
var impactPoint : GameObject;

function Start () {
	startPos.x = 0;
	startPos.y = 0;
	startPos.z = 150;
	transform.position = startPos;
	endPos.x = startPos.x + Random.Range(-10, 10);
	endPos.y = Random.Range(10, 25);
	endPos.z = Random.Range(0, 100);
	//endPos.y = 1000;
	rb = GetComponent.<Rigidbody>();
	//exhaust.SetActive (false);
	exhaust.Stop();
}

function Update () {
	if (Vector3.Distance(transform.position, endPos) > 5) {
		
		// move car Y-axis
		if (transform.position.y < endPos.y) {
			rb.AddForce(transform.up * force);
			if (exhaust.isPlaying == false) {
				exhaust.Play();
			}
		} else {
			if (exhaust.isPlaying == true) {
				exhaust.Stop();
				rb.AddForce(transform.up * force);
			}
		}
		
		// move car X-axis
		if (transform.position.x > endPos.x) {
			rb.AddForce(transform.right * -force);  // was *0.1
		} else {
			rb.AddForce(transform.right * force);
		}
		
		// move car Z-axis
		if (transform.position.z > endPos.z) {
			rb.AddForce(transform.forward * -force);
		} else {
			rb.AddForce(transform.forward * force);
		}
	} else {
		Debug.Log("flying car finds new endPos");
		endPos.x = Random.Range(-75.0, 75);
		endPos.y = Random.Range(10, 25);
		endPos.z = Random.Range(0,100);
	}
	
	if (transform.position.y < 5) {
		rb.AddForce(transform.up * force);
	}
	
}

function OnCollisionEnter (collision: Collision) {
	if (collision.transform.tag == "Player") {
		Instantiate(impactPoint, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}