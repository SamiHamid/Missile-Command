#pragma strict

private var spawnTime : float;

function Start () {
	spawnTime = Time.time;
}

function Update () {

}

function OnCollisionEnter(collision) {
	if (Time.time > spawnTime + 1) {	// slight delay so that blocks don't all change color at spawning
		var r = Random.Range(0.0,0.5);
		var g = Random.Range(0.0,0.5);
		var b = Random.Range(0.0,0.5);
		GetComponent.<Renderer>().material.color = Color(r,g,b);
	}
}