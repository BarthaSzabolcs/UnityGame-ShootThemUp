using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "BulletDataHolders/SelfDestructing")]
public class SelfDestructingBulletData : BulletDataHolder
{
	[Header("SelfDestruct Data")]
	public float lifeTime;
}
