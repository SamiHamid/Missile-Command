#pragma strict
private var delay : float = 0.1;
private var spawnTime : float;
var startMat : Material;
var cubeMat : Material;
var rend : Renderer;



function Start () {
	spawnTime = Time.time;
	//Debug.Log ("r = " + GetComponent.<Renderer>().material.color);
	rend = GetComponent.<Renderer>();
	startMat = rend.material;
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
		
		if (Random.Range(0,100) > 50) {
			if (rend.material == startMat) {
				rend.material = cubeMat;
			} else {
				rend.material = startMat;
			}
		}
	}
}