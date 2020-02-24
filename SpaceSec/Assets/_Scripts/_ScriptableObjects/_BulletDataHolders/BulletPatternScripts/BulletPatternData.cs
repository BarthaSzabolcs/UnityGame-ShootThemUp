using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPatternData : ScriptableObject
{
	[SerializeField] protected float spacing;

	public virtual void ShootBullet(int patternLVL, Transform instanceTransform, GameObject bullet, Vector2 barrel, Transform bulletParent)
	{

	}

}
