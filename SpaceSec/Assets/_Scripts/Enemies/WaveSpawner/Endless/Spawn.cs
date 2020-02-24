using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : ScriptableObject
{
	[Range(0,1)]public float spawnWeight;
	public int spawnValue;
	public int pointBeforeSpawn;
	public float spawnDelay;

	public virtual void SpawnInstance(Transform goalTransform)
	{
	}

	public virtual void SpawnInstance(Transform goalTransform, Vector2 position)
	{
	}
}
