using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        if(IsOwner){
            cam = GameObject.FindWithTag("MainCamera");
            SceneManager.sceneLoaded += OnLoaded;
            cam.transform.position = atk.transform.position + Vector3.back*3.5f +Vector3.up*2f;
            cam.transform.parent = tar.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if(IsOwner){
            float h = Input.GetAxis("Mouse X");
            // atk.transform.RotateAround (tar.transform.position, Vector3.up, h*Time.deltaTime*speed);
            cam.transform.RotateAround (tar.transform.position, Vector3.up, h*Time.deltaTime*speed);
            // cam.transform.position = atk.transform.position + Vector3.back*2.8f + Vector3.up*2f;
            aroundServerRpc(h);
            // cam.transform.position = atk.transform.position + Vector3.back*3.5f +Vector3.up*2f;

        }

    }

    void OnLoaded(Scene s,LoadSceneMode m){
        cam = GameObject.FindWithTag("MainCamera");        
    }

    void Awake(){
    }

    [Unity.Netcode.ServerRpc]
    void aroundServerRpc(float h){
            atk.transform.RotateAround (tar.transform.position, Vector3.up, h*Time.deltaTime*speed);
        return;
    }
}
