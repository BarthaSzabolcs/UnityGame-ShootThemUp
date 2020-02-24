using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ArmedDestructable : Destructable
{
	#region Events
	public delegate void CannonOn();
	public event CannonOn OnCannonOn;

	public delegate void CannonOff();
	public event CannonOff OnCannonOff;


	public delegate void SuperChargeOn(float speed);
	public event SuperChargeOn OnSuperChargeOn;

	public delegate void SuperChargeOff();
	public event SuperChargeOff OnSuperChargeOff;
	#endregion
	protected Rigidbody2D self;


	protected void TurnOnTurrets()
	{
		if (OnCannonOn != null)
		{
			OnCannonOn();
		}
	}

	protected void TurnOffTurrets()
	{
		if (OnCannonOff != null)
		{
			OnCannonOff();
		}
	}

	protected IEnumerator TurretOverCharge(float duration, float speed)
	{
		if (OnSuperChargeOn != null)
		{
			OnSuperChargeOn(speed);
		}

		yield return new WaitForSeconds(duration);

		if (OnSuperChargeOff != null)
		{
			OnSuperChargeOff();
		}
	}
}