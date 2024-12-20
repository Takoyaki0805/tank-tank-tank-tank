using UnityEngine;
using Unity.Netcode;
using System.Collections;

public class mine : NetworkBehaviour
{
    float timer=0;
    public float pheseA=5.0f;
    public float pheseB=8.0f;
    public float pheseC=11.0f;
    public int atk = 100;
    public GameObject bomb;
    Animator anim;
    public ParticleSystem particle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
        bomb.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        // Debug.Log(pheseA);
        if(timer>=pheseA){
            anim.SetBool("alarm",true);
        }
        if(timer>=pheseB){
            bomb.SetActive(true);
            // パーティクルシステムのインスタンスを生成する。
			ParticleSystem newParticle = Instantiate(particle);
			// パーティクルの発生場所をこのスクリプトをアタッチしているGameObjectの場所にする。
			newParticle.transform.position = this.transform.position;
			// パーティクルを発生させる。
			newParticle.Play();
			// インスタンス化したパーティクルシステムのGameObjectを5秒後に削除する。(任意)
			// ※第一引数をnewParticleだけにするとコンポーネントしか削除されない。
			// Destroy(newParticle.gameObject, 5.0f);
        }
        if(timer>=pheseC){
            if(IsHost){
                dismine();
            }else{
                dismineRpc();
            }
        }
    }

    public void dismine(){
        this.gameObject.GetComponent<NetworkObject>().Despawn();
    }

    [Rpc(SendTo.Server)]
    public void dismineRpc(){
        dismine();
    }

}
