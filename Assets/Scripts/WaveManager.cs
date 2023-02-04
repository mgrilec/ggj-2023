using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    public List<Wave> Waves = new List<Wave>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(StartWaves());
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
        yield return new WaitForSeconds(wave.Delay);

        foreach (var spawn in wave.Spawns)
        {
            Spawn(spawn);
            yield return new WaitForSeconds(wave.DelayBetweenSpawns);
        }
    }

    void Spawn(WaveMonster spawn)
    {
        var instance = Instantiate(spawn.EnemyPrefab.gameObject);
        instance.transform.position = spawn.Portal.transform.position;
    }
}
