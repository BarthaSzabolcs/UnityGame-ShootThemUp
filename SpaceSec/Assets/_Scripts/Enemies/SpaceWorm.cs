using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceWorm : Destructable
{
	#region SerializedFields
	SpaceWormDataHolder wData;
	[SerializeField] SpaceWormDataHolder headData;
	[SerializeField] SpaceWormDataHolder bodyData;
	[SerializeField] SpaceWormDataHolder tailData;
	[SerializeField] float targetRefreshRate;
	#endregion
	#region PrivateFields
	Rigidbody2D self;
	int prioritySearchIndex;
	float damageTimer = 0;
	int growPhase;
	public int GrowPhase
	{
		get { return growPhase; }
		set
		{
			if (value >= wData.growLimit)
			{
				Grow();
				growPhase = 0;
			}
			else
			{
				growPhase = value;
			}
		}
	}
	float targetRefreshTimer;
	[SerializeField] GameObject target;
	[SerializeField] GameObject followedBy;
	#endregion

	private void Update()
	{
		CheckDirection();
	}

	void CheckDirection()
	{
		if (wData.rotations.Length != 0)
		{
			int index = Mathf.FloorToInt(transform.rotation.eulerAngles.z / (360 / wData.rotations.Length));
			if (index >= 0 && index < wData.rotations.Length)
			{
				GetComponent<SpriteRenderer>().sprite = wData.rotations[index];
			}
		}
	}

	public override void TakeDamage(int damage, GameObject attacker)
	{
		base.TakeDamage(damage, attacker);
		//foreach (var tag in wData.taggedToTaunt)
		//{
		//	if (tag == attacker.gameObject.tag)
		//	{
		//		Taunt("Player");
		//	}
		//}
	}

	public override void Die()
	{
		if(target != null && (target.tag == "SpaceWorm" || target.tag == "SpaceWormHead" || target.tag == "SpaceWormTail"))
		{
			target.GetComponent<SpaceWorm>().followedBy = null;
			RepairSnake();
		}
		if (followedBy != null)
		{
			followedBy.GetComponent<SpaceWorm>().target = null;
			RepairSnake();
		}
		base.Die();
	}
	
	protected override void Awake()
	{
		base.Awake();
		wData = (SpaceWormDataHolder)data;
		self = GetComponent<Rigidbody2D>();
		StartCoroutine(FollowTheTarget());
	}

	protected void OnTriggerEnter2D(Collider2D coll)
	{
			Targeting(coll);
	}

	protected void OnTriggerStay2D(Collider2D coll)
	{
		//if (tag == "SpaceWormHead" && Time.time - targetRefreshTimer > targetRefreshRate)
		//{
		//	Targeting(coll);
		//	targetRefreshTimer = Time.time;
		//}
		Targeting(coll);
	}

	protected void OnCollisionEnter2D(Collision2D coll)
	{
		foreach (string tag in wData.taggedToEat)
		{
			if (wData.canGrow && coll != null && coll.gameObject.tag == tag)
			{
				GrowPhase++;
			}
		}

		//foreach (string tag in wData.taggedToDamage)
		//{
		//	if (coll != null && coll.gameObject.tag == tag)
		//	{
		//		int damage = Mathf.RoundToInt(coll.relativeVelocity.magnitude * wData.baseDamage);
		//		damage = damage > wData.maxDamage ? wData.maxDamage : damage;
		//		coll.gameObject.GetComponent<Destructable>().TakeDamage(damage, gameObject);
		//	}
		//}

		foreach (string tag in wData.taggedToDamage)
		{
			if (coll != null && coll.gameObject.tag == tag)
			{
				if (Time.time - damageTimer > wData.damageFrequency)
				{
					coll.gameObject.GetComponent<Destructable>().TakeDamage(wData.damage, gameObject);

					damageTimer = Time.time;
				}
			}
		}

		foreach (string tag in wData.taggedToDestroy)
		{
			if (coll != null && coll.gameObject.tag == tag)
			{
				Die();
			}
		}
	}

	protected void OnCollisionStay2D(Collision2D coll)
	{
		//foreach (string tag in wData.taggedToDamage)
		//{
		//	if (coll != null && coll.gameObject.tag == tag)
		//	{
		//		if (Time.time - damageTimer > wData.damageFrequency)
		//		{
		//			int damage = Mathf.RoundToInt(coll.relativeVelocity.magnitude * wData.baseDamage);
		//			damage = damage > wData.maxDamage ? wData.maxDamage : damage;
		//			coll.gameObject.GetComponent<Destructable>().TakeDamage(damage, gameObject);

		//			damageTimer = Time.time;
		//		}
		//	}
		//}

		foreach (string tag in wData.taggedToDamage)
		{
			if (coll != null && coll.gameObject.tag == tag)
			{
				if (Time.time - damageTimer > wData.damageFrequency)
				{
					coll.gameObject.GetComponent<Destructable>().TakeDamage(wData.damage, gameObject);

					damageTimer = Time.time;
				}
			}
		}

		foreach (string tag in wData.taggedToDestroy)
		{
			if (coll != null && coll.gameObject.tag == tag)
			{
				Die();
			}
		}

	}

	IEnumerator FollowTheTarget()
	{
		while (true)
		{
			if (target != null )
			{
				if ((target.tag == "SpaceWormHead" || target.tag == "SpaceWorm" || target.tag == "SpaceWormTail")&& 
						Vector2.Distance(target.transform.position, self.position) > wData.integrityRange)//WormPartLost
				{
					SetTarget(null);
					RepairSnake();
				}
				//else if(Vector2.Distance(target.transform.position, self.position) > wData.seekingRange)
				//{
				//	SetTarget(null);
				//	RepairSnake();
				//}
				else if (target.tag == "SpaceWormTail" && Vector2.Distance(target.transform.position, self.position) < wData.mergeDistance)//WormMerge
				{
					RepairSnake();
				}
				else if (Vector2.Distance(target.transform.position, self.position) > wData.distance)
				{
					Vector2 direction = (Vector2)target.transform.position - self.position;

					direction.Normalize();

					float rotateAmmount = Vector3.Cross(direction, transform.up).z;

					self.angularVelocity = -rotateAmmount * wData.rotateSpeed;

					self.velocity = transform.up * wData.speed;
				}
			}
			else
			{
				SetData(headData);
				self.angularVelocity += 1;
				self.velocity = transform.up * wData.speed;
			}
			yield return new WaitForEndOfFrame();
		}
	}

	void Targeting(Collider2D coll)
	{
		if (target == null)
		{
			print("targeting");
			for (int i = 0; i < wData.taggedToSeek.Length; i++)
			{
				if (wData.taggedToSeek[i] == coll.tag)
				{
					if (coll.tag == "SpaceWormTail" || coll.tag == "SpaceWormHead")
					{
						if (coll.GetComponent<SpaceWorm>().followedBy == null && coll.gameObject != GetTail() && coll.GetComponent<SpaceWorm>().GetHead() != gameObject)
						{
							SetTarget(coll.gameObject);
							break;
						}
					}
					else
					{
						SetTarget(coll.gameObject);
						break;
					}
				}
			}
		}

	}

	public void SetData(SpaceWormDataHolder newData)
	{
		wData = newData;
		GetComponent<SpriteRenderer>().sprite = newData.bodySprite;
		gameObject.tag = newData.tag;
		transform.GetChild(0).gameObject.GetComponent<CircleCollider2D>().radius = newData.seekingRange;
		prioritySearchIndex = wData.taggedToSeek.Length;
	}

	public void SetTarget(GameObject newTarget)
	{
	
		//for (int i = 0; i < wData.taggedToSeek.Length; i++)
		//{
		//	if (newTarget != null && newTarget.tag == wData.taggedToSeek[i])
		//	{
		//		prioritySearchIndex = i+1;
		//	}
		//}

		//if (newTarget == null)
		//{
		//	prioritySearchIndex = wData.taggedToSeek.Length;
		//}

		if (newTarget == null && (target.tag == "SpaceWorm" || target.tag == "SpaceWormHead" || target.tag == "SpaceWormTail"))
		{
			target.GetComponent<SpaceWorm>().SetFollowedBy(null);
			target.GetComponent<SpaceWorm>().RepairSnake();
		}

		target = newTarget;

		if (target != null && (target.tag == "SpaceWorm" || target.tag == "SpaceWormHead" || target.tag == "SpaceWormTail"))
		{
			target.GetComponent<SpaceWorm>().SetFollowedBy(gameObject);
		}
	}

	public void SetFollowedBy(GameObject newFollowedBy)
	{
		followedBy = newFollowedBy;
	}

	public void RepairSnake()
	{
		GetHead().GetComponent<SpaceWorm>().CheckPositionInSneak();
	}

	public void CheckPositionInSneak()
	{
		if(target!= null)
		{
			if(target.tag == "SpaceWormTail" || target.tag == "SpaceWorm" || target.tag == "SpaceWormHead")
			{
				GetComponent<SpriteRenderer>().sortingOrder = target.GetComponent<SpriteRenderer>().sortingOrder -1;
			}
			else
			{
				GetComponent<SpriteRenderer>().sortingOrder = 0;
			}

			if ((target.tag == "SpaceWormHead" && followedBy != null) || (target.tag == "SpaceWorm" && followedBy != null))//-.-
			{
				SetData(bodyData);
			}
			else if ((target.tag == "SpaceWormHead" || target.tag == "SpaceWorm") && followedBy == null)//--.
			{
				SetData(tailData);
			}
			else if (target.tag == "SpaceWormTail")
			{
				if (Vector2.Distance(target.transform.position, transform.position) < wData.mergeDistance)
				{
					if (followedBy != null)
					{
						SetData(bodyData);
					}
					else
					{
						SetData(tailData);
					}
				}
				else
				{
					SetData(headData);
				}
			}
		}
		else
		{
			SetData(headData);
		}

		if(followedBy != null)
		{
			followedBy.GetComponent<SpaceWorm>().CheckPositionInSneak();
		}
	}

	//public void Taunt(string newTarget)
	//{
	//	if(target == null)
	//	{
	//		target = GameObject.Find(newTarget);
	//	}
	//	else if(target.tag != newTarget)
	//	{
	//		if(tag == "SpaceWormHead")
	//		{
	//			target = GameObject.Find(newTarget);
	//		}
	//		else if(tag == "SpaceWorm")
	//		{
	//			target.GetComponent<SpaceWorm>().Taunt(newTarget);
	//		}
	//	}
	//}

	void Grow()//Átírni úgy, hogy a farok nőjön, hátha jobb lesz
	{
		var instance = Instantiate(wData.head, (Vector2)transform.position + self.velocity.normalized, transform.localRotation, transform.parent);
		instance.GetComponent<SpaceWorm>().SetData(headData);
		instance.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder + 1;
		SetTarget(instance.gameObject);
		RepairSnake();
	}

	public GameObject GetHead()
	{
		if (target != null)
		{
			if (target.tag == "SpaceWormHead" || target.tag == "SpaceWorm" || target.tag == "SpaceWormTail")
			{
				return target.GetComponent<SpaceWorm>().GetHead();
			}
			else
			{
				return gameObject;
			}
				
		}
		else
		{
			return gameObject;
		}
	}

	public GameObject GetTail()
	{
		if (followedBy!= null)
		{
			return followedBy.GetComponent<SpaceWorm>().GetTail();
		}
		else
		{
			return gameObject;
		}
	}
}
