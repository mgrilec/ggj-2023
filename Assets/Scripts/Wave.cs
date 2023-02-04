using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WaveMonster
{
    public Enemy EnemyPrefab;
    public Portal Portal;
}

[Serializable]
public class Wave
{
    public float Delay;
    public float DelayBetweenSpawns;
    public List<WaveMonster> Spawns = new List<WaveMonster>();
}
