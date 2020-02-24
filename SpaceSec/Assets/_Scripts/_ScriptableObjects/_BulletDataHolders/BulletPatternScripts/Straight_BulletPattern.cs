using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BulletPattern/Precise")]
public class Straight_BulletPattern : BulletPatternData
{
	
	public override void ShootBullet(int patternLVL, Transform instanceTransform, GameObject bullet, Vector2 barrel, Transform bulletParent)
	{
		for (int i = 0; i < patternLVL; i++)
		{
			Vector2 pos = new Vector2(barrel.x + i * spacing - spacing * 0.5f * (patternLVL-1), barrel.y);

			GameObject instance = Instantiate(bullet, instanceTransform.position + instanceTransform.rotation * pos, Quaternion.identity, bulletParent);
			instance.transform.up = instanceTransform.up;
		}
	}

}
