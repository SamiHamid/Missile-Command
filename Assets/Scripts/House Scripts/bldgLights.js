#pragma strict
private var delay : float = 0.1;
private var spawnTime : float;

function Start () {
	spawnTime = Time.time;
	//Debug.Log ("r = " + GetComponent.<Renderer>().material.color);
}

function Update () {

}

function OnCollisionEnter(collision) {
	if (Time.time > spawnTime + delay) {	// slight delay so that blocks don't all change color at spawning
		/*
		var r = Random.Range(0.0,0.5);
		var g = Random.Range(0.0,0.5);
		var b = Random.Range(0.0,0.5);
		GetComponent.<Renderer>().material.color = Color(r,g,b) ;
		*/
		var currentColor = GetComponent.<Renderer>().material.color;
		GetComponent.<Renderer>().material.color = currentColor * 0.9;
	}
}