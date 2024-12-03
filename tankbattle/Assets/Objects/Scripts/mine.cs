using UnityEngine;
using Unity.Netcode;
using System.Collections;

public class mine : NetworkBehaviour
{
    float timer=0;
    public float pheseA=5.0f;
    public float pheseB=8.0f;
    public float pheseC=11.0f;
    public int atk = 100;
    public GameObject bomb;
    Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
        bomb.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        // Debug.Log(pheseA);
        if(timer>=pheseA){
            anim.SetBool("alarm",true);
        }
        if(timer>=pheseB){
            bomb.SetActive(true);
        }
        if(timer>=pheseC){
            if(IsHost){
                dismine();
            }else{
                dismineRpc();
            }
        }
    }

    public void dismine(){
        this.gameObject.GetComponent<NetworkObject>().Despawn();
    }

    [Rpc(SendTo.Server)]
    public void dismineRpc(){
        dismine();
    }

}
