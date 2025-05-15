using UnityEngine;
using Unity.Netcode;
using System.Collections;

public class mine : NetworkBehaviour
{
    float mine_timer=0;
    public float mine_pheseA=5.0f;
    public float mine_pheseB=8.0f;
    public float mine_pheseC=11.0f;
    public int mine_attack = 100;
    public GameObject bomb;
    Animator anim;
    public ParticleSystem particle;
    string bomb_anim_name = "alarm";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //アニメを起動する
        anim = this.gameObject.GetComponent<Animator>();
        bomb.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //時間経過で爆破判定を出し、エフェクトを出す
        mine_timer += Time.deltaTime;
        if(mine_timer>=mine_pheseA){
            anim.SetBool(bomb_anim_name,true);
        }
        if(mine_timer>=mine_pheseB){
            bomb.SetActive(true);
            // パーティクルシステムのインスタンスを生成する。
			ParticleSystem newParticle = Instantiate(particle);
			// パーティクルの発生場所をこのスクリプトをアタッチしているGameObjectの場所にする。
			newParticle.transform.position = this.transform.position;
			// パーティクルを発生させる。
			newParticle.Play();
			// インスタンス化したパーティクルシステムのGameObjectを5秒後に削除する。(任意)
			// ※第一引数をnewParticleだけにするとコンポーネントしか削除されない。
        }
        if(mine_timer>=mine_pheseC){
            //地雷を出したプレイヤーがゲームホスト化クライアントかで切り替える
            if(IsHost){
                DisSpawnMine();
            }else{
                DisSpawnMineRpc();
            }
        }
    }

    //地雷オブジェクトを破棄する
    public void DisSpawnMine(){
        this.gameObject.GetComponent<NetworkObject>().Despawn();
    }

    //ゲームホストに破棄を依頼する
    [Rpc(SendTo.Server)]
    public void DisSpawnMineRpc(){
        DisSpawnMine();
    }

}
