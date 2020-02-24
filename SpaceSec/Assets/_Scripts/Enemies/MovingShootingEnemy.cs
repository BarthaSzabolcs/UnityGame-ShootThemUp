using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingShootingEnemy : ShootingEnemy
{
	[Header("Movement Settings:")]
	[SerializeField] MovingPattern movementPattern;
	MovingPattern movingPatternInstance;

	protected override void Awake()
	{
		base.Awake();
		movingPatternInstance = Instantiate(movementPattern);
		movingPatternInstance.SetUp(self);
	}

	protected override void Update()
	{
		base.Update();
		movingPatternInstance.Move();
	}
}
