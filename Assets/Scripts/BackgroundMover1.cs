using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMover1 : MonoBehaviour
{
   //[SerializeField] private GameObject[] _backgroundArr;
// private Queue<GameObject> _backgroundQueue = new Queue<GameObject>(3);.
[SerializeField] private List<Transform> _transformList = new List<Transform>();
   private bool _isMoving = false;
   private float _speed = 20f;
    private Vector3 _newPosition;
   
    // Start is called before the first frame update
    void Start()
    {
        
        // for (int i =0; i<3; i++) {
        //     GameObject obj = transform.GetChild(i).gameObject; 
        //     //Debug.Log(obj.Ge);
        // }
       // Debug.Log(transform.childCount());
    //    foreach (Transform child in transform)
    //     {
    //         child.position += Vector3.up * 10.0f;
    //     }
        
    }
    private void MoveBackground(){
         foreach (Transform child in transform) {   
                _newPosition = new Vector3(0,
                                    child.position.y - _speed*Time.deltaTime,
                                    1);
                child.position = _newPosition;
            }
    }

    void UdpateBackgroundQueue(){
       //int x = transform.childCount;
       //Debug.Log(transform.GetChild(5)); 
       _newPosition = new Vector3(0,
                            transform.GetChild(5).transform.position.y +
                            transform.GetChild(5).GetComponent<SpriteRenderer>().bounds.size.y/2,
                            1);
       transform.GetChild(0).position = _newPosition;
       //transform.GetChild(0).transform.position =  transform.GetChild(5).transform.position;
       transform.GetChild(0).SetAsLastSibling();
    }

   void Test(){
    //    float lenght =  transform.GetChild(0).GetComponent<SpriteRenderer>().bounds.size.y;
    //    //Debug.Log(lenght);
    //   // Debug.Log(transform.position.y);
    //    Debug.Log(transform.GetChild(0).transform.position.y + lenght);
   }

    bool ShoudUpdate(){
        return (transform.GetChild(0).transform.position.y + 
                transform.GetChild(0).GetComponent<SpriteRenderer>().bounds.size.y) < 0 ;
    }
    public void UpdateIsMoving(){
      _isMoving = !_isMoving; // return _isMoving;
    }
    // Update is called once per frame
    void Update()
    {
        // if( Input.GetMouseButtonDown(0)) {
        //     _isMoving = !_isMoving;
           
        //     //Test();
        // }
        if(_isMoving) {

            MoveBackground();
        }
        if (!_isMoving){
            if (ShoudUpdate()) UdpateBackgroundQueue();
        }
       
        //if( Input.GetMouseButtonDown(0)) ;
    }
}
