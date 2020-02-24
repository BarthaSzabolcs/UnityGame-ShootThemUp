using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : ScriptableObject
{
	public string abilityName;
	public float coolDown;
	public Sprite skillImage;

	public virtual void ApplyEffect (GameObject gob)
	{
		Debug.Log("Lófasz se");
	}
}
