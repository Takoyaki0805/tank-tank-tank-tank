using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;


public class Bullet_move : NetworkBehaviour
{
    public int attack = 30;
    public float time_limit = 120.0f;
    public int bounce = 2;
    int count = 0;
    float timer = 0f;
    // Update is called once per frame
    void Update()
    {
        if(timer >= time_limit || count>bounce){
            if(IsHost){
                DisSpawn();
                
            }else{
                DisSpawnRpc();
            }
        }
        timer += Time.deltaTime;
    }
    //弾丸を破壊する
    void DisSpawn(){
        this.gameObject.GetComponent<NetworkObject>().Despawn();
    }

    //ホストにオブジェクトの破棄を依頼する
    [Rpc(SendTo.Server)]
    public void DisSpawnRpc(){
        DisSpawn();
    }


    void OnCollisionEnter(Collision c){
    //ゲームホスト、クライアントのどちらに弾丸が命中したかによって処理を変更する。
            if(IsHost){
                DisSpawn();
            }else{
                DisSpawnRpc();
            }
    }
}
