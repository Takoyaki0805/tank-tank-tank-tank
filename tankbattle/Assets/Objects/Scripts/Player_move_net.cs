using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;


public class NewMove : Move
{
    public GameObject obj;
    Vector2 move_value;
    InputAction key;
    public GameObject fuc;
    public GameObject spawnpos;
    bool decidespawn = false;
    public bool ablemove = false;
    public Material red;
    public Material blue;
    GameObject mng;
    GameObject cam;

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


        // if(IsOwner&&spawnpos.GetComponent<flagcolor>().flagred){
        // //    mng.GetComponent<scoreboard>().isred = true;
        //     cam.GetComponent<color>().isred = true;
        //     // Debug.Log("aaaaaaa"); 
        // }
        // if(IsOwner&&spawnpos.GetComponent<flagcolor>().flagblue){
        // //    mng.GetComponent<scoreboard>().isblue = true; 
        //     cam.GetComponent<color>().isblue = true; 
        // }

        move_value = key.ReadValue<Vector2>();
        if(IsOwner&&ablemove){
            // moveServerRpc(move_value);
            // Rig.linearVelocity = new Vector3(move_value.x*speed,0f,move_value.y*speed);
            if(move_value.x!=0){
                Rig.linearVelocity = tar.transform.forward*Math.Abs(move_value.x)*speed;
                tar.transform.eulerAngles += new Vector3(0,move_value.x*speed*50*Time.deltaTime,0);
                // Debug.Log("a");
            // Debug.Log(Time.deltaTime);
            }else{
                Rig.linearVelocity = tar.transform.forward*move_value.y*speed;
                // Debug.Log("i");
            }
        }

        
    }

    void Awake(){
        DontDestroyOnLoad(this);
    }

    public override void OnNetworkSpawn(){
    }

}