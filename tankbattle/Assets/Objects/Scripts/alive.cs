using UnityEngine;
using Unity.Netcode;

public class alive : NetworkBehaviour
{
    public int maxlife = 100;
    public int life;
    public GameObject dea;
    bool onetime = true;
    GameObject cam;
    GameObject UI;
    NetworkVariable<int> networklife = new NetworkVariable<int>(
            0,                                          // 初期値
            NetworkVariableReadPermission.Everyone,     // 読み取り権限
            NetworkVariableWritePermission.Server        // 書き込み権限
        );
    
    GameObject h;

    // public int atk = 30;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        life = maxlife;
        if(IsHost){
            networklife.Value = maxlife;
        }
        cam = GameObject.FindWithTag("MainCamera");
        UI = GameObject.FindWithTag("playUI");
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsHost){
            networklife.OnValueChanged += lifeValueChanged;
        }
        if(life<=0&&onetime){            
            isGameOver();
            onetime = false;
        }        
    }

    void OnCollisionEnter(Collision c){
        if(c.gameObject.tag=="ball"){
            // Destroy(c.gameObject);
            life -= c.gameObject.GetComponent<bulletmove>().atk;
            if(IsHost){
                networklife.Value = life;
            }
        }

        // Debug.Log("Aaassssssss");
    }

    void OnTriggerEnter(Collider c){
        if(c.gameObject.tag=="ball"){
            // Destroy(c.gameObject);
            life -= c.gameObject.GetComponent<bulletmove>().atk;
        }
        if(c.gameObject.tag=="mineatkzone"){
            // Destroy(c.gameObject);
            life -= c.gameObject.GetComponent<minedmg>().atk;
            if(IsHost){
                networklife.Value = life;
            }
        }
        if(life<=0&&onetime){
            isGameOver();
            onetime = false;
        }
    }    

    void isGameOver(){
        // if(IsHost){
        //     h = dethSpawn();
        // }else{
        //     dethRpc();
        // }
        
        if(IsOwner){
            cam.transform.parent = null;

            // cam.transform.localPosition = this.transform.position;
            UI.SetActive(false);
            this.transform.localScale = Vector3.zero;
            // this.gameObject.SetActive(false);
            // cam.SetActive(false);
            // cam.transform.parent = h.transform;
            // cam.transform.localPosition = this.transform.position;
            // cam.transform.localPosition = Vector3.up*20f;
            cam.transform.localScale = new Vector3(1,1,1);
        }
    }

    void lifeValueChanged(int a,int b){
        life = networklife.Value; 
    }
    // [Rpc(SendTo.Server)]
    // void isgRpc(){
    //     isGameOver();
    // }

    [Rpc(SendTo.Server)]
    void dethRpc(){
        h = dethSpawn();
    }

    GameObject dethSpawn(){
            GameObject d = Instantiate(dea,this.transform.position,Quaternion.EulerAngles(this.transform.localEulerAngles));
            d.GetComponent<NetworkObject>().Spawn();
            return d;
    }

    void isone(){
        onetime = true;
    }

    
}
