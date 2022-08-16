using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _SpawnPoints;

    [SerializeField] private Enemy[] _Enemies;

    [SerializeField] private float m_SpawnDelay;

    private bool _firstSpawn;

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
        int numberOfEnemies = Random.Range(1, 3);

        if (_firstSpawn)
        {
            numberOfEnemies = 1;
            _firstSpawn = false;
        }

        StartCoroutine(Spawn(numberOfEnemies));
    }
    
    private IEnumerator Spawn(int count)
    {
        yield return new WaitForSeconds(m_SpawnDelay);
        
        var enemy = Instantiate(_Enemies[Random.Range(0, _Enemies.Length)],
            _SpawnPoints[Random.Range(0, _SpawnPoints.Length)].transform.position, Quaternion.Euler(0,0,0));

        enemy.MovingLine = EnemyMovingLines.Instance.GetMovingLine();
    }
}
