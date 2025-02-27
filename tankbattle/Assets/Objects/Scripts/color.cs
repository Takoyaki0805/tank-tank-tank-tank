using UnityEngine;
using Unity.Netcode;

public class color : NetworkBehaviour
{
    public bool isred;
    public bool isblue;
    GameObject spawnMng;
    GameObject cam;
    public GameObject spawnpos;
    [SerializeField]int n;
    bool onetime = true;
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
        if(isblue||isred){
            if(IsHost){
                colorsetRpc(isred,isblue,n);
            }
                if(IsOwner&&onetime){
                    onetime = false;
                    this.gameObject.transform.position = spawnpos.transform.position;
                }
                    // tranRpc(spawnpos.transform.position);
                // }
        }

        // if(IsOwner&&onetime){
        //     onetime = false;
        //     this.gameObject.transform.position = spawnpos.transform.position;
        // }

    }

    public void setupMNG(){
        spawnMng = GameObject.FindWithTag("spawnMNG");
        // if(IsHost){
        //     colorsetRpc(isred,isblue);
        // }
        // spawnMng.GetComponent<>()
    }

    public void teamset(){
        // if(IsHost){
            if(spawnpos.GetComponent<flagcolor>().flagred){
                isred = true;
            }
            if(spawnpos.GetComponent<flagcolor>().flagblue){
                isblue = true;
            }

        // }
    }

    [Rpc(SendTo.Everyone)]
    public void colorsetRpc(bool r,bool b,int n){
         if(!IsHost){
            isred = r;
            isblue = b;
            spawnpos = spawnMng.GetComponent<SpawnManagement>().getObj(n);
        }
    }    

    public void countteam(){
        GameObject[] p = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject i in p){
            color d = i.GetComponent<color>();
            if(d.isred){
                Debug.Log("on");
            }
        }
    }

    public override void OnNetworkSpawn(){
        setupMNG();
        if(IsOwner){
            if(!IsHost){
                ServerpossetRpc();
            }else{
                Serverposset();
            }
            spawnMng.GetComponent<Readypipe>().showready();
        }
    }

    public void Serverposset(){
        n = spawnMng.GetComponent<SpawnManagement>().playerattach();
        spawnpos = spawnMng.GetComponent<SpawnManagement>().getObj(n);
        teamset();
    }

    [Rpc(SendTo.Server)]
    public void ServerpossetRpc(){
        Serverposset();
    }

    [Rpc(SendTo.Server)]
    public void tranRpc(Vector3 pos){
        this.gameObject.transform.position = pos;        
    }

}

