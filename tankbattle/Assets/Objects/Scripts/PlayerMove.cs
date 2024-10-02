using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : Move
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
            moveServerRpc(m);
            // Debug.Log(m);
        }
    }

    void Awake(){
        DontDestroyOnLoad(this);
    }
}
