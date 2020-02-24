using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FloatingScores : MonoBehaviour
{
	[SerializeField] float speed;
	[SerializeField] float lifeTime;
	[SerializeField] float fadeTime;
	Text text;

	void Start ()
	{
		StartCoroutine(SelfDestruct());
		text = GetComponent<Text>();
	}

	public void SetUpScore(int value, float cameraSpeed)
	{
		GetComponent<Text>().text = value.ToString();
		GetComponent<Rigidbody2D>().velocity = Vector2.up * (speed + cameraSpeed);
	}

	IEnumerator SelfDestruct()
	{
		yield return new WaitForSeconds(0.5f);

		text.CrossFadeAlpha(0,fadeTime, false);
		yield return new WaitForSeconds(fadeTime);
		Destroy(gameObject);
	}
}
