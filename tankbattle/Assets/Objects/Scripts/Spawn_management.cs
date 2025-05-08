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

    public void ResetManage(){
        if(IsReset){return;}
            empty = new bool[2];
            Array.Fill(empty,true);
            IsReset = false;
    }

    public int PlayerAttach(){
        bool escaped = false;
        while(!escaped){
            spawn_index = rnd.Next(empty.Length);
            if(empty[spawn_index]){
                empty[spawn_index] = false;
                escaped = true;
            }
        }
        Debug.Log(spawn_index);  
        return spawn_index;
    }
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
