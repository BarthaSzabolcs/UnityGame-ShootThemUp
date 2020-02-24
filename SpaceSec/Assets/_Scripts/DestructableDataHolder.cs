using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BasicDataHolders/Destructable")]
public class DestructableDataHolder : ScriptableObject
{
	[Header("Destructable Data:")]
	public int maxHealth;
	public bool flashOnHealthChange;
	public float flashTime = 1f;
	
	public GameObject deathAnim;
	public string hitAudio;
	public string deathAudio;
}
