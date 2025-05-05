using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class Button_sheel : NetworkBehaviour
{
    private NetworkVariable<int> networkint = new NetworkVariable<int>(
        0,                                          // 初期値
        NetworkVariableReadPermission.Everyone,     // 読み取り権限
        NetworkVariableWritePermission.Server        // 書き込み権限
    );
    public Button target_button;
    bool IsReady = false;
    public GameObject camera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(networkint.Value >= 2 && !IsReady){
            // tar.interactable = true;
            try{Destroy(target_button.gameObject);}
            catch (MissingReferenceException e){

            }
            IsReady = true;
        }
        if(IsReady){
            GameObject target = camera.gameObject.transform.parent.parent.gameObject;
            target.GetComponent<playersheel>().button();
        }
    }

    public void GetReady(){
        target_button.interactable = false;
        if(IsHost){
            ReadyCount();
        }else{
            ReadyCountRpc();
        }
    } 

    public void ReadyCount(){
        networkint.Value = networkint.Value + 1;
    }

    [Rpc(SendTo.Server)]
    public void ReadyCountRpc(){
        ReadyCount();
    }
}
