using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Move : NetworkBehaviour
{
    public float speed = 1.0f;
    public Rigidbody Rig;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
                
    }

    [Unity.Netcode.ServerRpc]
    public void moveServerRpc(float x,float y){
        Rig.velocity = new Vector3(x*speed,0f,y*speed);
        return;
    }
}
