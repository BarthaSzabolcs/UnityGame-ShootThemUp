using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SecondaryCannonUp : SimpleEffectDataHolder
{
	public override void ApplyEffect(GameObject gob)
	{
		gob.GetComponent<PlayerShip>().SecondaryTurretsUp();
	}
}
