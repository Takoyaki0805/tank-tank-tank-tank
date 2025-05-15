using UnityEngine;
using Unity.Netcode;
using Unity.Services.Lobbies;
using UnityEngine.SceneManagement;
using System;
using System.Threading.Tasks;
using Unity.Services.Authentication;

public class Session_event : NetworkBehaviour
{
    [SerializeField]GameObject ready;
    //準備完了ボタンを出現させる
    public void PlayerBoot(){
        ready.SetActive(true);
    }

    //ネットワークを終了しマッチ画面に移動する
    public void stopnet(){
        string manager_tag_name = "NET";
        string scene_name = "NewMatch";
        GameObject net = GameObject.FindWithTag(manager_tag_name);
        Destroy(net);
        LeaveSession();
        NetworkManager.Singleton.Shutdown();
        SceneManager.LoadScene(scene_name);
    }

    //セッションを破棄する
    async Task LeaveSession(){
        try
        {
            string player_id = AuthenticationService.Instance.PlayerId;
            var lobby_id = await LobbyService.Instance.GetJoinedLobbiesAsync();
            await LobbyService.Instance.RemovePlayerAsync(lobby_id[0], player_id);
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
}
