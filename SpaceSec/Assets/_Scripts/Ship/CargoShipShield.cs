using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoShipShield : Destructable
{
	public delegate void ShieldDown();
	public event ShieldDown OnShieldDown;
	[SerializeField] float shieldRegenSpeed;
	[SerializeField] float shieldRegenDelay;
	[SerializeField] Sprite[] shieldPhases;
	[SerializeField] ColorChange[] colorOptions;
	[SerializeField] float[] colors;
	enum ColorChange { DoNotChange, Decrase, DecraseToHalf,Incrase, IncraseToHalf };

	SpriteRenderer sRenderer;
	bool damageTaken;

	protected override void Awake()
	{
		base.Awake();
		sRenderer = GetComponent<SpriteRenderer>();
		colors = new float[4];
		ChangeAppearenceOnHealthChange();
	}

	public override void TakeDamage(int damage, GameObject attacker)
	{
		base.TakeDamage(damage, attacker);
		ChangeAppearenceOnHealthChange();
		damageTaken = true;
	}

	public override void GainHealth(int heal)
	{
		base.GainHealth(heal);
		ChangeAppearenceOnHealthChange();
	}

	private void ChangeAppearenceOnHealthChange()
	{
		if (sRenderer == null)
		{
			sRenderer = GetComponent<SpriteRenderer>();
		}

		sRenderer.color = CalculateColor(sRenderer.color);
		sRenderer.sprite = shieldPhases[shieldPhases.Length - 1 - Mathf.FloorToInt((shieldPhases.Length - 1) * (float)Health / data.maxHealth)];
	}

	protected Color CalculateColor (Color color)
	{
		colors[0] = color.a;
		colors[1] = color.r;
		colors[2] = color.g;
		colors[3] = color.b;

		for (int i = 0; i < 4; i++)
		{
			if(colorOptions[i] == ColorChange.Decrase)
			{
				colors[i] = (float)Health / data.maxHealth;
			}
			else if (colorOptions[i] == ColorChange.DecraseToHalf)
			{
				colors[i] = 0.5f+ 0.5f*Health / data.maxHealth;
			}
			else if (colorOptions[i] == ColorChange.Incrase)
			{
				colors[i] = 1- (float)Health / data.maxHealth;
			}
			else if (colorOptions[i] == ColorChange.IncraseToHalf)
			{
				colors[i] = 0.5f * (1- Health / data.maxHealth);
			}
		}
		color.a = colors[0];
		color.r = colors[1];
		color.g = colors[2];
		color.b = colors[3];
		return color;
	}

	public override void Die()
	{
		if (OnShieldDown != null) { OnShieldDown(); }
		gameObject.SetActive(false);
	}

	public void ReviveShield()
	{
		Health = 1;
		dead = false;
		damageTaken = false;
	}

	IEnumerator ShieldRegen()
	{
		while (true)
		{
			if (damageTaken != true & Health < data.maxHealth)
			{
				GainHealth(1);
				yield return new WaitForSeconds(shieldRegenSpeed);
			}
			else
			{
				damageTaken = false;
				yield return new WaitForSeconds(shieldRegenDelay);
			}
			
		}
	}

	private void OnEnable()
	{
		StartCoroutine("ShieldRegen");
	}

}
