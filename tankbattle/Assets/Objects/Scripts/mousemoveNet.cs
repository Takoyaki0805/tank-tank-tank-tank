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
    void FixedUpdate()
    {
        float h = Input.GetAxis("Mouse X");
        // Debug.Log(h);
        if(h>=3f){
            h=3f;
        }
        if(h<=-3f){
            h=-3f;
        }
        
        if(IsOwner){
            // atk.transform.RotateAround (tar.transform.position, Vector3.up, h*Time.deltaTime*speed);
            cam.transform.RotateAround (tar.transform.position, Vector3.up, h*Time.deltaTime*speed);
            // cam.transform.position = atk.transform.position + Vector3.back*2.8f + Vector3.up*2f;
            aroundServerRpc(h);
            // turnServerRpc(atk.transform.position,atk.transform.localEulerAngles);
            // cam.transform.position = atk.transform.position + Vector3.back*3.5f + Vector3.up*2f;
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

    [Unity.Netcode.ServerRpc]
    void turnServerRpc(Vector3 p,Vector3 l){
        // atk.transform.RotateAround (tar.transform.position, Vector3.up, h*Time.deltaTime*speed*1000);
        atk.transform.position = p;
        atk.transform.localEulerAngles = l;
        return;
    }
}
