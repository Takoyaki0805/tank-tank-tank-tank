using UnityEngine;
using Unity.Netcode;

public class Player_life : NetworkBehaviour
{
    public int maxlife = 100;
    public int life;
    public GameObject deadObject;
    bool isOnetime = true;
    GameObject camera;
    public ParticleSystem particle;
    public GameObject[] tankPolygon;
    // public int atk = 30;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private NetworkVariable<int> networklife = new NetworkVariable<int>(
        100,                                          // 初期値
        NetworkVariableReadPermission.Everyone,     // 読み取り権限
        NetworkVariableWritePermission.Server        // 書き込み権限
        );
    void Start()
    {
        life = maxlife;
        camera = GameObject.FindWithTag("MainCamera");
        if(IsHost){
        networklife.Value = maxlife;
        }
    }

    // Update is called once per frame
    void Update()
    {
        life = networklife.Value;
        if(life<=0&&isOnetime){
            IsGameOver();
            isOnetime = false;
        }
    }

    void OnCollisionEnter(Collision c){
        if(c.gameObject.tag=="ball"){
            Destroy(c.gameObject);
            life -= c.gameObject.GetComponent<bulletmove>().atk;
            networklife.Value = life;
        }
        if(life<=0&&isOnetime){
            IsGameOver();
            isOnetime = false;
        }
    }

    void OnTriggerEnter(Collider c){
        if(c.gameObject.tag=="ball"){
            Destroy(c.gameObject);
            life -= c.gameObject.GetComponent<bulletmove>().atk;
            networklife.Value = life;
        }
        if(c.gameObject.tag=="mineatkzone"){
            // Destroy(c.gameObject);
            life -= c.gameObject.GetComponent<minedmg>().atk;
            networklife.Value = life;
        }
        if(life<=0&&isOnetime){
            IsGameOver();
            isOnetime = false;
        }
    }    

    void IsGameOver(){
            // パーティクルシステムのインスタンスを生成する。
			ParticleSystem newParticle = Instantiate(particle);
			// パーティクルの発生場所をこのスクリプトをアタッチしているGameObjectの場所にする。
			newParticle.transform.position = this.transform.position;
			// パーティクルを発生させる。
			newParticle.Play();
			// インスタンス化したパーティクルシステムのGameObjectを5秒後に削除する。(任意)
			// ※第一引数をnewParticleだけにするとコンポーネントしか削除されない。
			Destroy(newParticle.gameObject, 5.0f);
        // GameObject d = Instantiate(dea,this.transform.position,Quaternion.EulerAngles(this.transform.localEulerAngles));
        // this.transform.localScale = Vector3.zero;
        foreach (GameObject gameObject in tank_polygon){
            gameObject.gameObject.layer = LayerMask.NameToLayer("Unseen");
        }

        GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
        GameObject manager = GameObject.FindGameObjectWithTag("spawnMNG");
        if(IsOwner&&this.gameObject.GetComponent<color>().isred){
            if(IsHost){
                manager.GetComponent<scoreboard>().redscore();
            }else{
                manager.GetComponent<scoreboard>().redscoreRpc();
            }
        }
        if(IsOwner&&this.gameObject.GetComponent<color>().isblue){
            if(IsHost){
                manager.GetComponent<scoreboard>().bluescore();
            }else{
                manager.GetComponent<scoreboard>().bluescoreRpc();
            }            
        }
    }

    void DoOnetime(){
        isOnetime = true;
    }
}
