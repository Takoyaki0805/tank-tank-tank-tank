using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;


public class Player_move_net : Player_move
{
    public GameObject player_target;
    Vector2 move_value;
    InputAction key;
    public GameObject player_direction;
    public GameObject spawn_position;
    bool IsDecideSpawn = false;
    public bool IsMove = false;
    public Material red_material;
    public Material blue_material;
    GameObject manager;
    GameObject cam;

    // Start is called before the first frame update
    void Start()
    {
        var Input = player_target.GetComponent<PlayerInput>();
        // if(IsOwner){
        key = Input.actions["move"];
        // }
    }

    // Update is called once per frame
    public void Update()
    {
        //プレイヤーの操作を入力する
        move_value = key.ReadValue<Vector2>();
        if(IsOwner&&IsMove){
            if(move_value.x!=0){
                Rig.linearVelocity = tar.transform.forward*Math.Abs(move_value.x)*speed;
                tar.transform.eulerAngles += new Vector3(0,move_value.x*speed*50*Time.deltaTime,0);
            }else{
                Rig.linearVelocity = tar.transform.forward*move_value.y*speed;
            }
        }
    }

    void Awake(){
        DontDestroyOnLoad(this);
    }

    public override void OnNetworkSpawn(){
    }

}