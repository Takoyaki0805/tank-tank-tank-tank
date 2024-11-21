using UnityEngine;
using Unity.Netcode;

public class mine : NetworkBehaviour
{
    float timer;
    public float pheseA=5.0f;
    public float pheseB=8.0f;
    public float pheseC=3.0f;
    public GameObject bomb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bomb.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
