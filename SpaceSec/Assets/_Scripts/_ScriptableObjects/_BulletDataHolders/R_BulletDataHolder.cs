using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Rocket", menuName = "BulletDataHolders/Rocket")]
public class R_BulletDataHolder : BulletDataHolder
{
	[Header("Rocket Data")]
	public float explosionRange;
	public LayerMask mask;
	public bool damageTiles;
}
