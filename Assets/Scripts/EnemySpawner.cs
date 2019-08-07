using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int waveIndex = 0;
    [SerializeField] bool loopingWaves = 0;

    // Start is called before the first frame update
    IEnumerable Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (loopingWaves);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator SpawnAllWaves()
    {                
        foreach (WaveConfig child in waveConfigs)
        {
            yield return StartCoroutine(SpawnAllEnemiesInWave(child));            
        }        
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {        
        for (int i = 0; i < waveConfig.GetNumberOfEnemies(); i++) {
            var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWaypoints()[0].transform.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);

            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }    
}
