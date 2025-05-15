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
    public GameObject cam;
    int players = 2;
    // Update is called once per frame
    void Update()
    {
        //プレイヤー二人が準備完了したらプレイヤーの制限を開放する
        if(networkint.Value >= players && !IsReady){
            try{Destroy(target_button.gameObject);}
            catch (MissingReferenceException e){
            }
            IsReady = true;
        }
        if(IsReady){
            GameObject target = cam.gameObject.transform.parent.parent.gameObject;
            target.GetComponent<Player_sheel>().DecideButton();
        }
    }

    //ボタンを押したプレイヤーがゲームホスト、クライアントによって動作を切り替える
    public void GetReady(){
        target_button.interactable = false;
        if(IsHost){
            ReadyCount();
        }else{
            ReadyCountRpc();
        }
    }

    //何人が準備完了したかをカウントする
    public void ReadyCount(){
        networkint.Value = networkint.Value + 1;
    }

    //ゲームホストにカウントを依頼する
    [Rpc(SendTo.Server)]
    public void ReadyCountRpc(){
        ReadyCount();
    }
}
