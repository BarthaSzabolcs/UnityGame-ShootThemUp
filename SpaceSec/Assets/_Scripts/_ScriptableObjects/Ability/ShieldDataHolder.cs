using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ShieldDataHolder : DestructableDataHolder
{
	public Color baseColor;
	public Sprite[] shieldPhases;
	public ColorChange[] colorOptions;
	public float lifeDrainSpeed;
	public int lifeDrain;

	public enum ColorChange { DoNotChange, Decrase, DecraseToHalf, Incrase, IncraseToHalf };
}
