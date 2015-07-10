#pragma strict
var startPos : Vector3;
var endPos : Vector3;
var startTime : float = 0.0;
var journeyTime : float = 5.0;

function Start () {
	startPos = Random.insideUnitSphere * 100;
	startPos.y = 1;
	//Debug.Log("chick startPos = " + startPos);
	endPos = Random.insideUnitSphere * 100;
	endPos.y = 1;
}

function Update () {
 	var fracComplete = (Time.time - startTime) / journeyTime;
	transform.position = Vector3.Slerp(startPos, endPos, fracComplete);
	if (Vector3.Distance(transform.position, endPos) < 1){
		startPos = endPos;
		endPos = Random.insideUnitSphere * 100;
		endPos.y = 1;
		startTime = Time.time;
	}
}