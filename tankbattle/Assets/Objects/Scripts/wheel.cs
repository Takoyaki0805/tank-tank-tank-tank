using UnityEngine;
using Unity.Netcode;

public class Wheel : NetworkBehaviour
{
    public GameObject wheel_object;
    // bool Isstay = true;
    public float timer;
    public bool IsWheelable = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(IsWheelable){
            timer += Time.deltaTime;
            if(0.35f<=timer){
                if(IsHost){
                    WheelLine(this.transform.position);
                }else{
                    WheelLineRpc(this.transform.position);
                }
                timer = 0;
            }
        }
    }

    void WheelLine(Vector3 position){
        GameObject spawn_wheel_line = Instantiate (wheel_object,position,Quaternion.Euler(this.transform.localEulerAngles));
        // NetworkObject f = h.GetComponent<NetworkObject>();
        spawn_wheel_line.GetComponent<NetworkObject>().Spawn();
        // spob.transform.localEulerAngles = this.transform.localEulerAngles;
        // Debug.Log("でた");
    }

    

    [Rpc(SendTo.Server)]
    public void WheelLineRpc(Vector3 position){
        WheelLine(position);
    }   
}
