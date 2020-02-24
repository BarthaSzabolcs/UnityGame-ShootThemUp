using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour
{
	[SerializeField] LayerMask mask;
	[SerializeField] int abilityIndex;
	[SerializeField] int mouseButtonIndex;
	[SerializeField] string triggerName;
	[SerializeField] Color reloadColor ,readyColor;
	PlayerShip player;
	Image cdRadial;
	Image cdSkillImage;
	Image clickRadial;
	Image clickSkillImage;
	Transform clickTransform;
	public Vector2 relativePosition;
	public Vector2 fixPosition;

	private void Awake()
	{
		player = GameObject.Find("Player").GetComponent<PlayerShip>();

		cdRadial = transform.Find("CD/CD_Radial").GetComponent<Image>();
		cdSkillImage = transform.Find("CD/CD_SkillImage").GetComponent<Image>();

		clickRadial = transform.Find(triggerName+ "/Click_Radial").GetComponent<Image>();
		clickSkillImage = transform.Find(triggerName + "/Click_SkillImage").GetComponent<Image>();
		clickTransform = transform.Find(triggerName).GetComponent<Transform>();
	}

	void Update ()
	{
#if UNITY_STANDALONE ||UNITY_EDITOR

		if (Input.GetMouseButtonDown(mouseButtonIndex))
		{
			player.UseAbility(abilityIndex);
		}
#endif
#if UNITY_ANDROID
		if (Input.touchCount == 1)
		{
			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);

			RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 100.0f, mask);

			if (hit.collider != null && hit.collider.name == triggerName)
			{
				player.buttonClicked = true;
				if (Input.GetTouch(0).phase == TouchPhase.Began)
				{
					player.UseAbility(abilityIndex);
				}
			}
		}
		//else if (Input.touchCount > 1)
		//{
		//	Vector3 pos = Camera.main.ScreenToWorldPoint(Input.touches[1].position);

		//	RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 100.0f, mask);

		//	if (hit.collider != null && hit.collider.name == name)
		//	{
		//		//player.abilityClicked = true;
		//		if (Input.GetTouch(1).phase == TouchPhase.Began)
		//		{
		//			player.UseAbility(abilityIndex);
		//		}
		//	}
		//}
#endif
	}

	public void SetFill(float ammount)
	{
		cdRadial.fillAmount = ammount;
		clickRadial.fillAmount = ammount;

		if (ammount <= 0)
		{
			clickSkillImage.color = readyColor;
			cdSkillImage.color = readyColor;
		}
		else
		{
			clickSkillImage.color = reloadColor;
			cdSkillImage.color = reloadColor;
		}
	}

	public void SetSkillImage(Sprite skillImage)
	{
		cdSkillImage.sprite = skillImage;
		clickSkillImage.sprite = skillImage;
	}

	public void SetPosition(Vector2 newpos)
	{
		clickTransform.position = newpos;
	}

	public void HideClickable()
	{
		clickTransform.gameObject.SetActive(false);
	}

	public void ShowClickable()
	{
		clickTransform.gameObject.SetActive(true);
	}
}
