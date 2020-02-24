using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "EnemyDataHolder/ShootingEnemy")]
public class ShootingEnemyDataHolder : DestructableDataHolder
{
	[Header("ShootingEnemy Data:")]
	public bool followPlayer;
	public Vector2[] turretPositions;
	public TurretDataHolder[] turretTypes;
	public bool rotateTurretsOnly;
}
