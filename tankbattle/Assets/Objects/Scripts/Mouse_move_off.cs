using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;

//オフライン版のマウス操作の挙動
public class Mouse_move_off : MonoBehaviour
{
    public GameObject target;
    public GameObject cam;
    public GameObject attack;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
    //カメラを視点の位置に移動
        cam = GameObject.FindWithTag("MainCamera");
        SceneManager.sceneLoaded += OnLoaded;
        cam.transform.position = attack.transform.position + Vector3.back*3.5f +Vector3.up*1.5f;
        cam.transform.parent = target.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //上限を調整
        float h = Input.GetAxis("Mouse X");
        if(h>=3f){
            h=3f;
        }
        if(h<=-3f){
            h=-3f;
        }
        attack.transform.RotateAround (target.transform.position, Vector3.up, h*Time.deltaTime*speed);
        cam.transform.RotateAround (target.transform.position, Vector3.up, h*Time.deltaTime*speed);
    }

    void OnLoaded(Scene s,LoadSceneMode m){
        cam = GameObject.FindWithTag("MainCamera");        
    }

    void Awake(){
    }


}
