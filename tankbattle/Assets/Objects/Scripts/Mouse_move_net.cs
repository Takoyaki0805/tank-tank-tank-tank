using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;
using UnityEngine.InputSystem;

public class Mouse_move_net : NetworkBehaviour
{
    public GameObject target;
    public GameObject cam;
    public GameObject player_attack_value;
    public float speed;
    InputAction key;
    Vector2 move_value;
    public bool AbleCameraMove = false;

    private NetworkVariable<float> move_point = new NetworkVariable<float>(
        0f,                                          // 初期値
        NetworkVariableReadPermission.Everyone,     // 読み取り権限
        NetworkVariableWritePermission.Owner        // 書き込み権限
        );
    // Start is called before the first frame update
    void Start()
    {
        //カメラを適切な視点に移動
        if(IsOwner){
            cam = GameObject.FindWithTag("MainCamera");
            SceneManager.sceneLoaded += OnLoaded;
            cam.transform.position = player_attack_value.transform.position + Vector3.back*4.2f +Vector3.up*2f;
            cam.transform.parent = target.transform;
        }
        var Input = this.gameObject.GetComponent<PlayerInput>();
        key = Input.actions["camera"];
    }

    //砲身をマウスで動かす
    // Update is called once per frame
    void Update()
    {
        float mouse_value = Input.GetAxis("Mouse X");
        float mouse_move_max = 0;
        move_value = key.ReadValue<Vector2>();
        if(AbleCameraMove){
            //上限を調整
            if(mouse_value>0){
                mouse_move_max=3f;
            }
            if(mouse_value<0){
                mouse_move_max=-3f;
            }
            //下限を調整
            if(mouse_value<0.1&&mouse_value>-0.1){
                mouse_move_max=0;
            }else{
                mouse_move_max=mouse_value;
            }
            if(IsOwner){
                if(move_value.x!=0){
                    move_point.Value = move_value.x*Time.deltaTime*speed/3.5f;
                }else{
                    move_point.Value = mouse_move_max*Time.deltaTime*speed;
                }
                cam.transform.RotateAround (target.transform.position, Vector3.up, move_point.Value);
                player_attack_value.transform.RotateAround (target.transform.position, Vector3.up, move_point.Value);
            }
        }
    }

    void OnLoaded(Scene s,LoadSceneMode m){
        cam = GameObject.FindWithTag("MainCamera");        
    }

    [Unity.Netcode.ServerRpc]
    void TurnServerRpc(Vector3 p,Vector3 l){
        player_attack_value.transform.position = p;
        player_attack_value.transform.localEulerAngles = l;
        return;
    }
}
