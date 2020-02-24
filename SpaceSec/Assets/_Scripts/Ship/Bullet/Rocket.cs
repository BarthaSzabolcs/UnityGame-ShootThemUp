using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class Rocket : Bullet
{
	R_BulletDataHolder rData;

	protected override void Start()
	{
		base.Start();
		rData = (R_BulletDataHolder)data;
	}

	protected override void OnCollisionEnter2D(Collision2D coll)
	{

		foreach (string tag in rData.taggedToDamage)
		{
			if (tag == coll.gameObject.tag)
			{
				Collider2D[] objectsInRange = Physics2D.OverlapCircleAll(transform.position, rData.explosionRange, rData.mask);
				foreach (Collider2D col in objectsInRange)
				{
					foreach (string tagInRange in data.taggedToDamage)
					{
						if (col.gameObject.tag == tagInRange)
						{
							col.gameObject.GetComponent<Destructable>().TakeDamage(data.damage, gameObject);
						}
					}
				}

				if (rData.damageTiles)
				{
					Tilemap tilemap = GameObject.Find("DestructableTiles").GetComponent<Tilemap>();

					Vector3Int currentCell = tilemap.WorldToCell(transform.position /*+ new Vector3(0, 1 , 0)*/);
					Vector3Int[] positions =
					{ currentCell ,
					currentCell + new Vector3Int(0,2,0),
					currentCell + new Vector3Int(0,1,0),
					currentCell + new Vector3Int(0,-1,0),
					currentCell + new Vector3Int(0,-2,0),
					currentCell + new Vector3Int(-1,1,0),
					currentCell + new Vector3Int(-1,0,0),
					currentCell + new Vector3Int(-1,-1,0),
					currentCell + new Vector3Int(-2,0,0),
					currentCell + new Vector3Int(1,1,0),
					currentCell + new Vector3Int(1,0,0),
					currentCell + new Vector3Int(1,-1,0),
					currentCell + new Vector3Int(2,0,0)};

					foreach (Vector3Int p in positions)
					{
						tilemap.SetTile(p, null);
					}
				}

				var deathAnim = Instantiate(data.deathAnim, transform.position, Quaternion.identity);
				deathAnim.GetComponent<Transform>().localScale *= rData.explosionRange / 10;
				Destroy(gameObject);
			}
		}

		foreach (string tag in rData.taggedToDestroyWithoutEffects)
		{
			if(tag == coll.gameObject.tag)
			{
				Destroy(gameObject);
			}
		}


	}
}
