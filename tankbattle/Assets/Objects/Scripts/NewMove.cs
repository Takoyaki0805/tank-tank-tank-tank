using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewMove : Move
{
    public GameObject obj;
    Vector2 m;
    InputAction key;

    // Start is called before the first frame update
    void Start()
    {
        var Input = obj.GetComponent<PlayerInput>();
        // if(IsOwner){
        key = Input.actions["move"];
        // }
    }

    // Update is called once per frame
    public void Update()
    {
        m = key.ReadValue<Vector2>();
        if(IsOwner){
            // moveServerRpc(m);
            Rig.linearVelocity = new Vector3(m.x*speed,0f,m.y*speed);
            // Debug.Log(m);
        }
    }

    void Awake(){
        DontDestroyOnLoad(this);
    }
}