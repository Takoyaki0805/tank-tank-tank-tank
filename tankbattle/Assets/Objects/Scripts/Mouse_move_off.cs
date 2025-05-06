using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;

public class mousemove : MonoBehaviour
{
    public GameObject target;
    public GameObject cam;
    public GameObject attack;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        // if(IsOwner){
            cam = GameObject.FindWithTag("MainCamera");
            SceneManager.sceneLoaded += OnLoaded;
            cam.transform.position = attack.transform.position + Vector3.back*3.5f +Vector3.up*1.5f;
            cam.transform.parent = target.transform;
        // }
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
        
        // if(IsOwner){
            attack.transform.RotateAround (target.transform.position, Vector3.up, h*Time.deltaTime*speed);
            cam.transform.RotateAround (target.transform.position, Vector3.up, h*Time.deltaTime*speed);
            // cam.transform.position = atk.transform.position + Vector3.back*2.8f + Vector3.up*2f;
            // aroundServerRpc(h);
            // turnServerRpc(atk.transform.position,atk.transform.localEulerAngles);
            // cam.transform.position = atk.transform.position + Vector3.back*3.5f + Vector3.up*2f;
        // }

    }

    void OnLoaded(Scene s,LoadSceneMode m){
        cam = GameObject.FindWithTag("MainCamera");        
    }

    void Awake(){
    }


}
