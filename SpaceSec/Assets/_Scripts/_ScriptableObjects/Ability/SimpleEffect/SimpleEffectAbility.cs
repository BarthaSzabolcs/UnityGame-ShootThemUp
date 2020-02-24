using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Ability/SimpleEffect")]
public class SimpleEffectAbility : Ability
{
	[SerializeField] SimpleEffectDataHolder data;

	public override void ApplyEffect(GameObject gob)
	{
		data.ApplyEffect(gob);
	} 
}
