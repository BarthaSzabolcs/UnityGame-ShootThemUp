using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Spawnable")]
public class SpawnerAbility : Ability
{
	[SerializeField] GameObject spawnTarget;
	[SerializeField] bool parent;
	[SerializeField] Vector2 offSet;

	public override void ApplyEffect(GameObject gob)
	{
		if(parent)
		{
			Instantiate(spawnTarget, gob.transform.position + (Vector3)offSet, Quaternion.identity, gob.transform);
		}
		else
		{
			Instantiate(spawnTarget, gob.transform.position + (Vector3)offSet, Quaternion.identity);
		}
	} 
}
