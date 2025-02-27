using UnityEngine;
using System;
using Unity.Netcode;

public class SpawnManagement : NetworkBehaviour
{
    GameObject[] anthor;
    public bool[] empty;
    System.Random rnd = new();
    public GameObject[] Wherespawn;
    int sIndex;
    bool on = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Wherespawn = GameObject.FindGameObjectsWithTag("spawn");
        if(IsHost){

        }
        empty = new bool[2];
        Array.Fill(empty,true);
        Debug.Log("Heyheyhey");

    }

    void Awake(){

    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(empty.Length);
    }

    public void resetmanage(){
        if(on){return;}
            empty = new bool[2];
            Array.Fill(empty,true);
            on = false;
    }

    public int playerattach(){
        // GameObject g = null;
        bool escaped = false;
        // Debug.Log(escaped);
        while(!escaped){
            sIndex = rnd.Next(empty.Length);
            if(empty[sIndex]){
                empty[sIndex] = false;
                escaped = true;
            }
        }
        Debug.Log(sIndex);  
        return sIndex;
    }
    [Rpc(SendTo.Server)]
    public void SpawnchkRpc(){
        playerattach();
    }

    public GameObject getObj(int num){
        return Wherespawn[num];
    }

    public void spawnout(GameObject g){
        int point = 0;
        foreach(GameObject tar in Wherespawn){
            if(tar == g){
                empty[point] = false;
            }
            point++;
        }
    }
}
