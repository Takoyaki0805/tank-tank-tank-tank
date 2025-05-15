using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Fire_off : MonoBehaviour
{
    public GameObject bullet_object;
    public GameObject target;
    //弾丸を発射する
    public void OnFire(InputAction.CallbackContext context){
        Vector3 position = target.transform.position;
        if(context.performed){
            Instantiate (bullet_object,position,Quaternion.identity);
        }
    }
}
