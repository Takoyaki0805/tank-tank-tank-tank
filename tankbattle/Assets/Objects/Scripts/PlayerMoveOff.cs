using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveOff : MonoBehaviour
{
    public GameObject obj;
    Vector2 m;
    InputAction key;
     
    public float speed = 1.0f;
    public Rigidbody Rig;
    public GameObject tar;

    // Start is called before the first frame update
    void Start()
    {
        var Input = obj.GetComponent<PlayerInput>();
        // if(IsOwner){
        key = Input.actions["move"];
        DontDestroyOnLoad(this);
        // }
    }

    // Update is called once per frame
    public void Update()
    {
        m = key.ReadValue<Vector2>();    
        // if(IsOwner){
            move(m);
            // Debug.Log(m);
        // }
    }

    public void move(Vector2 m){
        Rig.linearVelocity = new Vector3(m.x*speed,0f,m.y*speed);
        return;
    }

    // void Awake(){
    //     DontDestroyOnLoad(this);
    // }
}
