using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Player_move : NetworkBehaviour
{
    public float speed = 1.0f;
    public Rigidbody Rig;
    public GameObject tar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
                
    }

    [Unity.Netcode.ServerRpc]
    public void moveServerRpc(Vector2 m){
        Rig.linearVelocity = new Vector3(m.x*speed,0f,m.y*speed);
        return;
    }
}
