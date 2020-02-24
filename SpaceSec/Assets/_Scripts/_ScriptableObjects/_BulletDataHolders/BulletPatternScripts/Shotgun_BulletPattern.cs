using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BulletPattern/Shotgun")]
public class Shotgun_BulletPattern : BulletPatternData
{
	[SerializeField] float angle;
	[SerializeField] float angleOffSet;

	public override void ShootBullet(int patternLVL, Transform instanceTransform, GameObject bullet, Vector2 barrel, Transform bulletParent)
	{
		patternLVL -= 1;
		float offSet = instanceTransform.localPosition.x < 0 ? angleOffSet : -angleOffSet;

		if(patternLVL > 0)
		{
			for (int i = 0; i <= patternLVL; i++)
			{
				Vector3 pos = new Vector2(barrel.x + i * spacing - spacing * 0.5f * (patternLVL), barrel.y);

				GameObject instance = Instantiate(bullet, instanceTransform.position + instanceTransform.rotation * pos, Quaternion.identity, bulletParent);

				instance.transform.up = Quaternion.Euler(0, 0, i * angle / patternLVL - angle * 0.5f + offSet) * instanceTransform.up;
			}
		}
		else
		{
			GameObject instance = Instantiate(bullet, instanceTransform.position + instanceTransform.rotation * barrel, Quaternion.identity, bulletParent);
		}
		
	}

}
