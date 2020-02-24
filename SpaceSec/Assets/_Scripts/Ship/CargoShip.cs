using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CargoShip : Destructable
{
	#region Variables
	#region Events
	public delegate void CargoShipDestroyed();
	public static event CargoShipDestroyed OnCargoShipDestroyed;
	#endregion
	#region SerializedFields
	[SerializeField] GameObject shield;
	[SerializeField] float shieldResetDelay;
	//[SerializeField] string[] taggedToDamage;  //collision damage to asteroids and stuff
	#endregion
	#region private
	Coroutine shieldReset;
	#endregion
	#region Components
	Rigidbody2D self;
	SpriteRenderer sRenderer;
	[SerializeField] Sprite[] damagePhases;
	#endregion
	#endregion

	
	public override void TakeDamage(int damage, GameObject attacker) // ToDo property override
	{
		base.TakeDamage(damage, attacker);
		ChangeAppearenceOnHealthChange();
		if (shieldReset != null) { shieldReset = StartCoroutine("ShieldReset"); }
	}

	public override void GainHealth(int heal)
	{
		base.GainHealth(heal);
		ChangeAppearenceOnHealthChange();
	}

	private void OnEnable()
	{
		shield.GetComponent<CargoShipShield>().OnShieldDown += HandleShieldDown;
	}

	void HandleShieldDown()
	{
		shieldReset = StartCoroutine("ShieldReset");
	}

	private void OnDisable()
	{
		shield.GetComponent<CargoShipShield>().OnShieldDown -= HandleShieldDown;
	}

	public override void Die()
	{
		BeatMaster.Instance.PlaySound(data.deathAudio);
		SceneManager.LoadScene(0);

		if(OnCargoShipDestroyed != null)
		{
			OnCargoShipDestroyed();
		}
	}

	IEnumerator ShieldReset()
	{
		yield return new WaitForSeconds(shieldResetDelay);
		shield.SetActive(true);
		shield.GetComponent<CargoShipShield>().ReviveShield();
		shieldReset = null;
	}

	private void ChangeAppearenceOnHealthChange()
	{
		if (sRenderer == null)
		{
			sRenderer = GetComponent<SpriteRenderer>();
		}

		sRenderer.sprite = damagePhases[damagePhases.Length - 1 - Mathf.FloorToInt((damagePhases.Length - 1) * (float)Health / data.maxHealth)];
	}
}
