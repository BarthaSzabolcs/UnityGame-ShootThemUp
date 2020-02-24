using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HealEffect : SimpleEffectDataHolder
{
	[SerializeField] int healAmmount;
	[SerializeField] string whoToHeal;
	public override void ApplyEffect(GameObject gob)
	{
		GameObject.Find(whoToHeal).GetComponent<Destructable>().GainHealth(healAmmount);
	}
}
