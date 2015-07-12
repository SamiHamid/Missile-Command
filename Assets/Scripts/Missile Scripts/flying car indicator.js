#pragma strict
var myMissile : Transform;
 
function Start () {

}

function Update () {
	transform.position = myMissile.transform.position;
	transform.position.y = 0.5;
	transform.rotation = Quaternion.identity;
	transform.localScale.y = 0.05 * myMissile.transform.position.y;
}