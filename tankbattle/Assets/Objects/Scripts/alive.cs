using UnityEngine;
using Unity.Netcode;

public class alive : NetworkBehaviour
{
    public int maxlife = 100;
    public int life;
    public GameObject dea;
    bool onetime = true;
    GameObject cam;
    public ParticleSystem particle;
    public GameObject[] tankpolygon;
    // public int atk = 30;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private NetworkVariable<int> networklife = new NetworkVariable<int>(
        0,                                          // 初期値
        NetworkVariableReadPermission.Everyone,     // 読み取り権限
        NetworkVariableWritePermission.Server        // 書き込み権限
        );
    void Start()
    {
        life = maxlife;
        cam = GameObject.FindWithTag("MainCamera");
        networklife.Value = maxlife;
    }

    // Update is called once per frame
    void Update()
    {
        life = networklife.Value;
        if(life<=0&&onetime){
            isGameOver();
            onetime = false;
        }
    }

    void OnCollisionEnter(Collision c){
        if(c.gameObject.tag=="ball"){
            Destroy(c.gameObject);
            life -= c.gameObject.GetComponent<bulletmove>().atk;
            networklife.Value = life;
        }
        if(life<=0&&onetime){
            isGameOver();
            onetime = false;
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
        if(life<=0&&onetime){
            isGameOver();
            onetime = false;
        }
    }    

    void isGameOver(){
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
        foreach (GameObject g in tankpolygon){
            g.gameObject.layer = LayerMask.NameToLayer("Unseen");
        }

        GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
        GameObject mn = GameObject.FindGameObjectWithTag("spawnMNG");
        if(IsOwner&&cam.GetComponent<color>().isred){
            if(IsHost){
                mn.GetComponent<scoreboard>().redscore();
            }else{
                mn.GetComponent<scoreboard>().redscoreRpc();
            }
        }
        if(IsOwner&&cam.GetComponent<color>().isblue){
            if(IsHost){
                mn.GetComponent<scoreboard>().bluescore();
            }else{
                mn.GetComponent<scoreboard>().bluescoreRpc();
            }            
        }
        // cam.GetComponent<playersheel>().sheel();
        // this.gameObject.SetActive(false);
        // cam.SetActive(false);
        // cam.transform.parent = d.transform;
        // cam.transform.localPosition = Vector3.up*5f;
    }

    void isone(){
        onetime = true;
    }
}
