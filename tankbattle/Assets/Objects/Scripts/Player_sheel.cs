using UnityEngine;
using System;
using Unity.Netcode;

public class Player_sheel : NetworkBehaviour
{
    public GameObject target;
    private NetworkVariable<bool> networkBool = new NetworkVariable<bool>(
        false,                                          // 初期値
        NetworkVariableReadPermission.Everyone,     // 読み取り権限
        NetworkVariableWritePermission.Server        // 書き込み権限
    );
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        networkBool.OnValueChanged += (bool oldParam, bool newParam) =>
        {
            if(IsOwner){
                SellectSheel(newParam);
            }
        };
    }

    //プレイヤーの動きを封じる、開放する、現在の状況から切り替える
    public void OnSheel(){
        target.GetComponent<Player_move_net>().IsMove = false;
        target.GetComponent<Fire_Net>().IsFire = false;
        target.GetComponent<Fire_Net>().IsMine = false;
        target.GetComponent<Mouse_move_net>().AbleCameraMove = false;
        target.GetComponent<Wheel>().IsWheelable = false;
    }

    public void OffSheel(){
        target.GetComponent<Player_move_net>().IsMove = true;
        target.GetComponent<Fire_Net>().IsFire = true;
        target.GetComponent<Fire_Net>().IsMine = true;
        target.GetComponent<Mouse_move_net>().AbleCameraMove = true;
        target.GetComponent<Wheel>().IsWheelable = true;
    }

    public void SellectSheel(bool b){
        target.GetComponent<Player_move_net>().IsMove = b;
        target.GetComponent<Fire_Net>().IsFire = b;
        target.GetComponent<Fire_Net>().IsMine = b;
        target.GetComponent<Mouse_move_net>().AbleCameraMove = b;
        target.GetComponent<Wheel>().IsWheelable = b;
    }

    public void DecideButton(){
        if(IsHost){
            DecideBool();
        }else{
            DecideBoolRpc();
        }
    }

    public void CancelButton(){
        if(IsHost){
            CancelBool();
        }else{
            CancelBoolRpc();
        }
    }

    [Rpc(SendTo.Server)]
    public void DecideBoolRpc(){
        networkBool.Value = true;
    }

    void DecideBool(){
        networkBool.Value = true;
    }

    [Rpc(SendTo.Server)]
    public void CancelBoolRpc(){
        networkBool.Value = false;
    }

    void CancelBool(){
        networkBool.Value = false;
    }    
}
