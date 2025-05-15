using UnityEngine;

public class Ready_event : MonoBehaviour
{
    public GameObject ready;
    //準備完了ボタンに関するイベントを起動する
    public void ShowReady(){
        ready.GetComponent<Session_event>().PlayerBoot();
    }
}