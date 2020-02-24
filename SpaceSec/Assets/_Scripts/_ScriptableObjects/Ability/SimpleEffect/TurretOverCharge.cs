using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TurretOverCharge : SimpleEffectDataHolder
{
	[SerializeField] float duration, speed;

	public override void ApplyEffect(GameObject gob)
	{
		gob.GetComponent<PlayerShip>().OverChargeTurrets(duration, speed);
	}

}
