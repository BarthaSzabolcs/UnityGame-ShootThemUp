using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WaveDataHolder : ScriptableObject
{
	[SerializeField] SpawnEnemy[] enemies;
	public float timeBeforeWave;
	public string waveDescription;

	public List<SpawnEnemy> GetSpawnList(int spawnPoints) 
	{
		int spawnValueSum = 0;
		List<SpawnEnemy> spawnArray = new List<SpawnEnemy>();

		int cycleCount = 0;
		while (spawnValueSum <= spawnPoints  && cycleCount < spawnPoints)
		{
			cycleCount++;
			SpawnEnemy choosenOne = ChooseRandomlyByWieght(spawnValueSum);
			if (spawnValueSum + choosenOne.spawnValue <= spawnPoints)
			{
				spawnArray.Add(choosenOne);
				spawnValueSum += choosenOne.spawnValue;
			}
			else
			{
				foreach (SpawnEnemy e in enemies)
				{
					if (spawnValueSum + e.spawnValue <= spawnPoints)
					{
						spawnArray.Add(e);
						spawnValueSum += choosenOne.spawnValue;
						break;
					}
				}
			}
		}
		return spawnArray;
	}

	SpawnEnemy ChooseRandomlyByWieght(int pointsSoFar)
	{
		float weightSum = 0;

		foreach (SpawnEnemy e in enemies)
		{
			if(e.pointBeforeSpawn <= pointsSoFar)
			{
				weightSum += e.spawnWeight;
			}
		}

		float rand = Random.Range(0f, weightSum);

		int i = 0;
		foreach (SpawnEnemy e in enemies)
		{
			if (e.pointBeforeSpawn <= pointsSoFar)
			{
				rand -= e.spawnWeight;

				if (rand < e.spawnWeight)
				{
					break;
				}
				i++;
			}
			else
			{
				i++;
			}
			
		}
		//{
		//	if (enemies[i].pointBeforeSpawn <= pointsSoFar)
		//	{
		//		weightSum += enemies[i].spawnWeight;
		//		rand -= enemies[i].spawnWeight;
		//	}
		//	i++;
		//}

		return enemies[i];
	}
}
