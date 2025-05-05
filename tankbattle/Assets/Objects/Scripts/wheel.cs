using UnityEngine;
using Unity.Netcode;

public class wheel : NetworkBehaviour
{
    public GameObject obj;
    // bool Isstay = true;
    public float timer;
    public bool wheelable = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(wheelable){
            timer += Time.deltaTime;
            if(0.35f<=timer){
                if(IsHost){
                    wheelline(this.transform.position);
                }else{
                    wheellineRpc(this.transform.position);
                }
                timer = 0;
            }
        }
    }

    // void  OnTriggerExit(Collider c){
    //     if(Isstay){

    //     }
    //     Isstay = true;
    // }

    // void OnTriggerEnter(Collider c){
    //     Isstay = false;
    //     Debug.Log("in");
    // }

    void wheelline(Vector3 position){
        GameObject spob = Instantiate (obj,position,Quaternion.Euler(this.transform.localEulerAngles));
        // NetworkObject f = h.GetComponent<NetworkObject>();
        spob.GetComponent<NetworkObject>().Spawn();
        // spob.transform.localEulerAngles = this.transform.localEulerAngles;
        // Debug.Log("でた");
    }

    

    [Rpc(SendTo.Server)]
    public void wheellineRpc(Vector3 position){
        wheelline(position);
    }   
}
