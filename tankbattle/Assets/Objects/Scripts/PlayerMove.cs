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
        key = Input.actions["move"];
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    public void Update()
    {
        if(IsOwner){
            m = key.ReadValue<Vector2>();    
            moveServerRpc(m.x,m.y);
            // Debug.Log(m);
        }
    }
}
