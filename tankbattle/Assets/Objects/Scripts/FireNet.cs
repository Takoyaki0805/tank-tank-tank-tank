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
    public GameObject g; 
    public float speed = 100;
    public GameObject mine;

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
            if(IsHost){
                spawn(pos);
            }else{
                SpawnRpc(pos);
            }
        }
    }

    public void Setmine(InputAction.CallbackContext context){
        if(context.performed&&IsOwner){
            Vector3 pos = tar.transform.position;
            if(IsHost){
                minespawn(pos);
            }else{
                mineSpawnRpc(pos);
            }
        }
    }

    public void spawn(Vector3 pos){
        GameObject h = Instantiate (obj,pos,Quaternion.identity);
        NetworkObject f = h.GetComponent<NetworkObject>();
        h.GetComponent<NetworkObject>().Spawn();
        Rigidbody rig = h.GetComponent<Rigidbody>();
        h.transform.eulerAngles = g.transform.eulerAngles;
        rig.AddForce( tar.transform.forward*speed,ForceMode.Impulse);   
    }

    public void minespawn(Vector3 pos){
        GameObject h = Instantiate (mine,pos,Quaternion.identity);
        NetworkObject f = h.GetComponent<NetworkObject>();
        h.GetComponent<NetworkObject>().Spawn();
    }

    [Rpc(SendTo.Server)]
    public void SpawnRpc(Vector3 pos){
        spawn(pos);
    }
    
    [Rpc(SendTo.Server)]
    public void mineSpawnRpc(Vector3 pos){
        minespawn(pos);
    }
}
