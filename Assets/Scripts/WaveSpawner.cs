using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Transform StartNode;
    public Transform EnemyPrefab;
    public Text WaveCountDownText;
    public float TimeBetweenWaves = 4f;
    public float TimeBetweenEnemySpawns = 0.8f;

    private float countDown;
    private int currentWave;
    private bool isWaveInProgress;

    private void Update()
    {

        if (!isWaveInProgress)
        {
            if (countDown <= 0)
                StartCoroutine(SpawnWave());
            else
            {
                countDown -= Time.deltaTime;
                WaveCountDownText.text = Mathf.Max(Mathf.CeilToInt(countDown),1).ToString();
            }
        }
        
    }

    private IEnumerator SpawnWave()
    {
        isWaveInProgress = true;
        currentWave++;
        countDown = TimeBetweenWaves;
        WaveCountDownText.CrossFadeAlpha(0f, 0.2f, true);
        for (int i = 0; i < currentWave; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(TimeBetweenEnemySpawns);
        }
        isWaveInProgress = false;
        WaveCountDownText.CrossFadeAlpha(1f, 0.2f, true);
    }

    private void SpawnEnemy()
    {
        Instantiate(EnemyPrefab, StartNode.position + new Vector3(0, 1, 0), StartNode.rotation);
    }

}
