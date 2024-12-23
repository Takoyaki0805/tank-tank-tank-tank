using UnityEngine;
using Unity.Netcode;
using TMPro;
using System;

public class scoreboard : NetworkBehaviour
{
    public TMP_Text redtxt;
    public TMP_Text bluetxt;

    public int maxscore = 1;
    private NetworkVariable<int> redteam = new NetworkVariable<int>(
        1,                                          // 初期値
        NetworkVariableReadPermission.Everyone,     // 読み取り権限
        NetworkVariableWritePermission.Server        // 書き込み権限
    );
    private NetworkVariable<int> blueteam = new NetworkVariable<int>(
        1,                                          // 初期値
        NetworkVariableReadPermission.Everyone,     // 読み取り権限
        NetworkVariableWritePermission.Server        // 書き込み権限
    );
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(IsHost){
            resetscore();
        }else{
            // resetscoreRpc();
        }
        redtxt.SetText(redteam.Value.ToString());
        bluetxt.SetText(blueteam.Value.ToString());

    }

    // Update is called once per frame
    void Update()
    {
        redtxt.SetText(redteam.Value.ToString());
        bluetxt.SetText(blueteam.Value.ToString());    
    }

    public void redscore(){
        redteam.Value = redteam.Value -1;
    }

    public void bluescore(){
        blueteam.Value = blueteam.Value -1;
    }

    [Rpc(SendTo.Server)]
    public void redscoreRpc(){
        redscore();
    }    

    [Rpc(SendTo.Server)]
    public void bluescoreRpc(){
        bluescore();
    } 

    public void resetscore(){
        redteam.Value = maxscore;
        blueteam.Value = maxscore;
    }

    [Rpc(SendTo.Server)]
    public void resetscoreRpc(){
        resetscore();
    }   
    
}
