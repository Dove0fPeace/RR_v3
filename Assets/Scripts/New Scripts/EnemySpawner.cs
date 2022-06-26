using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _SpawnPoints;

    [SerializeField] private GameObject[] _Enemies;

    private void Start()
    {
        Player.Instance.EnemyKilled += SpawnInwoke;
        SpawnInwoke();
    }

    private void OnDestroy()
    {
        Player.Instance.EnemyKilled -= SpawnInwoke;
    }
    
    private void SpawnInwoke()
    {
        StartCoroutine(Spawn());
    }
    
    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(0.5f);
        
        Instantiate(_Enemies[Random.Range(0, _Enemies.Length)],
            _SpawnPoints[Random.Range(0, _SpawnPoints.Length)].transform.position, Quaternion.Euler(0,0,0));
    }
}
