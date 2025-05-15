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
    string camera_tag_name = "MainCamera";    
    // Start is called before the first frame update
    void Start()
    {
        float upper_correction = 1.5f;
        float back_correction = 3.5f;
        //カメラを視点の位置に移動
        cam = GameObject.FindWithTag(camera_tag_name);
        SceneManager.sceneLoaded += OnLoaded;
        cam.transform.position = attack.transform.position + Vector3.back*back_correction +Vector3.up*upper_correction;
        cam.transform.parent = target.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float mouse_speed_max = 3f;
        string mouse_action_name = "Mouse X";
        //上限を調整
        float h = Input.GetAxis(mouse_action_name);
        if(h >= mouse_speed_max){
            h = mouse_speed_max;
        }
        if(h <= -mouse_speed_max){
            h = -mouse_speed_max;
        }
        attack.transform.RotateAround (target.transform.position, Vector3.up, h*Time.deltaTime*speed);
        cam.transform.RotateAround (target.transform.position, Vector3.up, h*Time.deltaTime*speed);
    }

    void OnLoaded(Scene s,LoadSceneMode m){
        cam = GameObject.FindWithTag(camera_tag_name);        
    }
}
