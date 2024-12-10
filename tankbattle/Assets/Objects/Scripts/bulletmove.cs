using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;


public class bulletmove : NetworkBehaviour
{
    public int atk = 30;
    // public GameObject tar; 
    // public GameObject bt; 
    // public Rigidbody rig;
    // public float speed = 50;
    public float timelimit = 120.0f;
    public int bounce = 2;
    int count = 0;
    float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        // rig = tar.GetComponent<Rigidbody>();
        // foreach(GameObject bt in GameObject.FindGameObjectsWithTag("playerbullet")){
        //     if(bt.IsOwner){
        //         this.transform.localEulerAngles = bt.transform.localEulerAngles;
        //         rig.AddForce( bt.transform.forward*speed,ForceMode.Impulse);    
        //     }
        // }
        // Debug.Log(bt.transform.localEulerAngles);

    }

    // Update is called once per frame
    void Update()
    {
        if(timer >= timelimit || count>bounce){
            // Destroy(this.gameObject);
            if(IsHost){
                deletespawn();
                
            }else{
                desSpawnRpc();
            }
        }
        timer += Time.deltaTime;
        // Debug.Log(timer);
    }
    public override void OnNetworkSpawn()
    {

        // //ホストの場合
        // if (IsHost)
        // {
        //     rig.AddForce( tar.transform.forward*speed,ForceMode.Impulse);  
        // }
    }

    void deletespawn(){
        this.gameObject.GetComponent<NetworkObject>().Despawn();
    }

    void OnCollisionEnter(Collision c){
        // if(c.gameObject.tag=="wall"){
        // Rigidbody rig = this.gameObject.GetComponent<Rigidbody>();
        // rig.linearVelocity = Vector3.Reflect(rig.linearVelocity,c.GetContact(0).normal);
        // rig.AddForce( this.transform.forward*8f,ForceMode.Impulse);   
        // Debug.Log(rig.linearVelocity);
        // count++;
        // }
        // if(c.gameObject.tag=="ball"){
            if(IsHost){
                deletespawn();
            }else{
                desSpawnRpc();
            }
        // }
    }

    [Rpc(SendTo.Server)]
    public void desSpawnRpc(){
        deletespawn();
    }

}
