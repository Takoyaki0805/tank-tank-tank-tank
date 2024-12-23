using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
        if(IsOwner&&spawnpos.GetComponent<flagcolor>().flagred){
        //    mng.GetComponent<scoreboard>().isred = true;
            cam.GetComponent<color>().isred = true;
            // Debug.Log("aaaaaaa"); 
        }
        if(IsOwner&&spawnpos.GetComponent<flagcolor>().flagblue){
        //    mng.GetComponent<scoreboard>().isblue = true; 
            cam.GetComponent<color>().isblue = true; 
        }
        if(!decidespawn){
            // if(mng == null){}else{

                decidespawn = true;
            // }
        }
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
        mng = GameObject.FindGameObjectWithTag("spawnMNG");
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        spawnpos = mng.GetComponent<SpawnMangement>().playerattach();
        this.gameObject.transform.position = spawnpos.transform.position;
        // GameObject child = spawnpos.transform.Find("flag").gameObject;
        // Debug.Log();
        // Debug.Log("ああああ");
        Debug.Log(red);

    }

    

}