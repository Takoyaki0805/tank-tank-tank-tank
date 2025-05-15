using UnityEngine;
using Unity.Netcode;

public class Player_life : NetworkBehaviour
{
    public int maxlife = 100;
    public int life;
    public GameObject deadObject;
    bool isOnetime = true;
    GameObject cam;
    public ParticleSystem particle;
    public GameObject[] tank_polygon;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    int life_gameover = 0;
    string camera_tag_name = "MainCamera";
    string bullet_tag_name = "ball";
    string mine_area_tag_name = "mineatkzone";
    string manager_tag_name = "spawnMNG";
    string Unseen_layer_name = "Unseen";
    private NetworkVariable<int> networklife = new NetworkVariable<int>(
        100,                                          // 初期値(仮としてHPを100に設定)
        NetworkVariableReadPermission.Everyone,     // 読み取り権限
        NetworkVariableWritePermission.Server        // 書き込み権限
        );
    //初期HPを設定して同期
    void Start()
    {
        life = maxlife;
        cam = GameObject.FindWithTag(camera_tag_name);
        if(IsHost){
            networklife.Value = maxlife;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //現在のHPを同期する
        life = networklife.Value;
        if(life<=life_gameover&&isOnetime){
            IsGameOver();
            isOnetime = false;
        }
    }

    void OnCollisionEnter(Collision c){
        if(c.gameObject.tag == bullet_tag_name){
            Destroy(c.gameObject);
            life -= c.gameObject.GetComponent<Bullet_move>().attack;
            networklife.Value = life;
        }
        if(life<=0&&isOnetime){
            IsGameOver();
            isOnetime = false;
        }
    }

    void OnTriggerEnter(Collider c){
        if(c.gameObject.tag == bullet_tag_name){
            Destroy(c.gameObject);
            life -= c.gameObject.GetComponent<Bullet_move>().attack;
            networklife.Value = life;
        }
        if(c.gameObject.tag == mine_area_tag_name){
            // Destroy(c.gameObject);
            life -= c.gameObject.GetComponent<Mine_damage>().mine_atk;
            networklife.Value = life;
        }
        if(life<=life_gameover&&isOnetime){
            IsGameOver();
            isOnetime = false;
        }
    }    

    //ゲームオーバー時の処理
    void IsGameOver(){
        // パーティクルシステムのインスタンスを生成する。
		ParticleSystem newParticle = Instantiate(particle);
		// パーティクルの発生場所をこのスクリプトをアタッチしているGameObjectの場所にする。
		newParticle.transform.position = this.transform.position;
		// パーティクルを発生させる。
		newParticle.Play();
		// インスタンス化したパーティクルシステムのGameObjectを5秒後に削除する。(任意)
		// ※第一引数をnewParticleだけにするとコンポーネントしか削除されない。
        float effect_kill_time = 5.0f
		Destroy(newParticle.gameObject, effect_kill_time);
        //爆発エフェクトと同時に自機を見えなくさせる
        foreach (GameObject g in tank_polygon){
            g.gameObject.layer = LayerMask.NameToLayer(Unseen_layer_name);
        }
        //スコアを減らす
        GameObject cam = GameObject.FindGameObjectWithTag(camera_tag_name);
        GameObject manager = GameObject.FindGameObjectWithTag(manager_tag_name);
        if(IsOwner&&this.gameObject.GetComponent<Team_color>().IsRed){
            if(IsHost){
                manager.GetComponent<Score_board>().RedScore();
            }else{
                manager.GetComponent<Score_board>().RedScoreRpc();
            }
        }
        if(IsOwner&&this.gameObject.GetComponent<Team_color>().IsBlue){
            if(IsHost){
                manager.GetComponent<Score_board>().BlueScore();
            }else{
                manager.GetComponent<Score_board>().BlueScoreRpc();
            }            
        }
    }

    void DoOnetime(){
        isOnetime = true;
    }
}
-