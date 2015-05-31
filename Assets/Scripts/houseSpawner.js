

var maxHouses : int = 10;
var housePrefab : Transform;
var spawnPos : Vector3;

function Start () {
	for (var n=0; n < maxHouses; n++) {
		spawnPos.x = Random.Range(-150,150);
		spawnPos.z = Random.Range(-150,150);
		var newHouse = Instantiate(housePrefab, spawnPos, Quaternion.identity);
		Debug.Log("n = " + n);
	}
}

function Update () {


	
}