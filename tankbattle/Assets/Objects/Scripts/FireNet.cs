using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class FireNet : NetworkBehaviour
{
    public GameObject obj;
    // public GameObject objB;
    public GameObject tar;
    // public GameObject bt; 
    public float speed = 100;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
                
    }

    public void OnFire(InputAction.CallbackContext context){
        if(context.performed&&IsOwner){
            Vector3 pos = tar.transform.position;
            GameObject h = Instantiate (obj,pos,Quaternion.identity);
            NetworkObject f = h.GetComponent<NetworkObject>();
            f.Spawn();
            Rigidbody rig = h.GetComponent<Rigidbody>();
            rig.AddForce( tar.transform.forward*speed,ForceMode.Impulse);   
        }
    }
}
