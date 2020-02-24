using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfLevel : MonoBehaviour
{
	[SerializeField] GameMaster gameMaster;
	[SerializeField] string trigger;

	private void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.tag == trigger)
		{
			gameMaster.EndLevel();
		}
	}
	
}
