using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : Destructable
{
	#region SerializedFields
	AsteroidDataHolder aData;
	#endregion
	#region Components
	Rigidbody2D self;
	Transform goalTransform;
	#endregion

	protected override void Awake()
	{
		base.Awake();
		aData = (AsteroidDataHolder)data;
		self = GetComponent<Rigidbody2D>();
		goalTransform = GameObject.Find("LevelManager").transform;
		StartMoving();
	}

	private void Update()
	{
		//CheckDirection();
	}

	//NotUsed
	void CheckDirection()
	{
		if (aData.rotations.Length != 0)
		{
			int index = Mathf.FloorToInt(transform.rotation.eulerAngles.z / (360 / aData.rotations.Length));
			GetComponent<SpriteRenderer>().sprite = aData.rotations[index];
			print(index);
		}
	}

	public override void TakeDamage(int damage, GameObject attacker)
	{
		base.TakeDamage(damage, attacker);
		if(damage > 0 & Random.Range(0.0f, 1.0f) < aData.spawnChanceOnDamage)
		{
			SpawnAsteroid();
		}
	}

	protected void OnCollisionEnter2D(Collision2D coll)
	{
		foreach (string tag in aData.taggedToDamage)
		{
			if (coll != null & coll.gameObject.tag == tag)
			{
				int damage = Mathf.RoundToInt(coll.relativeVelocity.magnitude * aData.baseDamage);
				damage = damage > aData.maxDamage ? aData.maxDamage : damage;
				coll.gameObject.GetComponent<Destructable>().TakeDamage(damage, gameObject);
			}
		}

		foreach (string tag in aData.taggedToDestroy)
		{
			if (coll != null & coll.gameObject.tag == tag)
			{
				Instantiate(aData.deathAnim, transform.position, Quaternion.identity); //ToDo
				Die();
			}
		}
	}

	protected void OnCollisionStay2D(Collision2D coll)
	{
		foreach (string tag in aData.taggedToDestroy)
		{
			if (coll != null & coll.gameObject.tag == tag)
			{
				Instantiate(aData.deathAnim, transform.position, Quaternion.identity); //ToDo
				Die();
			}
		}
	}

	public override void Die()
	{
		//GetComponent<CircleCollider2D>().enabled = false;
		for (int i = 0; i < aData.smallerAsteroidCount; i++)
		{
			SpawnAsteroid();
		}
		Instantiate(aData.deathAnim, transform.position, Quaternion.identity);
		aData.gameMaster.PopScore(aData.scoreValue, transform.position);
		BeatMaster.Instance.PlaySound(aData.deathAudio);
		base.Die();
	}

	protected void SpawnAsteroid()
	{
		float radius = GetComponent<CircleCollider2D>().radius*0.8f;
		Vector3 randPos = new Vector3(self.position.x + Random.Range(-radius, radius), self.position.y + Random.Range(-radius, radius), 0);

		var instance = Instantiate(aData.smallerAsteroids[Random.Range(0, aData.smallerAsteroids.Length)], randPos, Quaternion.identity, goalTransform);
		instance.GetComponent<Rigidbody2D>().velocity = self.velocity * 0.8f;
	}

	public void StartMoving()
	{
		GetComponent<Rigidbody2D>().velocity = Quaternion.AngleAxis(-transform.localPosition.x * 
			Random.Range(0.0f, 1.0f), new Vector3(0, 0, 1)) * Vector2.down * Random.Range(aData.speedRangeMin, aData.speedRangeMax);
	}
}
