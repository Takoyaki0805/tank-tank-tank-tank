using UnityEngine;
using Unity.Netcode;

public class Wheel : NetworkBehaviour
{
    public GameObject wheel_object;
    public float timer;
    public bool IsWheelable = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //時間に経過で自機からタイヤ痕を生成する
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

    //タイヤ痕を生成
    void WheelLine(Vector3 position){
        GameObject spawn_wheel_line = Instantiate (wheel_object,position,Quaternion.Euler(this.transform.localEulerAngles));
        spawn_wheel_line.GetComponent<NetworkObject>().Spawn();
    }
 
    //ゲームホストに生成依頼
    [Rpc(SendTo.Server)]
    public void WheelLineRpc(Vector3 position){
        WheelLine(position);
    }   
}
