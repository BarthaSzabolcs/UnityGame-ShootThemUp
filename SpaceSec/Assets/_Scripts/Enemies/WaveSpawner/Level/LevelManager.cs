using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
	[SerializeField] GameMaster gameMaster;
	[SerializeField] string greetingText;
	[SerializeField] string victoryText;
	[SerializeField] float textFade;
	[SerializeField] float textTimer;

	Text MidText;

	void Start()
	{
		MidText = GameObject.Find("LevelManagerText").GetComponent<Text>();
		MidText.CrossFadeAlpha(0f, 0, false);
		StartCoroutine(WriteText(greetingText));
		gameMaster.StartLevel();
		//feliratkozás playerDeath meg hasonlók
	}

	IEnumerator WriteText(string text)
	{
		MidText.text = text;
		MidText.CrossFadeAlpha(1f, textFade, false);
		yield return new WaitForSeconds(textFade + textTimer);
		MidText.CrossFadeAlpha(0f, textFade, false);
	}

	IEnumerator EndLevelRoutine()
	{
		StartCoroutine(WriteText(victoryText));
		yield return new WaitForSeconds(2f);
		gameMaster.LoadMenu(0);
	}

	public void EndLevel()
	{
		StartCoroutine(EndLevelRoutine());
	}

}
