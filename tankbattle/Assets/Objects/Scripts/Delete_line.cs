using UnityEngine;
using Unity.Netcode;

public class Delete_line : NetworkBehaviour
{
    float timer;
    public float limit = 7.5f;
    public GameObject left_line;
    public GameObject right_line;
    // Update is called once per frame
    void Update()
    {
        //時間経過でタイヤ痕を消す
        timer += Time.deltaTime;
        if(timer>=limit){
        //ゲームホストかクライアントどちらがタイヤ痕を出したかによって切り替える
            if(IsHost){
                DeleteThis();
            }else{
                DeleteThisRpc();
            }
        }
        //時間によって透明度を増やす
        left_line.GetComponent<MeshRenderer>().material.color = new Color(1,1,1,1-(1*timer)/limit);   
        right_line.GetComponent<MeshRenderer>().material.color = new Color(1,1,1,1-(1*timer)/limit);
    }

    
    void DeleteThis(){
        this.gameObject.GetComponent<NetworkObject>().Despawn();
    }

    //ゲームホストに破棄を依頼する
    [Rpc(SendTo.Server)]
    public void DeleteThisRpc(){
        DeleteThis();
    } 
}