using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
	[SerializeField] Ability effect;

	private void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.tag == "Player")
		{
			effect.ApplyEffect(coll.gameObject);
			Destroy(gameObject);
		}
	}

}
