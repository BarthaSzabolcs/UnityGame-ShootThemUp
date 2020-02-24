using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Destructable
{
	#region [SerializeField]
	[SerializeField] int newLayer;
	[SerializeField] int oldLayer;
	#endregion
	#region
	Rigidbody2D player;
	Rigidbody2D self;
	ShieldDataHolder sData;
	float[] colors;
	SpriteRenderer sRenderer;
	#endregion

	private void ChangeAppearenceOnHealthChange()
	{
		if (sRenderer == null)
		{
			sRenderer = GetComponent<SpriteRenderer>();
		}

		sRenderer.color = CalculateColor(sRenderer.color);
		sRenderer.sprite = sData.shieldPhases[sData.shieldPhases.Length - 1 - Mathf.FloorToInt((sData.shieldPhases.Length - 1) * (float)Health / sData.maxHealth)];
	}

	protected Color CalculateColor(Color color)
	{
		colors[0] = color.a;
		colors[1] = color.r;
		colors[2] = color.g;
		colors[3] = color.b;

		for (int i = 0; i < 4; i++)
		{
			if (sData.colorOptions[i] == ShieldDataHolder.ColorChange.Decrase)
			{
				colors[i] = (float)Health / sData.maxHealth;
			}
			else if (sData.colorOptions[i] == ShieldDataHolder.ColorChange.DecraseToHalf)
			{
				colors[i] = 0.5f + 0.5f * Health / sData.maxHealth;
			}
			else if (sData.colorOptions[i] == ShieldDataHolder.ColorChange.Incrase)
			{
				colors[i] = 1 - (float)Health / sData.maxHealth;
			}
			else if (sData.colorOptions[i] == ShieldDataHolder.ColorChange.IncraseToHalf)
			{
				colors[i] = 0.5f * (1 - Health / sData.maxHealth);
			}
		}
		color.a = colors[0];
		color.r = colors[1];
		color.g = colors[2];
		color.b = colors[3];
		return color;
	}

	protected override void Awake()
	{
		base.Awake();
		sData = (ShieldDataHolder)data;
		player = GameObject.Find("Player").transform.Find("Body").gameObject.GetComponent<Rigidbody2D>();
		self = GetComponent<Rigidbody2D>();
		sRenderer = GetComponent<SpriteRenderer>();
		sRenderer.color = sData.baseColor;

		colors = new float[4];
		ChangeAppearenceOnHealthChange();

		StartCoroutine(LifeDrain());
		oldLayer = player.gameObject.layer;
		player.gameObject.layer = newLayer;
	}

	private void FixedUpdate()
	{
		self.MovePosition(player.position);
	}

	public override void TakeDamage(int damage, GameObject attacker)
	{
		ChangeAppearenceOnHealthChange();
		base.TakeDamage(damage, attacker);
	}

	public override void Die()
	{
		player.gameObject.layer = oldLayer;
		base.Die();
	}

	IEnumerator LifeDrain()
	{
		while (true)
		{
			yield return new WaitForSeconds(sData.lifeDrainSpeed);
			TakeDamage(sData.lifeDrain, gameObject);
		}
	}
}
