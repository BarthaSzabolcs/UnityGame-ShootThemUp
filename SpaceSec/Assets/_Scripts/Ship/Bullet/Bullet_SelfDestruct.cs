using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class Bullet_SelfDestruct : Bullet
{
	SelfDestructingBulletData sData;

	protected override void Start()
	{
		base.Start();
		sData = (SelfDestructingBulletData)data;
		StartCoroutine(SelfDestruct());
	}

	IEnumerator SelfDestruct()
	{
		yield return new WaitForSeconds(sData.lifeTime);
		Destroy(gameObject);
	}

}
