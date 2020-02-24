using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class ShootingEnemy : ArmedDestructable
{
	[SerializeField] GameObject turret;

	ShootingEnemyDataHolder sData;
	GameObject target;
	List<Rigidbody2D> turretRB;

	protected override void Awake()
	{
		base.Awake();
		sData = (ShootingEnemyDataHolder)data;
		target = GameObject.Find("Player");
		self = GetComponent<Rigidbody2D>();

		turretRB = new List<Rigidbody2D>();

		for (int i = 0; i < sData.turretPositions.Length; i++)
		{
			if (sData.turretPositions[i].x == 0)
			{
				GameObject t = Instantiate(turret, self.position + sData.turretPositions[i], Quaternion.identity, transform);
				t.GetComponent<Turret>().Setup(sData.turretTypes[i]);
				turretRB.Add(t.GetComponent<Rigidbody2D>());
			}
			else
			{
				GameObject t = Instantiate(turret, self.position + sData.turretPositions[i], Quaternion.identity, transform);
				t.GetComponent<Turret>().Setup(sData.turretTypes[i]);
				turretRB.Add(t.GetComponent<Rigidbody2D>());

				t = Instantiate(turret, self.position + new Vector2(-sData.turretPositions[i].x, sData.turretPositions[i].y), Quaternion.identity, transform);
				t.GetComponent<Turret>().Setup(sData.turretTypes[i]);
				turretRB.Add(t.GetComponent<Rigidbody2D>());
			}

			
		}

		TurnOnTurrets();
	}

	protected virtual void Update()
	{
		if(sData.followPlayer)
		{
			RotateTowardsTarget();
		}
	}

	private void RotateTowardsTarget()
	{
		if (sData.rotateTurretsOnly)
		{
			foreach(Rigidbody2D rb in turretRB)
			{
				//rb.transform.up = (Vector2)target.transform.position - self.position;
				rb.transform.rotation = Quaternion.Euler(0, 0 , Vector3.Angle(target.transform.position ,self.position));
			}

		}
		else
		{
			self.transform.up = (Vector2)target.transform.position - self.position;
		}
	}

}
