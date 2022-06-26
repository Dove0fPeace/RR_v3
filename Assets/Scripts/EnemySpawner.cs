using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private EnemyMove _enemyMove;
    Transform EnemyPoolAll; 
    Transform EnemyPoolActive;
    Transform SpawnLines;
    Transform SpawnPoints;
    private void SetRandomSpawnPoints(int EnemyCount) {
        switch (EnemyCount) {
            case 1:
                EnemyPoolActive.GetChild(0).transform.position = 
                SpawnPoints.GetChild(Random.Range(0,6)).transform.position;
                break;
            case 2:
                switch (Random.Range(1,4)){
                    case 1:
                        EnemyPoolActive.GetChild(0).transform.position = 
                        SpawnPoints.GetChild(Random.Range(0,2)).transform.position;
                        //SpawnLines.GetChild(0).transform.position;
                        EnemyPoolActive.GetChild(1).transform.position = 
                        SpawnPoints.GetChild(Random.Range(2,4)).transform.position;
                        //SpawnLines.GetChild(1).transform.position;
                        break;
                    case 2:
                        EnemyPoolActive.GetChild(0).transform.position = 
                        SpawnPoints.GetChild(Random.Range(0,2)).transform.position;
                        //SpawnLines.GetChild(0).transform.position;
                        EnemyPoolActive.GetChild(1).transform.position = 
                        SpawnPoints.GetChild(Random.Range(4,6)).transform.position;
                        //SpawnLines.GetChild(2).transform.position;
                        break;
                    case 3:
                        EnemyPoolActive.GetChild(0).transform.position = 
                        SpawnPoints.GetChild(Random.Range(2,4)).transform.position;
                        //SpawnLines.GetChild(1).transform.position;
                        EnemyPoolActive.GetChild(1).transform.position = 
                        SpawnPoints.GetChild(Random.Range(4,6)).transform.position;
                        //SpawnLines.GetChild(2).transform.position;
                        break;
                }
                break;
            case 3:
                for (int i = 0; i <3; i++) {
                    EnemyPoolActive.GetChild(i).transform.position = 
                    SpawnPoints.GetChild(i*2 + Random.Range(0,2)).transform.position;
                    //SpawnLines.GetChild(i).transform.position;
                }
                break;
        }
    }

    private void ActivateEnemy(){
        foreach (Transform Enemy in EnemyPoolActive) {
            Enemy.gameObject.SetActive(true);
        }
    }
    private void SpawnEnemies(int EnemyCount) {
        switch (EnemyCount) {
            case 1:
                MoveToActivePool(1);
                SetRandomSpawnPoints(1);
                break;
            case 2:
                MoveToActivePool(2);
                SetRandomSpawnPoints(2);
                break;
            case 3:
                MoveToActivePool(3);
                SetRandomSpawnPoints(3);
                break;
        }
        UpdateEnemySpeed();
        SetEnemyStartDirection();
        ActivateEnemy();
    }
    private void UpdateEnemySpeed(){
        foreach (Transform Enemy in EnemyPoolActive ) {
        _enemyMove = Enemy.GetComponent<EnemyMove>();
        _enemyMove.UpdateSpeed();
        }
        
    }
    private void SetEnemyStartDirection(){
        foreach (Transform Enemy in EnemyPoolActive ) {
        _enemyMove = Enemy.GetComponent<EnemyMove>();
        _enemyMove.SetStartDirection();
        }
        
    }
    public void MoveEnemy(){
        _enemyMove = EnemyPoolActive.GetChild(EnemyPoolActive.childCount -1).GetComponent<EnemyMove>();
        _enemyMove.Dash();
        MoveToAllPool();
    }
       
    void Awake(){
        EnemyPoolAll = transform.GetChild(0);
        EnemyPoolActive = transform.GetChild(1);
        //SpawnLines = transform.GetChild(2);
        SpawnPoints = transform.GetChild(3);
    }

    void Start() {
        SpawnEnemies(1);
    }

    public void CheckSpawning(){
        //if (EnemyPoolActive.childCount == 0) SpawnEnemies(2);
        if (EnemyPoolActive.childCount == 0) SpawnEnemies(Random.Range(1,4));
    }

    private void MoveToActivePool(int EnemyCount){
        for (int i = 0; i<EnemyCount;i++) EnemyPoolAll.GetChild(EnemyPoolAll.childCount -1).SetParent(EnemyPoolActive);
    }

    private void MoveToAllPool() {
        EnemyPoolActive.GetChild(EnemyPoolActive.childCount -1).SetParent(EnemyPoolAll);
    }  
}
