using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class Select_join : MonoBehaviour
{
    //現在は使用していないスプリクト
    public NetworkManager net;
    // Start is called before the first frame update
    void Start()
    {
        net = GetComponent<NetworkManager>();

    }

    public void JoinClient(){
        net.StartClient();
    } 
    
    public void JoinHost(){
        string next_scene = "Battle Stage";
        NetworkManager.Singleton.ConnectionApprovalCallback = ApprovalCheck;
        SceneManager.LoadScene(next_scene);
        net.StartHost();
    } 

    private void ApprovalCheck(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
    {
        // 追加の承認手順が必要な場合は、追加の手順が完了するまでこれを true に設定します
        // true から false に遷移すると、接続承認応答が処理されます。
        response.Pending = true;

        //最大人数をチェック(この場合は2人まで)
        int max_player = 3;
        if (NetworkManager.Singleton.ConnectedClients.Count >= max_player)
        {
            response.Approved = false;//接続を許可しない
            response.Pending = false;
            return;
        }

        //ここからは接続成功クライアントに向けた処理
        response.Approved = true;//接続を許可

        //PlayerObjectを生成するかどうか
        response.CreatePlayerObject = true;

        //生成するPrefabハッシュ値。nullの場合NetworkManagerに登録したプレハブが使用される
        response.PlayerPrefabHash = null;
        response.Pending = false;
    }

}
