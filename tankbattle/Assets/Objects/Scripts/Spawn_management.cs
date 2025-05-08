using UnityEngine;
using System;
using Unity.Netcode;

public class Spawn_management : NetworkBehaviour
{
    GameObject[] anthor;
    public bool[] empty;
    System.Random rnd = new();
    public GameObject[] where_spawn;
    int spawn_index;
    bool IsReset = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        empty = new bool[2];
        Array.Fill(empty,true);
    }

    void Awake(){

    }

    // Update is called once per frame
    void Update()
    {

    }

    //スポーン地点の予約状況をリセットさせる
    public void ResetManage(){
        if(IsReset){return;}
            empty = new bool[2];
            Array.Fill(empty,true);
            IsReset = false;
    }

    public int PlayerAttach(){
        bool escaped = false;
        while(!escaped){
            //ランダムに番号を出力し結果に応じてスポーン地点を振り分ける
            spawn_index = rnd.Next(empty.Length);
            //振り分けられた地点が予約済みじゃないならループから抜ける
            if(empty[spawn_index]){
                empty[spawn_index] = false;
                escaped = true;
            }
        }
        Debug.Log(spawn_index);  
        return spawn_index;
    }

    //ここから先は未使用
    [Rpc(SendTo.Server)]
    public void SpawnCheckRpc(){
        PlayerAttach();
    }

    public GameObject GetObject(int number){
        return where_spawn[number];
    }

    public void SpawnOut(GameObject g){
        int point = 0;
        foreach(GameObject target in where_spawn){
            if(target == g){
                empty[point] = false;
            }
            point++;
        }
    }
}
