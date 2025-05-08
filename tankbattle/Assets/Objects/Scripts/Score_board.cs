using UnityEngine;
using Unity.Netcode;
using TMPro;
using System;
using UnityEngine.UI;


public class Score_board : NetworkBehaviour
{
    public TMP_Text red_txt;
    public TMP_Text blue_txt;
    public bool IsRedTeam = false;
    public bool IsBlueTeam = false;
    public GameObject canvas;
    public GameObject win_txt;
    public GameObject lose_txt;
    public GameObject cam;
    public GameObject main;
    public int max_score = 1;

    bool IsRed = false;
    bool IsBlue = false;

    private NetworkVariable<int> red_team_menber = new NetworkVariable<int>(
        1,                                          // 初期値
        NetworkVariableReadPermission.Everyone,     // 読み取り権限
        NetworkVariableWritePermission.Server        // 書き込み権限
    );
    private NetworkVariable<int> blue_team_menber = new NetworkVariable<int>(
        1,                                          // 初期値
        NetworkVariableReadPermission.Everyone,     // 読み取り権限
        NetworkVariableWritePermission.Server        // 書き込み権限
    );
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //勝敗と退出ボタンを隠す
        canvas.SetActive(false);
        win_txt.SetActive(false);
        lose_txt.SetActive(false);
        if(IsHost){
            ResetScore();
        }else{
            
        }
        red_txt.SetText(red_team_menber.Value.ToString());
        blue_txt.SetText(blue_team_menber.Value.ToString());

    }

    // Update is called once per frame
    void Update()
    {
        red_txt.SetText(red_team_menber.Value.ToString());
        blue_txt.SetText(blue_team_menber.Value.ToString());
        //どちらかの陣営のスコアが0になったら自分の陣営に応じて勝敗のテキストを出す
        if(red_team_menber.Value == 0||blue_team_menber.Value == 0){
            canvas.SetActive(true);
            win_txt.SetActive(true);
            main.SetActive(false);
            cam.GetComponent<Button_Ready>().ButtonSwitchFalse();
            //それぞれの自機のチームを判別する
            GameObject[] group = GameObject.FindGameObjectsWithTag("Player");
            foreach(GameObject g in group){
                Debug.Log(g);
                if(g.name == "tank"||g.name == "tank(Clone)"){
                if(g.GetComponent<NetworkObject>().IsOwner){
                    IsRed = g.GetComponent<Team_color>().IsRed; 
                    IsBlue = g.GetComponent<Team_color>().IsBlue;
                }
                }
            }
            if(red_team_menber.Value == 0&&IsRed){
                win_txt.SetActive(false);
                lose_txt.SetActive(true);
            }
            if(blue_team_menber.Value == 0&&IsBlue){
                win_txt.SetActive(false);
                lose_txt.SetActive(true);
            }            
        }    
    }
    //それぞれのチームのスコアを減らす
    public void RedScore(){
        red_team_menber.Value = red_team_menber.Value -1;
    }

    public void BlueScore(){
        blue_team_menber.Value = blue_team_menber.Value -1;
    }
    //ゲームホストに情報を集中させるためにクライアントがゲームホストに得点操作を依頼する
    [Rpc(SendTo.Server)]
    public void RedScoreRpc(){
        RedScore();
    }    

    [Rpc(SendTo.Server)]
    public void BlueScoreRpc(){
        BlueScore();
    } 

    public void ResetScore(){
        red_team_menber.Value = max_score;
        blue_team_menber.Value = max_score;
    }

    [Rpc(SendTo.Server)]
    public void ResetScoreRpc(){
        ResetScore();
    }   
    
}
