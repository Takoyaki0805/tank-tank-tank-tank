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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //準備完了ボタンを出現させる
    public void PlayerBoot(){
        ready.SetActive(true);
    }

    //ネットワークを終了しマッチ画面に移動する
    public void stopnet(){
        GameObject net = GameObject.FindWithTag("NET");
        Destroy(net);
        LeaveSession();
        NetworkManager.Singleton.Shutdown();
        SceneManager.LoadScene("NewMatch");
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
