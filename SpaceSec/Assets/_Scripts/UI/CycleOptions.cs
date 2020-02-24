using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CycleOptions : MonoBehaviour
{
	[SerializeField] Sprite[] options;
	[SerializeField] Image image;
	[SerializeField] string valueName;

	private void Start()
	{
		image.sprite = options[PlayerPrefs.GetInt(valueName, 0)];
	}

	public void CycleImage()
	{
		if(PlayerPrefs.GetInt(valueName, 0) < options.Length - 1)
		{
			PlayerPrefs.SetInt(valueName ,PlayerPrefs.GetInt(valueName, 0) + 1);
		}
		else
		{
			PlayerPrefs.SetInt(valueName, 0);
		}
		PlayerPrefs.Save();

		image.sprite = options[PlayerPrefs.GetInt(valueName, 0)];
	}

}
