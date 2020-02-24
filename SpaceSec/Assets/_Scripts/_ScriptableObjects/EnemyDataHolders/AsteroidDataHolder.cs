using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Asteroid" , menuName = "EnemyDataHolder/Asteroid")]
public class AsteroidDataHolder : DestructableDataHolder
{
	[Header ("Asteroid Data")]
	public GameMaster gameMaster;
	public int spawnValue;
	public float spawnDelay;
	public int scoreValue;

	public float speedRangeMin;
	public float speedRangeMax;

	public int maxDamage;
	public int baseDamage;
	public string[] taggedToDamage;
	public string[] taggedToDestroy;

	public Sprite[] rotations;

	public GameObject[] smallerAsteroids;
	public float spawnChanceOnDamage;
	public int smallerAsteroidCount;
}
