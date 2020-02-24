using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpaceWormSpawn")]
public class SpaceWormSpawn : SpawnEnemy
{
	[SerializeField] GameObject worm;
	[SerializeField] int length;
	[SerializeField] float spawnDistance;
	[SerializeField] Vector2[] spawnPositions;

	public override void SpawnInstance(Transform goalTransform)
	{
		Vector2 randPos = spawnPositions[Random.Range(0, spawnPositions.Length)];

		GameObject prevInstance = Instantiate(worm, (Vector2)goalTransform.position+randPos, Quaternion.AngleAxis(180, Vector2.right), goalTransform);
		//prevInstance.GetComponent<SpaceWorm>().SetTarget(GameObject.Find("Player"));
		GameObject currentInstance;

		for (int i = 0; i < length; i++)
		{
			currentInstance = Instantiate(worm, (Vector2)goalTransform.position + new Vector2(randPos.x, randPos.y + (i+1)*spawnDistance), Quaternion.AngleAxis(180, Vector2.right), goalTransform);
			currentInstance.GetComponent<SpaceWorm>().SetTarget(prevInstance);
			prevInstance = currentInstance;
		}
		prevInstance.GetComponent<SpaceWorm>().RepairSnake();
	}

	public override void SpawnInstance(Transform goalTransform, Vector2 position)
	{

		GameObject prevInstance = Instantiate(worm, (Vector2)goalTransform.position + position, Quaternion.AngleAxis(180, Vector2.right), goalTransform);
		//prevInstance.GetComponent<SpaceWorm>().SetTarget(GameObject.Find("Player"));
		GameObject currentInstance;

		for (int i = 0; i < length; i++)
		{
			currentInstance = Instantiate(worm, (Vector2)goalTransform.position + position + new Vector2(0, (i + 1) * spawnDistance), Quaternion.AngleAxis(180, Vector2.right), goalTransform);
			currentInstance.GetComponent<SpaceWorm>().SetTarget(prevInstance);
			prevInstance = currentInstance;
		}
		prevInstance.GetComponent<SpaceWorm>().RepairSnake();
	}
}
