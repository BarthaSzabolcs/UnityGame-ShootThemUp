using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretDropdown : MonoBehaviour
{
	[SerializeField] TurretDataHolder[] turrets;
	public Dropdown dropdown;
	[SerializeField] string stringToSet;
	[SerializeField] Transform turretImages;

	private void Start()
	{
		PopulateList();

		for (int i =0;  i < turrets.Length; i++)
		{
			if (turrets[i].name == PlayerPrefs.GetString(stringToSet))
			{
				dropdown.value = i;
				dropdown.captionText.text = turrets[i].turretName;
				SetTurretImage(i);
			}
		}
			
	}

	void SetTurretImage(int i)
	{
		if(name == "MainTurretSelect")
		{
			turretImages.Find("MainCannon").GetComponent<Image>().sprite = turrets[i].centerTurret;
		}
		else if(name == "SecondaryTurretSelect")
		{
			turretImages.Find("Secondary_R").GetComponent<Image>().sprite = turrets[i].rightTurret;
			turretImages.Find("Secondary_L").GetComponent<Image>().sprite = turrets[i].leftTurret;
		}
	}

	public void Dropdown_indexChanged(int i)
	{
		PlayerPrefs.SetString(stringToSet, turrets[i].name);
		PlayerPrefs.Save();
		SetTurretImage(i);
	}

	void PopulateList()
	{
		List<string> names = new List<string>();
		for (var i = 0; i < turrets.Length; i++)
		{
			names.Add(turrets[i].turretName);
		}
		dropdown.AddOptions(names);
	}
}
