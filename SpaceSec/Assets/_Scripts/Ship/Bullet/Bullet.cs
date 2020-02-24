using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	#region SerializedFields
	[SerializeField] public BulletDataHolder data;
	#endregion
	#region PrivateFields
	protected Rigidbody2D self;
	#endregion

	protected virtual void Start ()
	{
		GetComponent<SpriteRenderer>().color = data.bulletColor;
		self = GetComponent<Rigidbody2D>();
		self.velocity = transform.up * data.speed;
		self.mass = data.mass;
	}

	protected virtual void OnCollisionEnter2D(Collision2D coll)
	{

		foreach (var tag in data.taggedToDamage)
		{
			if (coll!=null & tag == coll.gameObject.tag)
			{
				coll.gameObject.GetComponent<Destructable>().TakeDamage(data.damage, gameObject);
			}
		}

		foreach (var tag in data.taggedToDestroy)
		{
			if (coll != null & tag == coll.gameObject.tag)
			{
				Instantiate(data.deathAnim, transform.position, Quaternion.identity);
				BeatMaster.Instance.PlaySound(data.deathAudio);
				Destroy(gameObject);
			}
		}

		foreach (var tag in data.taggedToDestroyWithoutEffects)
		{
			if (coll != null & tag == coll.gameObject.tag)
			{
				Destroy(gameObject);
			}
		}
	}

	protected void OnBecameInvisible()
	{
		Destroy(gameObject);
	}
}
