using UnityEngine;
using System;

public class SpawnMangement : MonoBehaviour
{
    GameObject[] anthor;
    bool[] empty;
    System.Random rnd = new();
    GameObject[] Wherespawn;
    int sIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Wherespawn = GameObject.FindGameObjectsWithTag("spawn");
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
        while(!escaped){
            sIndex = rnd.Next(empty.Length);
            if(empty[sIndex]){
                empty[sIndex] = false;
                escaped = true;
            }
        }        
        return Wherespawn[sIndex];
    }

}
