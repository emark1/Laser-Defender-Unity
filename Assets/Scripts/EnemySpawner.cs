using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfig;
    int startingWave = 0;
    // Start is called before the first frame update
    void Start()
    {
        var currentWave = waveConfig[startingWave];
        StartCoroutine(SpawnAllEnemies(currentWave));
    }

    private IEnumerator SpawnAllEnemies(WaveConfig waveConfig) {
        Instantiate(
            waveConfig.GetEnemyPrefab(),
            waveConfig.GetWaypoints()[0].transform.position,
            Quaternion.identity
        );
        yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
    }
}
