using UnityEngine;
using Unity.Netcode;

public class Wheel : NetworkBehaviour
{
    public GameObject wheel_object;
    public float timer;
    public bool IsWheelable = false;
    // Update is called once per frame
    void Update()
    {
        float delete_time = 0.35f;
        float reset_time = 0f;
        //時間に経過で自機からタイヤ痕を生成する
        if(IsWheelable){
            timer += Time.deltaTime;
            if(delete_time<=timer){
                if(IsHost){
                    WheelLine(this.transform.position);
                }else{
                    WheelLineRpc(this.transform.position);
                }
                timer = reset_time;
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
