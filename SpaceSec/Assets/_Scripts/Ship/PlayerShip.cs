using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerShip : ArmedDestructable
{
	#region Events

	#endregion
	#region SerializedFields
	[SerializeField] GameMaster gameMaster;
	[SerializeField] GameObject finger;
	[SerializeField] Sprite[] paintjobs;
	[SerializeField] PlayerShipDataHolder[] ships;
	[SerializeField] TurretDataHolder[] turrets;
	[SerializeField] AbilityButton[] buttons;
	int controlScheme;
	float[] coolDowns;
	#endregion
	#region PrivateFields
	Vector3 offSet;
	[HideInInspector] public bool buttonClicked = false;
	PlayerShipDataHolder sData;
	public int ControlScheme { get; private set; }
	#endregion

	private void Update()
	{
		Move();
		UpdateCooldowns();
	}

	void UpdateCooldowns()
	{ 
		for (int i = 0; i < buttons.Length; i++)
		{
			buttons[i].SetFill(1-((Time.timeSinceLevelLoad - coolDowns[i]) / sData.abilities[i].coolDown));
		}
	}

	public override void TakeDamage(int damage, GameObject attacker)
	{
		base.TakeDamage(damage, attacker);
	}

	protected override void Awake()
	{
		base.Awake();
		self = GetComponent<Rigidbody2D>();

		sData = (PlayerShipDataHolder)data;
		//SelectShip();
		SetLook();
		SetUpAbilityImages();
		SetUpTurrets();
		SetControllScheme();

#if UNITY_ANDROID
		finger.transform.localPosition = new Vector3(0, -sData.fingerOffSet.y);
#endif
		coolDowns = new float[sData.abilities.Length];
		GetComponent<SpriteRenderer>().sprite = sData.shipSprite;
	}

#region StartFunctions
	//private void SelectShip()
	//{
	//	foreach (var ship in ships)
	//	{
	//		if (ship.shipName == PlayerPrefs.GetString("ShipSelected", "KvibKvib"))
	//		{
	//			sData = ship;
	//		}
	//	}
	//}

	private void SetLook()
	{

		SpriteRenderer renderer = transform.Find("Window").GetComponent<SpriteRenderer>();
		renderer.color = new Color(PlayerPrefs.GetFloat("WindowColorr"), PlayerPrefs.GetFloat("WindowColorg"), PlayerPrefs.GetFloat("WindowColorb"), 1);
		SetLineRenderer(renderer.color);

		renderer = transform.Find("PaintJob").GetComponent<SpriteRenderer>();
		renderer.color = new Color(PlayerPrefs.GetFloat("PaintJobColorr"), PlayerPrefs.GetFloat("PaintJobColorg"), PlayerPrefs.GetFloat("PaintJobColorb"), 1);
		renderer.sprite = paintjobs[PlayerPrefs.GetInt("paintJobIndex")];

		renderer = transform.Find("HullColor").GetComponent<SpriteRenderer>();
		renderer.color = new Color(PlayerPrefs.GetFloat("HullColorr"), PlayerPrefs.GetFloat("HullColorg"), PlayerPrefs.GetFloat("HullColorb"), 1);

	}

	private void SetUpAbilityImages()
	{
		for (int i = 0; i < sData.abilities.Length; i++)
		{
			buttons[i].SetSkillImage(sData.abilities[i].skillImage);
		}
	}

	private void SetUpTurrets()
	{
		foreach (TurretDataHolder t in turrets)
		{
			if(t.name == PlayerPrefs.GetString("MainTurret"))
			{
				var mainTurret = Instantiate(sData.turret, transform);
				mainTurret.GetComponent<Turret>().Setup(t);
				mainTurret.transform.localPosition = sData.mainCannonPos;
				mainTurret.name = "MainTurret";
			}
			if(t.name == PlayerPrefs.GetString("SecondaryTurret"))
			{
				var secondaryRight = Instantiate(sData.turret, transform);
				secondaryRight.transform.localPosition = sData.secondaryCannonPos;
				secondaryRight.GetComponent<Turret>().Setup(t);
				secondaryRight.name = "SecondaryRight";

				var secondaryLeft = Instantiate(sData.turret, transform);
				secondaryLeft.transform.localPosition = new Vector2(-sData.secondaryCannonPos.x, sData.secondaryCannonPos.y);
				secondaryLeft.GetComponent<Turret>().Setup(t);
				secondaryLeft.name = "SecondaryLeft";
			}
		}		


		self = GetComponent<Rigidbody2D>();
		TurnOnTurrets();
	}

//	for (var i = 0; i< 3; i++)
//		{
//			foreach (TurretDataHolder t in turrets)
//			{
//				if(t.name == PlayerPrefs.GetString("Turret" + i))
//				{
//					var mainCannon = Instantiate(sData.turret, transform);
//	mainCannon.GetComponent<Turret>().Setup(t);
//	mainCannon.transform.localPosition = sData.turretPositions[i];

//					//var rightCannon = Instantiate(sData.turret, transform);
//					//rightCannon.GetComponent<Turret>().Setup(t);
//					//rightCannon.transform.localPosition = sData.turretPositions[i];

//					var leftCannon = Instantiate(sData.turret, transform);
//	leftCannon.transform.localPosition = new Vector2(-sData.turretPositions[i].x, sData.turretPositions[i].y);
//	leftCannon.GetComponent<Turret>().Setup(t);
//}
//			}		
//		}

	public void SetControllScheme()
	{
		controlScheme = PlayerPrefs.GetInt("ControlScheme", 0);
		if (ControlScheme == 0)
		{
			finger.SetActive(false);
		}
	}

	private void SetLineRenderer(Color color)
	{
		TrailRenderer tr;

		tr = transform.Find("Trail").GetComponent<TrailRenderer>();
		tr.material = new Material(Shader.Find("Sprites/Default"));

		Gradient gradient = new Gradient();
		gradient.SetKeys(
			new GradientColorKey[] { new GradientColorKey(color, 0.0f), new GradientColorKey(color, 1.0f) },
			new GradientAlphaKey[] { new GradientAlphaKey(1, 0.0f), new GradientAlphaKey(0, 1.0f) }
			);
		tr.colorGradient = gradient;
	}
	#endregion

	public override void Die() //ToDo
	{
		BeatMaster.Instance.PlaySound(sData.deathAudio);
		gameMaster.OnPlayerDeath();
	}

	private void Move()
	{
		if (!buttonClicked)
		{
#if UNITY_STANDALONE
			if (Input.GetMouseButton(0))
			{
				MoveToMouse();
			}
			else
			{
				StayInPlace();
			}
		}
#endif
#if UNITY_ANDROID

			if (Input.touchCount > 0)
			{
				MoveToCameraPosition();
			}
			else
			{
				StayInPlace();
			}
		}
		else
		{

			StayInPlace();
		}
		buttonClicked = false;
#endif
	}

	private void StayInPlace()
	{
#if UNITY_ANDROID
		ButtonPositionSet(false);
#endif
		TimeSlow(sData.timeSlowScale);

		self.velocity = Vector2.Lerp(self.velocity, Camera.main.GetComponent<Rigidbody2D>().velocity, 0.5f);
	}

	private void MoveToMouse()
	{
		TimeReset();

		float x = Input.mousePosition.x;
		float y = Input.mousePosition.y + sData.fingerOffSet.y;
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(x, y, 0));

		Vector3 vectorToTarget = mousePos - (new Vector3(transform.position.x, transform.position.y, 0));

		self.velocity = Vector2.Lerp(self.velocity, vectorToTarget.normalized * sData.speed, 0.8f);
	}

	private void MoveToCameraPosition()  //ToDO - egységesítés
	{
		TimeReset();
#if UNITY_ANDROID
		ButtonPositionSet(true);
#endif
		if (Input.GetTouch(0).phase == TouchPhase.Began)
		{
			if(controlScheme == 0)
			{
				offSet = (Vector2)transform.position - (Vector2)Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
			}
			else if(controlScheme == 1)
			{
				offSet = sData.fingerOffSet;
			}
		}
		else
		{
			Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position) + offSet;

			Vector3 vectorToTarget = touchPos - transform.position;

			self.velocity = Vector2.Lerp(self.velocity, vectorToTarget.normalized * sData.speed, 0.8f);
		}

	}

	private void TimeReset()
	{
#if UNITY_ANDROID
		if(controlScheme == 1)finger.SetActive(false);
#endif
		gameMaster.SetTimeScale(1);
	}

	private void TimeSlow(float speed = 0.3f)
	{
#if UNITY_ANDROID
		if (controlScheme == 1) finger.SetActive(true);
#endif
		gameMaster.SetTimeScale(sData.timeSlowScale);
	}

	public void UseAbility(int index)
	{
		if (Time.timeSinceLevelLoad - coolDowns[index] > sData.abilities[index].coolDown)
		{
			sData.abilities[index].ApplyEffect(gameObject);
			coolDowns[index] = Time.timeSinceLevelLoad;
		}
	}

	void ButtonPositionSet(bool playerMooving)
	{
		if (!playerMooving || buttonClicked)
		{
			foreach (AbilityButton b in buttons)
			{
				b.ShowClickable();
				b.SetPosition((Vector2)transform.position + b.relativePosition);
			}
		}
		else
		{
			foreach (AbilityButton b in buttons)
			{
				b.HideClickable();
			}
		}
	}

	public void OverChargeTurrets(float duration, float speed)
	{
		StartCoroutine(TurretOverCharge(duration, speed));
	}

	public void MainTurretUp()
	{
		transform.Find("MainTurret").GetComponent<Turret>().TurretUp();
	}

	public void SecondaryTurretsUp()
	{
		transform.Find("SecondaryLeft").GetComponent<Turret>().TurretUp();
		transform.Find("SecondaryRight").GetComponent<Turret>().TurretUp();
	}
}
