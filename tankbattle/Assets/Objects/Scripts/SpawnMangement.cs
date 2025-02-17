using UnityEngine;
using System;
using Unity.Netcode;

public class SpawnMangement : NetworkBehaviour
{
    GameObject[] anthor;
    public bool[] empty;
    System.Random rnd = new();
    public GameObject[] Wherespawn;
    int sIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Wherespawn = GameObject.FindGameObjectsWithTag("spawn");
        empty = new bool[2];
        Array.Fill(empty,true);
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(empty.Length);
    }

    public GameObject playerattach(){
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
        return Wherespawn[sIndex];
    }
    [Rpc(SendTo.Server)]
    public void SpawnchkRpc(){
        playerattach();
    }
}
