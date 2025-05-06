using UnityEngine;
using Unity.Netcode;

public class Team_color : NetworkBehaviour
{
    public bool IsRed;
    public bool IsBlue;
    GameObject spawn_manage;
    GameObject camera;
    public GameObject spawn_position;
    [SerializeField]int number;
    bool Isonetime = true;
    // public GameObject ready;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // if(IsHost&&IsOwner){
        //     isred = true;
        // }else if(IsOwner){
        //     isblue = true;
        // }
    }

    // Update is called once per frame
    void Update()
    {
        if(IsBlue||IsRed){
            if(IsHost){
                ColorSetRpc(IsRed,IsBlue,number);
            }
                if(IsOwner&&Isonetime){
                    Isonetime = false;
                    this.gameObject.transform.position = spawn_position.transform.position;
                }
                    // tranRpc(spawnpos.transform.position);
                // }
        }

        // if(IsOwner&&onetime){
        //     onetime = false;
        //     this.gameObject.transform.position = spawnpos.transform.position;
        // }

    }

    public void SetupManage(){
        spawn_manage = GameObject.FindWithTag("spawnMNG");
        // if(IsHost){
        //     colorsetRpc(isred,isblue);
        // }
        // spawnMng.GetComponent<>()
    }

    public void TeamSet(){
        // if(IsHost){
            if(spawn_position.GetComponent<Flag_color>().flag_red){
                IsRed = true;
            }
            if(spawn_position.GetComponent<Flag_color>().flag_blue){
                IsBlue = true;
            }

        // }
    }

    [Rpc(SendTo.Everyone)]
    public void ColorSetRpc(bool r,bool b,int n){
         if(!IsHost){
            IsRed = r;
            IsBlue = b;
            spawn_position = spawn_manage.GetComponent<Spawn_management>().GetObject(n);
        }
    }    

    public void CountTeam(){
        GameObject[] p = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject i in p){
            Team_color d = i.GetComponent<Team_color>();
            if(d.IsRed){
                Debug.Log("on");
            }
        }
    }

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

    public void ServerPositionSet(){
        number = spawn_manage.GetComponent<Spawn_management>().PlayerAttach();
        spawn_position = spawn_manage.GetComponent<Spawn_management>().GetObject(number);
        TeamSet();
    }

    [Rpc(SendTo.Server)]
    public void ServerPositionSetRpc(){
        ServerPositionSet();
    }

    [Rpc(SendTo.Server)]
    public void TransformRpc(Vector3 pos){
        this.gameObject.transform.position = pos;        
    }

}

