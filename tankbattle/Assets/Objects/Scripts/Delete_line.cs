using UnityEngine;
using Unity.Netcode;

public class Delete_line : NetworkBehaviour
{
    float timer;
    public float limit = 7.5f;
    public GameObject left_line;
    public GameObject right_line;
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
                DeleteThis();
            }else{
                DeleteThisRpc();
            }
        }
        left_line.GetComponent<MeshRenderer>().material.color = new Color(1,1,1,1-(1*timer)/limit);   
        right_line.GetComponent<MeshRenderer>().material.color = new Color(1,1,1,1-(1*timer)/limit);
    }

    void DeleteThis(){
        this.gameObject.GetComponent<NetworkObject>().Despawn();
    }

    [Rpc(SendTo.Server)]
    public void DeleteThisRpc(){
        DeleteThis();
    } 
}