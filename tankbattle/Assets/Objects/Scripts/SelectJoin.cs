using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;


public class SelectJoin : MonoBehaviour
{
    public NetworkManager net;
    // Start is called before the first frame update
    void Start()
    {
        net = GetComponent<NetworkManager>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void JoinClient(){
        SceneManager.LoadScene("Battle Stage");   
        net.StartClient();
    } 
    
    public void JoinHost(){
        SceneManager.LoadScene("Battle Stage");
        net.StartHost();
    } 

}
