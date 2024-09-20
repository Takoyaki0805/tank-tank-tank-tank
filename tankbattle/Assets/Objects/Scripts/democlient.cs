using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class democlient : MonoBehaviour
{
    public NetworkManager net;
    // Start is called before the first frame update
    void Start()
    {
        net = GetComponent<NetworkManager>();
        net.StartClient();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
