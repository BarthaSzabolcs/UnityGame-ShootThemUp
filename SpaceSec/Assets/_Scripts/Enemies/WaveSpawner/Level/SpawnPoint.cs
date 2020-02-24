using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
	[SerializeField] GameObject enemy;
	[SerializeField] string triggerTag;
	[SerializeField] string goalTransformName;
	Transform goalTransform;

	private void Start()
	{
		goalTransform = GameObject.Find(goalTransformName).GetComponent<Transform>();
	}

	private void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.gameObject.tag == triggerTag)
		{
			SpawnEnemy();
			Destroy(gameObject);
		}
	}

	private void SpawnEnemy()
	{
		Instantiate(enemy, transform.position, Quaternion.identity, goalTransform);
	}
}
