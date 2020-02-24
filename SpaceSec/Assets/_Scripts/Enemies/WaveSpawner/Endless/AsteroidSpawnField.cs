using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class AsteroidSpawnField : SpawnEnemy
{
	[SerializeField] GameObject asteroid;
	[SerializeField] Vector2[] spawnPositions;

	public override void SpawnInstance(Transform goalTransfrom)
	{
		var instance = Instantiate(asteroid, (Vector2)goalTransfrom.position + spawnPositions[Random.Range(0, spawnPositions.Length)], Quaternion.identity, goalTransfrom);
		instance.GetComponent<Asteroid>().StartMoving();
	}

	public override void SpawnInstance(Transform goalTransfrom, Vector2 position)
	{
		var instance = Instantiate(asteroid, (Vector2)goalTransfrom.position + position, Quaternion.identity, goalTransfrom);
		instance.GetComponent<Asteroid>().StartMoving();
	}

}
