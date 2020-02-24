using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "BulletDataHolders/Base")]
public class BulletDataHolder : ScriptableObject
{
	[Header("Bullet Data")]
	public string[] taggedToDamage;
	public string[] taggedToDestroy;
	public string[] taggedToDestroyWithoutEffects;
	public float speed;
	public int damage;
	public float mass;
	public Color bulletColor;
	public Color muzzleFlashColor;
	public float muzzleFlashSize;
	public string bulletFiredAudio;
	public string deathAudio;
	public GameObject deathAnim;
}
