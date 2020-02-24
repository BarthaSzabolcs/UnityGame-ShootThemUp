using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndlessWaveSpawner : MonoBehaviour
{
	[SerializeField] GameMaster gameMaster;
	[SerializeField] WaveDataHolder [] enemyWaves;
	[SerializeField] int maxPowerLevel = 0;
	[SerializeField] float difficultyTimerBase;
	[SerializeField] float textFadeTime;
	[SerializeField] string newWaveIncoming;
	[SerializeField] string newWaveSpawned;
	[SerializeField] int[] spawnPoints;
	[SerializeField] GameObject[] powerUps;

	int powerLevel = 0;
	float difficultyTimer;
	Text waveSpawnerText;

	void Start()
	{
		waveSpawnerText = GameObject.Find("LevelManagerText").GetComponent<Text>();
		waveSpawnerText.CrossFadeAlpha(0f, 0, false);
		StartCoroutine(SpawnWave());
	}

	IEnumerator SpawnWave()
	{
		gameMaster.StartLevel();
		while (true)
		{
			if (transform.childCount < 1)
			{
				Instantiate(powerUps[Random.Range(0, powerUps.Length)], transform.position, Quaternion.identity);

				int rand = Random.Range(0, enemyWaves.Length);

				BeatMaster.Instance.PlaySound(newWaveIncoming);

				waveSpawnerText.text = enemyWaves[rand].waveDescription;
				waveSpawnerText.CrossFadeAlpha(1f, textFadeTime, false);

				yield return new WaitForSeconds(enemyWaves[rand].timeBeforeWave);
				BeatMaster.Instance.PlaySound(newWaveSpawned);

				waveSpawnerText.CrossFadeAlpha(0f, textFadeTime, false);

				List<SpawnEnemy> spawnArray = enemyWaves[rand].GetSpawnList(spawnPoints[powerLevel]);

				foreach (SpawnEnemy e in spawnArray)
				{
					e.SpawnInstance(transform);
					yield return new WaitForSeconds(e.spawnDelay);

				}

				if (powerLevel < maxPowerLevel && Time.timeSinceLevelLoad - difficultyTimer > difficultyTimerBase * (powerLevel + 1))
				{
					difficultyTimer = Time.timeSinceLevelLoad;
					powerLevel++;
					gameMaster.IncreaseCameraSpeed(powerLevel*0.5f);
				}
			}
			else
			{
				yield return new WaitForEndOfFrame();
			}
		}
	}

}
