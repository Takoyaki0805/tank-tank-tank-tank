using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class FireNet : NetworkBehaviour
{
    public GameObject obj;
    public GameObject tar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
                
    }

    public void OnFire(InputAction.CallbackContext context){
        Vector3 pos = tar.transform.position;
        if(context.performed){
            Instantiate (obj,pos,Quaternion.identity);
        }
    }
}
