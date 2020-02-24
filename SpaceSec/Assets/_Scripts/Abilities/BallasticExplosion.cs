using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallasticExplosion : Destructable
{
	Rigidbody2D self;
	[SerializeField] float lifeTime;
	[SerializeField] float speed;

	private void Start()
	{
		self = GetComponent<Rigidbody2D>();
		self.velocity = Vector2.up * (Camera.main.GetComponent<Rigidbody2D>().velocity.y + speed);
		StartCoroutine(SpawnBaricade());
	}

	IEnumerator SpawnBaricade()
	{
		yield return new WaitForSeconds(lifeTime); 
		Destroy(gameObject);
	}
}
