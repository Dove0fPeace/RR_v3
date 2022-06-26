using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject _enemySpawner;
    [SerializeField] private GameObject _scoreCounter;
    [SerializeField] private GameObject _background;
    [SerializeField] private Transform _playerSpawnerPoint;
    [SerializeField] private Animator _animatior;
    
    //[SerializeField] 
    public GameObject DeadBody;
    
    //Rotate
    private bool _isSpinningRight = true;
    float z = 0;
    private float _rSpeed = 30;

    
    private EnemySpawner _enemySpawnerScript;
   // private PlayerScore _playerScoreScript;
    private BackgroundMover1 _backgroundMoverScript;
   
    private EnemyMove _enemyMove;
    private ScoreCounter _scoreCounterScript;
    private Queue<GameObject> _enemyQueue;
    private Vector2 _newPosition;
    //private int _score = 0;

    private bool _canMoving = true;
    private float _speed = 0.5f;
    private int _comboChainCount = 0;

    void Awake() {
            _enemySpawnerScript =  _enemySpawner.GetComponent<EnemySpawner>();
    //    //  _playerScoreScript = _playerScore.GetComponent<PlayerScore>();
            _backgroundMoverScript = _background.GetComponent<BackgroundMover1>();
            _scoreCounterScript = _scoreCounter.GetComponent<ScoreCounter>();
            DeadBody.SetActive(false);
            
    }
    
    private void AdjustPlayerPosition() {
        transform.position = _playerSpawnerPoint.position;
    }
    private void UpdateScrollingBackground() {
       _backgroundMoverScript.UpdateIsMoving();
    }

    private void StartMovingEnemy() {
        _enemySpawnerScript.MoveEnemy();
    }

    private void StopMovingEnemy(GameObject Enemy) {
        _enemyMove = Enemy.GetComponent<EnemyMove>();
        _enemyMove.Stop();
        Enemy.SetActive(false);
    }
    private void Rotate(){
        if (transform.rotation.eulerAngles.z > 30) {
           _isSpinningRight = false;
        } else if (transform.rotation.eulerAngles.z < 20) {
           _isSpinningRight = true;
        }
        if(_isSpinningRight){
            z += Time.deltaTime * _rSpeed;
        }
        else if(!_isSpinningRight) {
            z -= Time.deltaTime * _rSpeed;
        }
        transform.rotation = Quaternion.Euler(0,0,z);
    }


    private void MovePlayerToDeathZone(){
        _newPosition = new Vector2(transform.position.x, 
                                   transform.position.y - _speed*Time.deltaTime);
        transform.position = _newPosition;
    }
    
    private void CountComboChain(float x) {
        if( -0.2f <= x & x <= 0.2f) {
                //Debug.Log("Удар в центр!");
                _comboChainCount ++;   
        } else if(-0.2f > x | x > 0.2f){
            //Debug.Log("Мимо центра!");
            _comboChainCount = 1;
        }
    }
    private void CountPlayerStat(int Combo){
        _scoreCounterScript.IncreaseScore(Combo);
        _scoreCounterScript.IncreaseMoney();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Enemy")) {
            StopMovingEnemy(other.gameObject);
            CountComboChain(other.transform.position.x);
            CountPlayerStat(_comboChainCount);
            _animatior.SetBool("IsAttack",false);
          
            DeadBody.SetActive(true);
            DeadBody.transform.position = other.transform.position;
            
            AdjustPlayerPosition();
            _enemySpawnerScript.CheckSpawning();
            UpdateScrollingBackground();
            _canMoving = true;

        }
    }

    private void Update() {
        MovePlayerToDeathZone();
        if (_canMoving & !PauseMenu.IsGamePaused) {
            if( Input.GetMouseButtonDown(0) & Input.mousePosition.y < 800 ) {
                DeadBody.SetActive(false);
                UpdateScrollingBackground();
                StartMovingEnemy();
                _canMoving = false;
                _animatior.SetBool("IsAttack",true);
            }
        }
    }
}
