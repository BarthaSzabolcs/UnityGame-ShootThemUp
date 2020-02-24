using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "BulletDataHolders/Missle")]
public class S_BulletDataHolder : BulletDataHolder
{
	[Header("Missle Data")]
	public float rotateSpeed;
	public string[] taggedToSeek;
	public float homingRadius;
	public float followTime;
}
