using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class Buttondisable : NetworkBehaviour
{
    private NetworkVariable<int> networkint = new NetworkVariable<int>(
        0,                                          // 初期値
        NetworkVariableReadPermission.Everyone,     // 読み取り権限
        NetworkVariableWritePermission.Server        // 書き込み権限
    );
    public Button tar;
    bool ready = false;
    public GameObject cam;
    // int c = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(networkint.Value >= 2 && !ready){
            // tar.interactable = true;
            try{Destroy(tar.gameObject);}
            catch (MissingReferenceException e){

            }
            ready = true;
        }
        if(ready){
            GameObject tar = cam.gameObject.transform.parent.parent.gameObject;
            tar.GetComponent<playersheel>().button();
        }
    }

    public void getready(){
        tar.interactable = false;
        if(IsHost){
            count();
        }else{
            countRpc();
        }
    } 

    public void count(){
        networkint.Value = networkint.Value + 1;
    }

    [Rpc(SendTo.Server)]
    public void countRpc(){
        count();
    }
}
