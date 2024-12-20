using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class FireNet : NetworkBehaviour
{
    public GameObject obj;
    // public GameObject objB;
    public GameObject tar;
    public GameObject g; 
    public float speed = 100;
    public GameObject mine;
    public GameObject minepos;
    public int havbullet = 0;
    public int maxbullet = 2;
    public float bulletcharge = 5.0f;
    public float coolcharge = 1.5f;
    public int havmine = 3;
    public float chargetime = 0f;
    public float cooltimer = 0f;
    public AudioClip sound1;
    AudioSource audioSource;
    public bool ablemine = false;
    public bool ablefire = false;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(havbullet!=maxbullet){
            chargetime += Time.deltaTime;
            if(cooltimer<=coolcharge){
                cooltimer = coolcharge;
            }else{
                cooltimer += Time.deltaTime;
            }
            chargetime += Time.deltaTime;
            if(chargetime>=bulletcharge){
                havbullet++;
                chargetime = 0f;
            }
        }
    }

    public void OnFire(InputAction.CallbackContext context){
        if(context.performed&&IsOwner&&havbullet!=0&&coolcharge<=cooltimer&&ablefire){
            Vector3 pos = tar.transform.position;
            if(IsHost){
                spawn(pos);
            }else{
                SpawnRpc(pos);
            }
            havbullet--;
            cooltimer = 0f;
            audioSource.PlayOneShot(sound1);
        }
    }

    public void Setmine(InputAction.CallbackContext context){
        if(context.performed&&IsOwner&&havmine!=0&&ablemine){
            Vector3 pos = minepos.transform.position;
            if(IsHost){
                minespawn(pos);
            }else{
                mineSpawnRpc(pos);
            }
            havmine--;
        }
    }

    public void spawn(Vector3 pos){
        GameObject h = Instantiate (obj,pos,Quaternion.identity);
        NetworkObject f = h.GetComponent<NetworkObject>();
        h.GetComponent<NetworkObject>().Spawn();
        Rigidbody rig = h.GetComponent<Rigidbody>();
        h.transform.eulerAngles = g.transform.eulerAngles;
        rig.AddForce( tar.transform.forward*speed,ForceMode.Impulse);   
    }

    public void minespawn(Vector3 pos){
        GameObject h = Instantiate (mine,pos,Quaternion.identity);
        NetworkObject f = h.GetComponent<NetworkObject>();
        h.GetComponent<NetworkObject>().Spawn();
    }

    [Rpc(SendTo.Server)]
    public void SpawnRpc(Vector3 pos){
        spawn(pos);
    }
    
    [Rpc(SendTo.Server)]
    public void mineSpawnRpc(Vector3 pos){
        minespawn(pos);
    }
}
