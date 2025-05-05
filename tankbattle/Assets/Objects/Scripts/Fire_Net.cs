using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class Fire_Net : NetworkBehaviour
{
    public GameObject player_object;
    // public GameObject objB;
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
    public float mine_chargetime = 0f;
    public float cooltime_timer = 0f;
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
        if(have_bullet!=maxbullet){
            mine_chargetime += Time.deltaTime;
            if(cooltime_timer<=charge_cooltime){
                cooltime_timer = charge_cooltime;
            }else{
                cooltime_timer += Time.deltaTime;
            }
            mine_chargetime += Time.deltaTime;
            if(mine_chargetime>=bullet_charge){
                have_bullet++;
                mine_chargetime = 0f;
            }
        }
    }

    public void OnFire(InputAction.CallbackContext context){
        if(context.performed&&IsOwner&&have_bullet!=0&&charge_cooltime<=cooltime_timer&&IsFire){
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

    public void Setmine(InputAction.CallbackContext context){
        if(context.performed&&IsOwner&&have_mine!=0&&IsMine){
            Vector3 position = mine_position.transform.position;
            if(IsHost){
                MineSpawn(position);
            }else{
                MineSpawnRpc(position);
            }
            have_mine--;
        }
    }

    public void BulletSpawn(Vector3 pos){
        GameObject bullet_object = Instantiate (player_object,pos,Quaternion.identity);
        // NetworkObject f = bullet_object.GetComponent<NetworkObject>();
        bullet_object.GetComponent<NetworkObject>().Spawn();
        Rigidbody rig = bullet_object.GetComponent<Rigidbody>();
        bullet_object.transform.eulerAngles = machine_direction_object.transform.eulerAngles;
        rig.AddForce( target.transform.forward*player_speed,ForceMode.Impulse);   
    }

    public void MineSpawn(Vector3 pos){
        GameObject mine_object = Instantiate (mine,pos,Quaternion.identity);
        // NetworkObject f = mine_object.GetComponent<NetworkObject>();
        mine_object.GetComponent<NetworkObject>().Spawn();
    }

    [Rpc(SendTo.Server)]
    public void BulletSpawnRpc(Vector3 pos){
        BulletSpawn(pos);
    }
    
    [Rpc(SendTo.Server)]
    public void MineSpawnRpc(Vector3 pos){
        MineSpawn(pos);
    }
}
