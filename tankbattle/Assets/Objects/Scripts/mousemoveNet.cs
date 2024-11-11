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
    // float movep; 
    float deltaCA;
        private NetworkVariable<float> movep = new NetworkVariable<float>(
        0f,                                          // 初期値
        NetworkVariableReadPermission.Everyone,     // 読み取り権限
        NetworkVariableWritePermission.Owner        // 書き込み権限
        );
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
        float mh = Input.GetAxis("Mouse X");
        float h = 0;
        // Debug.Log(h);
        if(mh>0){
            h=3f;
        }
        if(mh<0){
            h=-3f;
        }
        if(mh<0.1&&mh>-0.1){
            h=0;
        }else{
            h=mh;
        }
        Debug.Log("h="+h+"mh="+mh);
        
        // movep = h*Time.deltaTime*speed;
        if(IsOwner){

            movep.Value = h*Time.deltaTime*speed;
            // atk.transform.RotateAround (tar.transform.position, Vector3.up, h*Time.deltaTime*speed);
            // cam.transform.position = atk.transform.position + Vector3.back*2.8f + Vector3.up*2f;
            cam.transform.RotateAround (tar.transform.position, Vector3.up, movep.Value);
            atk.transform.RotateAround (tar.transform.position, Vector3.up, movep.Value);

            // aroundServerRpc(h);
            // if(cam.transform.localEulerAngles.z!=atk.transform.localEulerAngles.z){
            //     deltaCA = atk.transform.localEulerAngles.z - cam.transform.localEulerAngles.z;
            // }

            // Debug.Log(deltaCA);
            // cam.transform.RotateAround (tar.transform.position, Vector3.up, deltaCA);

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
        // atk.transform.RotateAround (tar.transform.position, Vector3.up, movep.Value);
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
