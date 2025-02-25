using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;


public class NewMove : Move
{
    public GameObject obj;
    Vector2 m;
    InputAction key;
    public GameObject fuc;
    public GameObject spawnpos;
    bool decidespawn = false;
    public bool ablemove = false;
    public Material red;
    public Material blue;
    GameObject mng;
    GameObject cam;
    bool onetime = true;

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
        if(IsOwner&&onetime){
        if(IsHost){
            setspawnRpc(spawnpos.transform.position);
        }
            
        this.transform.position = spawnpos.transform.position;
        
        onetime = false;
        }else if(IsOwner){
            mng.GetComponent<SpawnMangement>().spawnout(spawnpos);
        }

        // if(IsOwner&&spawnpos.GetComponent<flagcolor>().flagred){
        // //    mng.GetComponent<scoreboard>().isred = true;
        //     cam.GetComponent<color>().isred = true;
        //     // Debug.Log("aaaaaaa"); 
        // }
        // if(IsOwner&&spawnpos.GetComponent<flagcolor>().flagblue){
        // //    mng.GetComponent<scoreboard>().isblue = true; 
        //     cam.GetComponent<color>().isblue = true; 
        // }

        m = key.ReadValue<Vector2>();
        if(IsOwner&&ablemove){
            // moveServerRpc(m);
            // Rig.linearVelocity = new Vector3(m.x*speed,0f,m.y*speed);
            if(m.x!=0){
                Rig.linearVelocity = tar.transform.forward*Math.Abs(m.x)*speed;
                tar.transform.eulerAngles += new Vector3(0,m.x*speed*50*Time.deltaTime,0);
                // Debug.Log("a");
            // Debug.Log(Time.deltaTime);
            }else{
                Rig.linearVelocity = tar.transform.forward*m.y*speed;
                // Debug.Log("i");
            }
        }

        
    }

    void Awake(){
        DontDestroyOnLoad(this);
    }

    public override void OnNetworkSpawn(){
        Debug.Log(mng);
        if(!mng == null){
        mng.GetComponent<SpawnMangement>().spawnout(spawnpos);
            Debug.Log("eeeeeeeeeeeeeeeeeeee");

        }


        // if(IsOwner){
        mng = GameObject.FindGameObjectWithTag("spawnMNG");
        // mng.GetComponent<ReadySet>().boot();
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        // spawnpos = mng.GetComponent<SpawnMangement>().playerattach();
        if(spawnpos != null){
            mng.GetComponent<SpawnMangement>().spawnout(spawnpos);
            Debug.Log("aaaaaaaaaaaaaaaaaa");
        }

        
        if(IsOwner){
        // Debug.Log(spawnpos);
        if(IsHost){
            spawnpos = mng.GetComponent<SpawnMangement>().playerattach();
        }else{
            pointdecRpc();
        }

        if(IsHost){
        }

        if(!IsHost){
            // setspawn(spawnpos.transform.position);
        }else{
            setspawnRpc(spawnpos.transform.position);
        }
            Debug.Log(spawnpos.transform.position);

        // if(IsHost){

        // }
        // if(IsOwnedByServer){
        mng.GetComponent<ReadySet>().boot();

        // if(IsOwner){
        cam.GetComponent<team>().setpos(spawnpos);
        cam.GetComponent<team>().setteam();
        }
        // }/
        // Debug.Log(IsOwner);

        // GameObject child = spawnpos.transform.Find("flag").gameObject;
        // Debug.Log();
        // Debug.Log("ああああ");
        // Debug.Log(red);
    }

        // }
    void setspawn(Vector3 p){
        this.gameObject.transform.position = p;
    }

    [Rpc(SendTo.Everyone)]
    public void setspawnRpc(Vector3 p){
        if(IsOwner){
            setspawn(p);
        }
    }

    [Rpc(SendTo.Server)]
    public void pointdecRpc(){
        spawnpos = mng.GetComponent<SpawnMangement>().playerattach();
    }

    [Rpc(SendTo.Everyone)]
    public void pointupRpc(){
        if(IsOwner){
            // spawnpos = sObj;
        }
    }

    

}