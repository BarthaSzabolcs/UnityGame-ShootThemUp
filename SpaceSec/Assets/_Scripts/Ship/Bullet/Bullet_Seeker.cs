using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Seeker: Bullet
{
	#region SerializedFields
	#endregion
	#region PrivateFields
	S_BulletDataHolder sData;
	GameObject target;
	float followTimer;
	#endregion

	protected override void Start()
	{
		base.Start();
		sData = (S_BulletDataHolder)data;
	}

	private void OnEnable()
	{
		StartCoroutine(FollowTheTarget());
	}

	protected void OnTriggerEnter2D(Collider2D coll)
	{
			Targeting(coll);
	}


	IEnumerator FollowTheTarget()
	{
		while (true)
		{ 
			if(target != null)
			{
				Vector2 direction = (Vector2)target.transform.position - self.position;

				direction.Normalize();

				float rotateAmmount = Vector3.Cross(direction, transform.up).z;

				self.angularVelocity = -rotateAmmount * sData.rotateSpeed;

				self.velocity = transform.up * data.speed;
				if(Time.time - followTimer > sData.followTime)
				{
					break;
				}
			}
			yield return new WaitForEndOfFrame();
		}
	}

	void Targeting(Collider2D coll)
	{
		if (target == null)
		{
			foreach (var tag in sData.taggedToSeek)
			{
				if (tag == coll.gameObject.tag)
				{
					target = coll.gameObject;
					followTimer = Time.time;
					break;
				}
			}
		}
	}
}
