using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using TMPro;
using System;

//UIとそれぞれのパラメータをつなげる
public class UI_system : NetworkBehaviour
{
    public GameObject ui_target;
    public GameObject[] bullet_mark;
    int player_bullet = 0;
    int player_mine = 0; 
    int player_maxhp = 0;
    int player_hp = 0;
    float bullet_charge = 0f;
    float charge_time = 0f;
    public Fire_Net data_source;
    public Player_life hp_data_source;
    public TMP_Text txt;
    public Slider hp_bar;
    public Slider bullet_charge_bar;
    GameObject ui_obj;
    // Update is called once per frame
    void Update()
    {
        try{
            ui_obj = ui_target.transform.parent.parent.gameObject;
        }catch(NullReferenceException e){
            return;
        }catch(MissingReferenceException e){
            return;
        }
        //HPバー、弾丸のチャージバーを設定する
        data_source = ui_obj.GetComponent<Fire_Net>();
        hp_data_source = ui_obj.GetComponent<Player_life>();
        player_bullet = data_source.have_bullet;
        player_mine = data_source.have_mine;
        charge_time = data_source.bullet_chargetime;
        bullet_charge = data_source.bullet_charge;
        player_hp = hp_data_source.life;
        player_maxhp = hp_data_source.maxlife;
        hp_bar.value = (float)player_hp/(float)player_maxhp;
        bullet_charge_bar.value = (float)charge_time/(float)bullet_charge;
        int charge_non = 0;
        int charge_mid = 1;
        int charge_max = 2;
        //弾丸のチャージ数でアイコンの表記を変える
        if(player_bullet==charge_non){
            foreach(GameObject j in bullet_mark){
                j.SetActive(false);
            }
        }
        if(player_bullet==charge_mid){
            bullet_mark[0].SetActive(true);
            bullet_mark[1].SetActive(false);
        }
        if(player_bullet==charge_max){
            foreach(GameObject j in bullet_mark){
                j.SetActive(true);
            }
        }
        string mine_number = player_mine + "";
        txt.SetText(mine_number);
    }
}
