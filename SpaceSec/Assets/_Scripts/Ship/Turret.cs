using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

	#region SerializedFields
	[SerializeField] TurretDataHolder data;
	#endregion
	#region Components
	Rigidbody2D self;
	TurretDataHolder instance;
	Coroutine shooting;
	#endregion

	public void Setup(TurretDataHolder newData)
	{
		data = newData;
		self = GetComponent<Rigidbody2D>();

		instance = Instantiate(data);

		if(transform.localPosition.x < 0)
		{
			GetComponent<SpriteRenderer>().sprite = data.leftTurret;
		}
		else if(transform.localPosition.x > 0)
		{
			GetComponent<SpriteRenderer>().sprite = data.rightTurret;
		}
		else if (transform.localPosition.x == 0)
		{
			GetComponent<SpriteRenderer>().sprite = data.centerTurret;
		}
		GetComponent<SpriteRenderer>().sortingOrder = data.sortingOrder;

		instance.SetUp(self, GameObject.Find("Bullets").transform, transform.GetChild(0).GetComponent<SpriteRenderer>());

		transform.parent.GetComponent<ArmedDestructable>().OnCannonOn += HandleCannonOn;
		transform.parent.GetComponent<ArmedDestructable>().OnSuperChargeOn += HandleSuperChargeOn;
	}

	void HandleCannonOn()
	{
		if(shooting == null) shooting = StartCoroutine(ShootBullets());
		transform.parent.GetComponent<ArmedDestructable>().OnCannonOn -= HandleCannonOn;
		transform.parent.GetComponent<ArmedDestructable>().OnCannonOff += HandleCannonOff;
	}

	IEnumerator ShootBullets()
	{
		yield return new WaitForSeconds(instance.currentFireRate);
		while (true)
		{
			ShootBullet();
			yield return new WaitForSeconds(instance.currentFireRate);
		}
	}

	void HandleCannonOff()
	{
		StopCoroutine(shooting);
		shooting = null;
		transform.parent.GetComponent<ArmedDestructable>().OnCannonOff -= HandleCannonOff;
		transform.parent.GetComponent<ArmedDestructable>().OnCannonOn += HandleCannonOn;
	}

	void HandleSuperChargeOn(float speed)
	{
		transform.parent.GetComponent<ArmedDestructable>().OnSuperChargeOn -= HandleSuperChargeOn;
		transform.parent.GetComponent<ArmedDestructable>().OnSuperChargeOff += HandleSuperChargeOff; 
		instance.currentFireRate = instance.baseFireRate * speed;
	}

	void HandleSuperChargeOff()
	{
		instance.currentFireRate = instance.baseFireRate;
		transform.parent.GetComponent<ArmedDestructable>().OnSuperChargeOff -= HandleSuperChargeOff;
		transform.parent.GetComponent<ArmedDestructable>().OnSuperChargeOn += HandleSuperChargeOn;
	}

	void ShootBullet()
	{
		instance.ShootBullet(transform);
		BeatMaster.Instance.PlaySound(data.bullet.GetComponent<Bullet>().data.bulletFiredAudio);
		StartCoroutine(MuzzleFlash());
	}

	IEnumerator MuzzleFlash()
	{
		BulletDataHolder bData = data.bullet.GetComponent<Bullet>().data;
	    instance.muzzleFlashRenderer.enabled = true;
		instance.muzzleFlashRenderer.transform.localPosition = transform.localPosition.x > 0 ? data.barrel : new Vector2 (-data.barrel.x, data.barrel.y);
		instance.muzzleFlashRenderer.color = bData.muzzleFlashColor;
		instance.muzzleFlashRenderer.transform.localScale = new Vector2(1.0f,1.0f);

		while (instance.muzzleFlashRenderer.transform.localScale.x < bData.muzzleFlashSize)
		{
			instance.muzzleFlashRenderer.transform.localScale *= 1.25f;
			yield return new WaitForEndOfFrame();
		}
		while (instance.muzzleFlashRenderer.transform.localScale.x > 1.0f)
		{
			instance.muzzleFlashRenderer.transform.localScale /= 1.25f;
			yield return new WaitForEndOfFrame();
		}
		instance.muzzleFlashRenderer.enabled = false;
	}

	private void OnDisable()
	{
		transform.parent.GetComponent<ArmedDestructable>().OnCannonOn -= HandleCannonOn;
		transform.parent.GetComponent<ArmedDestructable>().OnCannonOff -= HandleCannonOff;
		transform.parent.GetComponent<ArmedDestructable>().OnSuperChargeOff -= HandleSuperChargeOff;
		transform.parent.GetComponent<ArmedDestructable>().OnSuperChargeOn -= HandleSuperChargeOn;
	}

	public void TurretUp()
	{
		instance.turretLVL += 1;
	}
}
