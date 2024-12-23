using UnityEngine;
using System;
using Unity.Netcode;

public class playersheel : NetworkBehaviour
{
    public GameObject tar;
    private NetworkVariable<bool> networkbool = new NetworkVariable<bool>(
        false,                                          // 初期値
        NetworkVariableReadPermission.Everyone,     // 読み取り権限
        NetworkVariableWritePermission.Server        // 書き込み権限
    );
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        networkbool.OnValueChanged += (bool oldParam, bool newParam) =>
        {
            if(IsOwner){
                selsheel(newParam);
            }
            // ここに新旧の変数を利用した処理を書く
            // oldがいらない場合は使わなくてもOK
        };
    }

    // Update is called once per frame
    void Update()
    {


        // try{
        //     tar = this.gameObject.transform.parent.parent.gameObject;
        // }catch(NullReferenceException e){

        // }
        // dissheel();
    }

    public void sheel(){
        tar.GetComponent<NewMove>().ablemove = false;
        tar.GetComponent<FireNet>().ablefire = false;
        tar.GetComponent<FireNet>().ablemine = false;
        tar.GetComponent<mousemoveNet>().ablecameramove = false;
        tar.GetComponent<wheel>().wheelable = false;
    }

    public void dissheel(){
        tar.GetComponent<NewMove>().ablemove = true;
        tar.GetComponent<FireNet>().ablefire = true;
        tar.GetComponent<FireNet>().ablemine = true;
        tar.GetComponent<mousemoveNet>().ablecameramove = true;
        tar.GetComponent<wheel>().wheelable = true;
    }

    public void selsheel(bool b){
        tar.GetComponent<NewMove>().ablemove = b;
        tar.GetComponent<FireNet>().ablefire = b;
        tar.GetComponent<FireNet>().ablemine = b;
        tar.GetComponent<mousemoveNet>().ablecameramove = b;
        tar.GetComponent<wheel>().wheelable = b;
    }

    public void button(){
        if(IsHost){
            boolC();
        }else{
            boolRpc();
        }
    }

    [Rpc(SendTo.Server)]
    public void boolRpc(){
        networkbool.Value = true;
    }

    void boolC(){
        networkbool.Value = true;
    }


}
