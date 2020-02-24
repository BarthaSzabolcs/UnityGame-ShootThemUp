using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy
{
	[SerializeField] public SpawnEnemy enemy;
	[SerializeField] public Vector2 spawnPosition;
	[SerializeField] public float spawnDelay;
}
