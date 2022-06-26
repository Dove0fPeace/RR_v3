using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private Vector2 _newPosition;


    private float _speedX = 0;
    private float _uSpeedX = 0;
    private float _uSpeedY = 20f;
    private float _speedY = 0;
    private float _rSpeed = 30;
    private bool _isDash = false;
    private bool _isMovingRight = false;
    private bool _isSpinningRight = true;

    float z = 210; // для вращения типо угол вращения

    public void SetStartDirection() {
        if (transform.position.x < 0) {
            _isMovingRight = true;
            _speedX = _uSpeedX;
        }
        else {
            _isMovingRight = false;
            _speedX = -_uSpeedX;
        };
    }
    public void UpdateSpeed() {
        SetRandomSpeed(5f,8f);
    }

   private bool IsBorderRight(float x) {
       return x>0;
   }

     private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Border") {
            if (IsBorderRight(other.transform.position.x)) {
                if(_isMovingRight){
                    _speedX  = -_speedX;
                    _isMovingRight = false;
                }
            }
            else {
                 if(!_isMovingRight){
                    _speedX  = -_speedX;
                    _isMovingRight = true;
                }
            }
        }
    }

 

    public void SetRandomSpeed(float min, float max) {
        _uSpeedX = Random.Range(min,max);
    }
    
    public void Dash() {
        _isDash = true;
    }

    public void Stop(){
        _isDash = false;
        _speedX = _uSpeedX;
        _speedY = 0;
    }

    private void Move() {
        _newPosition = new Vector2(transform.position.x + _speedX*Time.deltaTime, 
                                   transform.position.y - _speedY*Time.deltaTime);
        transform.position = _newPosition;
    }

    private void Rotate(){
        if (transform.rotation.eulerAngles.z > 220) {
           _isSpinningRight = false;
        } else if (transform.rotation.eulerAngles.z < 200) {
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
    void Update()
    {
        Move();
        Rotate();
        if (_isDash) {
            _speedX = 0;
            _speedY = _uSpeedY;
        }
    }
}
