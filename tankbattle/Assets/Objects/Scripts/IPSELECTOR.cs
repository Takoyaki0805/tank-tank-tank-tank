using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using TMPro;

public class IPSELECTOR : MonoBehaviour
{
    public TMP_InputField txt;
    public NetworkManager net;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ipchange(){
        var transport = NetworkManager.Singleton.NetworkConfig.NetworkTransport;
        if (transport is Unity.Netcode.Transports.UTP.UnityTransport unityTransport)
        {
        // 接続先のIPアドレスとポートを指定
            unityTransport.SetConnectionData(txt.text, 5000);
        }
    }
}
