using UnityEngine;
using Unity.Netcode;

public class Team_color : NetworkBehaviour
{
    public bool IsRed;
    public bool IsBlue;
    GameObject spawn_manage;
    GameObject cam;
    public GameObject spawn_position;
    [SerializeField]int number;
    bool IsOnetime = true;
    // Update is called once per frame
    void Update()
    {
        //チーム設定されたならスポーン地点に移動する
        if(IsBlue||IsRed){
            if(IsHost){
                ColorSetRpc(IsRed,IsBlue,number);
            }
            if(IsOwner&&IsOnetime){
                IsOnetime = false;
                this.gameObject.transform.position = spawn_position.transform.position;
            }
        }
    }

    //スポーンマネージャを探す
    public void SetupManage(){
        string object_tag_name = "spawnMNG";
        spawn_manage = GameObject.FindWithTag(object_tag_name);
    }

    public void TeamSet(){
            if(spawn_position.GetComponent<Flag_color>().flag_red){
                IsRed = true;
            }
            if(spawn_position.GetComponent<Flag_color>().flag_blue){
                IsBlue = true;
            }
    }

    [Rpc(SendTo.Everyone)]
    public void ColorSetRpc(bool r,bool b,int n){
         if(!IsHost){
            IsRed = r;
            IsBlue = b;
            spawn_position = spawn_manage.GetComponent<Spawn_management>().GetObject(n);
        }
    }    

    //スポーン地点を設定して準備完了ボタンを出現させる
    public override void OnNetworkSpawn(){
        SetupManage();
        if(IsOwner){
            if(!IsHost){
                ServerPositionSetRpc();
            }else{
                ServerPositionSet();
            }
            spawn_manage.GetComponent<Ready_event>().ShowReady();
        }
    }

    //スポーン地点を記憶する
    public void ServerPositionSet(){
        number = spawn_manage.GetComponent<Spawn_management>().PlayerAttach();
        spawn_position = spawn_manage.GetComponent<Spawn_management>().GetObject(number);
        TeamSet();
    }

    //ゲームホストにスポーン地点の記憶を依頼する
    [Rpc(SendTo.Server)]
    public void ServerPositionSetRpc(){
        ServerPositionSet();
    }

    [Rpc(SendTo.Server)]
    public void TransformRpc(Vector3 pos){
        this.gameObject.transform.position = pos;        
    }
}

