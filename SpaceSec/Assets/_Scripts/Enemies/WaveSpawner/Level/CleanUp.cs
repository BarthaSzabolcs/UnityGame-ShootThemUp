using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanUp : MonoBehaviour
{
	[SerializeField] string[] taggedForCleanUp;

	private void OnTriggerEnter2D(Collider2D coll)
	{
		foreach (var tag in taggedForCleanUp)
		{
			if (tag == coll.gameObject.tag)
			{
				Destroy(coll.gameObject);
			}
		}
	}
}
