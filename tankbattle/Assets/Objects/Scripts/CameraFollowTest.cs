using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class CameraFollowTest : MonoBehaviour
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
        // }
    }
    void Awake(){
        cam.transform.position = this.transform.position + Vector3.back*3.5f + Vector3.up;    
    }
}
