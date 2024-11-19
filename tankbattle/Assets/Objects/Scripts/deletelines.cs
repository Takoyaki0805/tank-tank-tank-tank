using UnityEngine;
using Unity.Netcode;

public class deletelines : NetworkBehaviour
{
    float timer;
    public float limit=7.5f;
    public GameObject left;
    public GameObject right;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer>=limit){
            if(IsHost){
                deletethis();
            }else{
                deletethisRpc();
            }
        }
        left.GetComponent<MeshRenderer>().material.color = new Color(1,1,1,1-(1*timer)/limit);   
        right.GetComponent<MeshRenderer>().material.color = new Color(1,1,1,1-(1*timer)/limit);
    }

    void deletethis(){
        this.gameObject.GetComponent<NetworkObject>().Despawn();
    }

    [Rpc(SendTo.Server)]
    public void deletethisRpc(){
        deletethis();
    } 
}
