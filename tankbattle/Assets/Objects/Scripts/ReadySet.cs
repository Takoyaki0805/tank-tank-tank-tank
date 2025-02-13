using UnityEngine;
using Unity.Netcode;
using Unity.Services.Lobbies;
using UnityEngine.SceneManagement;
using System;
using System.Threading.Tasks;
using Unity.Services.Authentication;



public class ReadySet : NetworkBehaviour
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

    public void boot(){
        ready.SetActive(true);
    }

    public void stopnet(){
        GameObject net = GameObject.FindWithTag("NET");
        Destroy(net);
        leave();
        NetworkManager.Singleton.Shutdown();
        SceneManager.LoadScene("NewMatch");
    }

    async Task leave(){
        try
        {
                    //Ensure you sign-in before calling Authentication Instance
                    //See IAuthenticationService interface
                    string playerId = AuthenticationService.Instance.PlayerId;
                    var lobbyId = await LobbyService.Instance.GetJoinedLobbiesAsync();
                    await LobbyService.Instance.RemovePlayerAsync(lobbyId[0], playerId);
        }
        catch (LobbyServiceException e)
        {
                    Debug.Log(e);
        }
    }


    // private void OnServerStoped(){
    //     stopnet();
    // }

    // void OnNetworkSpawn()
    // {
    //     ready.SetActive(true);
    // }
}
