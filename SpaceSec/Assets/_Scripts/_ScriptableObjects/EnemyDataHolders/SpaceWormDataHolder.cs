using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Worm", menuName = "EnemyDataHolder/SpaceWorm")]
public class SpaceWormDataHolder : DestructableDataHolder
{
	[Header("SpaceWorm Data")]
	public string[] taggedToDamage;
	public string[] taggedToDestroy;
	public string[] taggedToSeek;
	public string[] taggedToEat;
	public string[] taggedToTaunt;

	public float speed;
	public float rotateSpeed;
	public float distance;

	public float integrityRange;
	public float seekingRange;
	public float mergeDistance;

	public int damage;

	public Sprite bodySprite;
	public Sprite[] rotations;

	public bool canGrow;
	public int growLimit;

	public GameObject head;
	public float damageFrequency;

	public string tag;
	public string[] seekPriorityList;
}
