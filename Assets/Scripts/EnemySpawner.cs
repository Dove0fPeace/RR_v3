using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _SpawnPoints;

    [SerializeField] private Enemy[] _Enemies;

    [SerializeField] private float m_SpawnDelay;

    [SerializeField] private bool m_SingleSpawn;

    private SpawnMode m_SpawnMode;

    private bool _firstSpawn = true;

    private void Start()
    {
        m_SpawnMode = (SpawnMode)PlayerPrefs.GetInt("SpawnMode");
        InvokeSpawn();
    }

    private void Update()
    {
        if(Enemy.Enemies.Count == 0)
        {
            InvokeSpawn();
        }
    }
    private void InvokeSpawn()
    {
        int numberOfEnemies = 1;
        switch (m_SpawnMode)
        {
            case SpawnMode.Single:
                numberOfEnemies = 1;
                break;
            case SpawnMode.Multiply:
                numberOfEnemies = Random.Range(1, 3);
                break;
        }

        if (_firstSpawn)
        {
            numberOfEnemies = 1;
            _firstSpawn = false;
        }

        var lines = EnemyMovingLinesGenerator.Instance.GetRandomMovingLines(numberOfEnemies);
        for (int i = 0; i < lines.Length; i++)
        {
            var enemy = Instantiate(_Enemies[Random.Range(0, _Enemies.Length)],
                _SpawnPoints[Random.Range(0, _SpawnPoints.Length)].transform.position, Quaternion.Euler(0, 0, 0));

            enemy.MovingLine = lines[i];
        }
    }
}
