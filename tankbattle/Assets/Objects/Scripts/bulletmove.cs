using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;


public class bulletmove : NetworkBehaviour
{
    public GameObject tar; 
    // public GameObject bt; 
    public Rigidbody rig;
    public float speed = 50;
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
    }
    public override void OnNetworkSpawn()
    {
        // //ホストの場合
        // if (IsHost)
        // {
        //     rig.AddForce( tar.transform.forward*speed,ForceMode.Impulse);  
        // }
    }

}
