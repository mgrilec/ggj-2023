using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    public List<Wave> Waves = new List<Wave>();

    [HideInInspector]
    public float NextWaveInTime;
    [HideInInspector]
    public bool WaitingForNextWave;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(StartWaves());
    }

    private void Update()
    {
        NextWaveInTime -= Time.deltaTime;
    }

    private IEnumerator StartWaves()
    {
        foreach (var wave in Waves)
        {
            yield return StartWave(wave);
        }
    }

    private IEnumerator StartWave(Wave wave)
    {
        // starting delay
        WaitingForNextWave = true;
        NextWaveInTime = wave.Delay;
        yield return new WaitForSeconds(wave.Delay);
        WaitingForNextWave = false;

        foreach (var spawn in wave.Spawns)
        {
            Spawn(spawn);
            yield return new WaitForSeconds(wave.DelayBetweenSpawns);
        }
    }

    public void StartExtraWave(Wave wave)
    {
        StartCoroutine(_StartExtraWave(wave));
    }

    private IEnumerator _StartExtraWave(Wave wave)
    {
        // starting delay
        yield return new WaitForSeconds(wave.Delay);

        foreach (var spawn in wave.Spawns)
        {
            Spawn(spawn);
            yield return new WaitForSeconds(wave.DelayBetweenSpawns);
        }
    }

    void Spawn(WaveMonster spawn)
    {
        var instance = Instantiate(spawn.EnemyPrefab.gameObject, spawn.Portal.transform.position, Quaternion.identity);
    }
}
