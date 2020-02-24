using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Destructable/PlayerShip")]
public class PlayerShipDataHolder : DestructableDataHolder
{
	[Header("PlayerShip Data")]
	public string shipName;
	public Sprite shipSprite;
	public Sprite windowSprite;
	public Color windowColor;
	public Sprite paintJobSprite;
	public Color paintJobColor;

	public GameObject turret;

	public Vector2 mainCannonPos;
	public Vector2 secondaryCannonPos;

	public Ability[] abilities;
	public float timeSlowScale;
	public float speed;
	public Vector3 fingerOffSet;
}
