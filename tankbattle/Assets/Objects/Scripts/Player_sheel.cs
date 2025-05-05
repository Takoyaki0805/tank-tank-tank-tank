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

    // Update is called once per frame
    void Update()
    {
    }

    public void OnSheel(){
        target.GetComponent<NewMove>().ablemove = false;
        target.GetComponent<FireNet>().ablefire = false;
        target.GetComponent<FireNet>().ablemine = false;
        target.GetComponent<mousemoveNet>().ablecameramove = false;
        target.GetComponent<wheel>().wheelable = false;
    }

    public void OffSheel(){
        target.GetComponent<NewMove>().ablemove = true;
        target.GetComponent<FireNet>().ablefire = true;
        target.GetComponent<FireNet>().ablemine = true;
        target.GetComponent<mousemoveNet>().ablecameramove = true;
        target.GetComponent<wheel>().wheelable = true;
    }

    public void SellectSheel(bool b){
        target.GetComponent<NewMove>().ablemove = b;
        target.GetComponent<FireNet>().ablefire = b;
        target.GetComponent<FireNet>().ablemine = b;
        target.GetComponent<mousemoveNet>().ablecameramove = b;
        target.GetComponent<wheel>().wheelable = b;
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
