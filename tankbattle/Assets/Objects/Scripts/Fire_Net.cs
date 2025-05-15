using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class Fire_Net : NetworkBehaviour
{
    public GameObject player_object;
    public GameObject target;
    public GameObject machine_direction_object; 
    public float player_speed = 100;
    public GameObject mine;
    public GameObject mine_position;
    public int have_bullet = 0;
    public int maxbullet = 2;
    public float bullet_charge = 5.0f;
    public float charge_cooltime = 1.5f;
    public int have_mine = 3;
    public float bullet_chargetime = 0f;
    public float cooltime_timer = 0f;
    int have_none = 0;
    public AudioClip sound_file;
    AudioSource audio_source;
    public bool IsMine = false;
    public bool IsFire = false;

    // Start is called before the first frame update
    void Start()
    {
        audio_source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //弾丸が上限数でなければチャージを開始する
        if(have_bullet!=maxbullet){
            bullet_chargetime += Time.deltaTime;
            if(cooltime_timer<=charge_cooltime){
                cooltime_timer = charge_cooltime;
            }else{
                cooltime_timer += Time.deltaTime;
            }
            bullet_chargetime += Time.deltaTime;
            if(bullet_chargetime>=bullet_charge){
                have_bullet++;
                bullet_chargetime = 0f;
            }
        }
    }

    //射撃をする
    public void OnFire(InputAction.CallbackContext context){
        if(context.performed&&IsOwner&&have_bullet!=have_none&&charge_cooltime<=cooltime_timer&&IsFire){
            Vector3 position = target.transform.position;
            if(IsHost){
                BulletSpawn(position);
            }else{
                BulletSpawnRpc(position);
            }
            have_bullet--;
            cooltime_timer = 0f;
            audio_source.PlayOneShot(sound_file);
        }
    }

    //地雷を設置する
    public void Setmine(InputAction.CallbackContext context){
        if(context.performed&&IsOwner&&have_mine!=have_none&&IsMine){
            Vector3 position = mine_position.transform.position;
            if(IsHost){
                MineSpawn(position);
            }else{
                MineSpawnRpc(position);
            }
            have_mine--;
        }
    }

    //オブジェクトを生成する
    public void BulletSpawn(Vector3 pos){
        GameObject bullet_object = Instantiate (player_object,pos,Quaternion.identity);
        bullet_object.GetComponent<NetworkObject>().Spawn();
        Rigidbody rig = bullet_object.GetComponent<Rigidbody>();
        bullet_object.transform.eulerAngles = machine_direction_object.transform.eulerAngles;
        rig.AddForce( target.transform.forward*player_speed,ForceMode.Impulse);   
    }

    public void MineSpawn(Vector3 pos){
        GameObject mine_object = Instantiate (mine,pos,Quaternion.identity);
        mine_object.GetComponent<NetworkObject>().Spawn();
    }

    //ゲームホストに生成を依頼する
    [Rpc(SendTo.Server)]
    public void BulletSpawnRpc(Vector3 pos){
        BulletSpawn(pos);
    }
    
    [Rpc(SendTo.Server)]
    public void MineSpawnRpc(Vector3 pos){
        MineSpawn(pos);
    }
}
