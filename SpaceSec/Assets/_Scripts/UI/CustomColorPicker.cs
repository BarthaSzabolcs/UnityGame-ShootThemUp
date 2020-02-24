using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomColorPicker : MonoBehaviour
{

	[SerializeField] Image image;
	[SerializeField] string valueName;
	[SerializeField] Slider sliderR, sliderG, sliderB;

	private void Start()
	{
		sliderR.value = PlayerPrefs.GetFloat(valueName + "r", 1);
		sliderG.value = PlayerPrefs.GetFloat(valueName + "g", 1);
		sliderB.value = PlayerPrefs.GetFloat(valueName + "b", 1);

		//image.color = new Color(sliderR.value, sliderG.value, sliderB.value, 1);
	}

	public void SetRedColor(float value)
	{
		image.color = new Color(value, image.color.g, image.color.b, 1);
		PlayerPrefs.SetFloat(valueName + "r", value);
	}

	public void SetGreenColor(float value)
	{
		image.color = new Color(image.color.r, value, image.color.b, 1);
		PlayerPrefs.SetFloat(valueName + "g", value);
	}

	public void SetBlueColor(float value)
	{
		image.color = new Color(image.color.r, image.color.g, value, 1);
		PlayerPrefs.SetFloat(valueName + "b", value);
	}
}
