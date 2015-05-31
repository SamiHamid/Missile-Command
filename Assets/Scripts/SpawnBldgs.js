#pragma strict
var maxHouses : int;
private var numHouses : int = 0;
var towerPrefab : Transform;
var spiralPrefab : Transform;
var courthousePrefab : Transform;
var xSpawnRange : int;
var zSpawnRange : int;
private var newSpawn : Transform;
private var spawnPos : Vector3;
var spawnDelay : float;
private var lastSpawnTime : float = 0.0;

function Start () {
}

function Update () {
	if (numHouses <= maxHouses) {
		if (Time.time > lastSpawnTime + spawnDelay) {
			
			// Set spawn position for new building.
			spawnPos.x = Random.Range(-xSpawnRange,xSpawnRange);
			spawnPos.y = 0;
			spawnPos.z = Random.Range(-zSpawnRange,zSpawnRange);
			
			// Select type of new building, and spawn it.
			var randomType : int = Random.Range(1,4);
			if (randomType == 1) {
			newSpawn = Instantiate(towerPrefab, spawnPos, Quaternion.identity);
			}
			if (randomType == 2) {
				newSpawn = Instantiate(spiralPrefab, spawnPos, Quaternion.identity);
			}
			if (randomType == 3) {
				newSpawn = Instantiate(courthousePrefab, spawnPos, Quaternion.identity);
			}
			
			// Slightly randomize the size of new building.
			newSpawn.transform.localScale.x = newSpawn.transform.localScale.x * Random.Range (0.5, 1.5);
			newSpawn.transform.localScale.y = newSpawn.transform.localScale.y * Random.Range (0.5, 1.5);
			newSpawn.transform.localScale.z = newSpawn.transform.localScale.z * Random.Range (0.5, 1.5);
			newSpawn.transform.Rotate(Vector3.up * Random.Range (0,1000));
			
			lastSpawnTime = Time.time;
			numHouses ++;
		}
	}
}