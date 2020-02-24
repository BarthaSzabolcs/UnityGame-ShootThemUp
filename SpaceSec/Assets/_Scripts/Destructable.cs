using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
	#region SerializedFields
	[SerializeField] protected DestructableDataHolder data;
	#endregion
	#region PrivateFields
	private int health;
	protected int Health
	{
		get { return health; }
		set
		{
			if (value >= data.maxHealth)
			{
				health = data.maxHealth;
			}else if (value <= 0)
			{
				health = 0;
			}
			else
			{
				health = value;
			}
		}
	}
	float flashProgress;
	protected bool dead = false;
	Coroutine flash;
	#endregion

	public virtual void TakeDamage(int damage, GameObject attacker)
	{
		if (data.flashOnHealthChange & flash == null & damage > 0)
		{
			flash = StartCoroutine(FlashRed());
		}

		if (!dead)
		{ 
			if (Health - damage < 0)
			{
				dead = true;
				Die();
			}
			else
			{
				if (data.hitAudio != null)
				{
					BeatMaster.Instance.PlaySound(data.hitAudio);
				}
				Health -= damage;
			}
		}
	}

	protected virtual void Awake()
	{
		Health = data.maxHealth;
	}

	public virtual void Die()//transfomr ami alá spawnolja a szutykot
	{
		if (data.deathAnim != null)
		{
			Instantiate(data.deathAnim, transform.position, Quaternion.identity);
		}
		if(data.deathAudio != null)
		{
			BeatMaster.Instance.PlaySound(data.deathAudio);
		}
		Destroy(gameObject);
	}

	void ResetHealth()
	{
		Health = data.maxHealth;
		dead = false;
	}

	public virtual void GainHealth(int heal)
	{
		Health += heal;
		if (data.flashOnHealthChange && flash == null && heal > 0)
		{
			flash = StartCoroutine(FlashGreen());
		}
		if (Health > data.maxHealth)
		{
			Health = data.maxHealth;
		}
	}

	protected IEnumerator FlashRed()
	{
		flashProgress = 1;
		while (flashProgress > 0)
		{
			GetComponent<SpriteRenderer>().color = new Color(1, 1 * flashProgress, 1 * flashProgress);

			flashProgress -= 1/ data.flashTime /2;
			yield return new WaitForSeconds(Time.deltaTime * (1 / Time.timeScale));
		}

		while (flashProgress <= 1)
		{
			GetComponent<SpriteRenderer>().color = new Color(1, 1 * flashProgress, 1 * flashProgress);

			flashProgress += 1 / data.flashTime / 2;
			yield return new WaitForSeconds(Time.deltaTime * (1 / Time.timeScale));
		}
		flash = null;
	}

	protected IEnumerator FlashGreen()
	{
		flashProgress = 1;
		while (flashProgress > 0)
		{
			GetComponent<SpriteRenderer>().color = new Color(1 * flashProgress, 1 , 1 * flashProgress);

			flashProgress -= 1 / data.flashTime / 2;
			yield return new WaitForSeconds(Time.deltaTime * (1 / Time.timeScale));
		}

		while (flashProgress <= 1)
		{
			GetComponent<SpriteRenderer>().color = new Color(1 * flashProgress, 1 , 1 * flashProgress);

			flashProgress += 1 / data.flashTime / 2;
			yield return new WaitForSeconds(Time.deltaTime * (1 / Time.timeScale));
		}
		flash = null;
	}
}
