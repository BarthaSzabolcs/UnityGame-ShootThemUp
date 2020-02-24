using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class GameMaster : ScriptableObject
{
	public delegate void SceneLoadEvent();
	public event SceneLoadEvent OnBeforeSceneLoad;

	[SerializeField] float cameraSpeed;
	Camera camera;
	float currentCameraSpeed;
	[SerializeField] GameObject scoreText;
	Text currentScoreText;
	public int currentScore;
	GameObject loseScreen;

	#region keys
	string highScoreKey = "highScore";
	#endregion

	Transform floatingScoresTransform;
	bool gamePaused = false;

	public void ReloadLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
	}

	public void LoadMenu(int index)
	{
		if (OnBeforeSceneLoad != null)
		{
			OnBeforeSceneLoad();
		}
		SceneManager.LoadScene(index);
	}

	public void LoadLevel(int index)
	{
		if (OnBeforeSceneLoad != null)
		{
			OnBeforeSceneLoad();
		}
		SceneManager.LoadScene(index);
	}

	public void PauseGame()
	{
		Time.timeScale = 0;
		Time.fixedDeltaTime = 0.02F * Time.timeScale;
		gamePaused = true;
	}

	public void SetTimeScale(float scale)
	{
		if (!gamePaused)
		{
			Time.timeScale = scale;
			Time.fixedDeltaTime = 0.02F * Time.timeScale;
		}
	}

	public void ContinueGame()
	{
		Time.timeScale = 1;
		Time.fixedDeltaTime = 0.02F * Time.timeScale;
		gamePaused = false;
	}

	public void SwapControllScheme()
	{
		if(PlayerPrefs.GetInt("ControlScheme") == 0)
		{
			PlayerPrefs.SetInt("ControlScheme", 1);
		}else if (PlayerPrefs.GetInt("ControlScheme") == 1)
		{
			PlayerPrefs.SetInt("ControlScheme", 0);
		}

		GameObject player;
		if(player = GameObject.Find("Player"))
		{
			player.GetComponent<PlayerShip>().SetControllScheme();
		}
	}

	public void StartMainCamera()
	{

	}

	public void StartLevel()
	{
		currentScore = 0;
		Debug.Log("Level started");
		loseScreen = GameObject.Find("LoseScreen");
		loseScreen.SetActive(false);
		camera = Camera.main;
		camera.GetComponent<Rigidbody2D>().velocity = Vector2.up * cameraSpeed;
		currentCameraSpeed = cameraSpeed;
		GameObject.Find("MovingBackGround").GetComponent<MovingBackGround>().OnLevelStart();

		floatingScoresTransform = GameObject.Find("FloatingScores").GetComponent<Transform>();
		currentScoreText = GameObject.Find("CurrentScore").GetComponent<Text>();
	}

	public void EndLevel()
	{
		Debug.Log("Level ended");
		Camera.main.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		GameObject.Find("MovingBackGround").GetComponent<MovingBackGround>().OnLevelEnd();
		GameObject.Find("LevelManager").GetComponent<LevelManager>().EndLevel();
	}

	public void ExitGame()
	{
		Application.Quit();
	}

	public void IncreaseCameraSpeed(float ammount)
	{
		Camera.main.GetComponent<Rigidbody2D>().velocity = Vector2.up * cameraSpeed * ammount;
		currentCameraSpeed = cameraSpeed * ammount;
	}

	public void PopScore(int value, Vector3 position)
	{
		var score = Instantiate(scoreText, Vector2.zero, Quaternion.identity, floatingScoresTransform);
		score.GetComponent<FloatingScores>().SetUpScore(value, currentCameraSpeed);
		score.transform.position = position;
		currentScore += value;
		currentScoreText.text = currentScore.ToString();
	}

	public void OnPlayerDeath()
	{
		loseScreen.SetActive(true);
		PauseGame();

		if (currentScore > GetHighScore())
		{
			SaveHighScore(currentScore);
		}

		GameObject.Find("HighScoreText").GetComponent<Text>().text = GetHighScore().ToString();
		GameObject.Find("EndScoreText").GetComponent<Text>().text = currentScore.ToString();
	}

	public void SaveHighScore(int value)
	{
		PlayerPrefs.SetInt(highScoreKey, value);
	}

	public int GetHighScore()
	{
		return PlayerPrefs.GetInt(highScoreKey, 0);
	}
}
