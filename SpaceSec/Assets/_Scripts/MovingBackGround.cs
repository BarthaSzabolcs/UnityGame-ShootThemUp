using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBackGround : MonoBehaviour
{
	[SerializeField] float scrollSpeed;
	Transform [] planes;
	Transform camera;
	Rigidbody2D self;

	public void OnLevelStart()
	{
		self.velocity = Vector3.up * scrollSpeed;
	}

	public void OnLevelEnd()
	{
		self.velocity = Vector3.zero;
	}

	void Awake ()
	{
		camera = Camera.main.transform;
		planes = transform.GetComponentsInChildren<Transform>();
		self = GetComponent<Rigidbody2D>();
	}

	void Update ()
	{
		foreach (Transform t in planes)
		{
			if(t.position.y < camera.position.y-60)
			{
				t.position = new Vector2(t.position.x, t.position.y + 60);
			}
		}
	}
}
