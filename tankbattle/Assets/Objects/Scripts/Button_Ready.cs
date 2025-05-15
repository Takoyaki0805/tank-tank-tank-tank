using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class Button_Ready : MonoBehaviour
{
    //準備完了ボタンを表示したり消したりする。
    public void ButtonSwitch(){
        GameObject target = this.gameObject.transform.parent.parent.gameObject;
        target.GetComponent<Player_sheel>().DecideButton();
    }

    public void ButtonSwitchFalse(){
        GameObject target = this.gameObject.transform.parent.parent.gameObject;
        target.GetComponent<Player_sheel>().CancelButton();
    }
}

