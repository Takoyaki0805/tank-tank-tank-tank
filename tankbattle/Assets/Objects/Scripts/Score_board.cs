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
        1,                                          // 初期値(チーム残機の初期値を1とする)
        NetworkVariableReadPermission.Everyone,     // 読み取り権限
        NetworkVariableWritePermission.Server        // 書き込み権限
    );
    private NetworkVariable<int> blue_team_menber = new NetworkVariable<int>(
        1,                                          // 初期値(チーム残機の初期値を1とする)
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
        }
        red_txt.SetText(red_team_menber.Value.ToString());
        blue_txt.SetText(blue_team_menber.Value.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        int gameover_score = 0;
        red_txt.SetText(red_team_menber.Value.ToString());
        blue_txt.SetText(blue_team_menber.Value.ToString());
        //どちらかの陣営のスコアが0になったら自分の陣営に応じて勝敗のテキストを出す
        if(red_team_menber.Value == gameover_score||blue_team_menber.Value == gameover_score){
            canvas.SetActive(true);
            win_txt.SetActive(true);
            main.SetActive(false);
            cam.GetComponent<Button_Ready>().ButtonSwitchFalse();
            //それぞれの自機のチームを判別する
            string player_tag = "Player";
            string player_prefab_name = "tank";
            string player_prefab_name_clone = "tank(Clone)";
            GameObject[] group = GameObject.FindGameObjectsWithTag(player_tag);
            foreach(GameObject g in group){
                Debug.Log(g);
                if(g.name == player_prefab_name||g.name == player_prefab_name_clone){
                if(g.GetComponent<NetworkObject>().IsOwner){
                    IsRed = g.GetComponent<Team_color>().IsRed; 
                    IsBlue = g.GetComponent<Team_color>().IsBlue;
                }
                }
            }
            if(red_team_menber.Value == gameover_score&&IsRed){
                win_txt.SetActive(false);
                lose_txt.SetActive(true);
            }
            if(blue_team_menber.Value == gameover_score&&IsBlue){
                win_txt.SetActive(false);
                lose_txt.SetActive(true);
            }            
        }    
    }
    //それぞれのチームのスコアを減らす
    public void RedScore(){
        int score_dec = 1;
        red_team_menber.Value = red_team_menber.Value - score_dec;
    }

    public void BlueScore(){
        int score_dec = 1;
        blue_team_menber.Value = blue_team_menber.Value - score_dec;
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
