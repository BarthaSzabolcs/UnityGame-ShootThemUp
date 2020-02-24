using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "BasicDataHolders/Turret")]
public class TurretDataHolder : ScriptableObject
{
	[Header("Turret Data:")]
	#region Variables
	#region SerializedFields
	[SerializeField] BulletPatternData patternData;
	public int lvlTreshold = 1;
	public string turretName;
	public Sprite leftTurret, centerTurret, rightTurret;
	public int sortingOrder = -2;
	public GameObject bullet;
	public Sprite muzzleFlashSprite;
	public float baseFireRate;

	[HideInInspector] public float currentFireRate;
	#endregion
	#region instanceVariables
	Rigidbody2D self;
	Transform bulletParent;
	[HideInInspector] public int lastFired;
	[HideInInspector] public SpriteRenderer muzzleFlashRenderer;
	public Vector2 barrel;
	public int turretLVL = 1;
	#endregion
	#endregion

	public void SetUp(Rigidbody2D rigidBody2D, Transform buletts, SpriteRenderer renderer)
	{
		self = rigidBody2D;
		
		currentFireRate = baseFireRate;
		bulletParent = buletts;
		muzzleFlashRenderer = renderer;
		muzzleFlashRenderer.sprite = muzzleFlashSprite;
	}
	
	public void ShootBullet(Transform t)
	{
		patternData.ShootBullet(Mathf.CeilToInt(turretLVL / (float)lvlTreshold), t, bullet, barrel, bulletParent);
	}
}
