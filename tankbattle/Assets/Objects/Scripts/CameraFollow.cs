using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class CameraFollow : NetworkBehaviour
{
    public GameObject cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(cam == null){
            cam = GameObject.FindWithTag("MainCamera");
        }
        // if(IsOwner){
        //     cam.transform.position = this.transform.position + Vector3.back*3.5f + Vector3.up;
        // }

    }
    void Awake()
    {
        if(IsOwner){
            cam.transform.position = this.transform.position + Vector3.back*3.5f + Vector3.up;    
        }
    }
}
