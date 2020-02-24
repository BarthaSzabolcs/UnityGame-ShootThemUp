using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipCustomizationDisplay : MonoBehaviour
{
	[Header("References")]
	[SerializeField] Image hullImage;
	[SerializeField] Image windowImage;
	[SerializeField] Image paintJobImage;
	[Header ("Selectable Items")]
	[SerializeField] Sprite [] paintJobs;

	private void Start()
	{
		windowImage.color = new Color(PlayerPrefs.GetFloat("WindowColorr"), PlayerPrefs.GetFloat("WindowColorg"), PlayerPrefs.GetFloat("WindowColorb"), 1);
		paintJobImage.color = new Color(PlayerPrefs.GetFloat("PaintJobColorr"), PlayerPrefs.GetFloat("PaintJobColorg"), PlayerPrefs.GetFloat("PaintJobColorb"), 1);
		hullImage.color = new Color(PlayerPrefs.GetFloat("HullColorr"), PlayerPrefs.GetFloat("HullColorg"), PlayerPrefs.GetFloat("HullColorb"), 1);

		paintJobImage.sprite = paintJobs[PlayerPrefs.GetInt("paintJobIndex", 0)];
	}
}
