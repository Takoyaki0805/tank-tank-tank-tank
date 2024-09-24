using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class mousemoveNet : NetworkBehaviour
{
    public GameObject tar;
    public GameObject cam;
    public GameObject atk;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Mouse X");
        if(IsOwner){
            atk.transform.RotateAround (tar.transform.position, Vector3.up, h*Time.deltaTime*speed);
            cam.transform.RotateAround (tar.transform.position, Vector3.up, h*Time.deltaTime*speed);
            aroundServerRpc(h);
        }
    }

    void Awake(){
        cam = GameObject.FindWithTag("MainCamera");
    }

    [Unity.Netcode.ServerRpc]
    void aroundServerRpc(float h){
        atk.transform.RotateAround (tar.transform.position, Vector3.up, h*Time.deltaTime*speed);
        return;
    }
}
