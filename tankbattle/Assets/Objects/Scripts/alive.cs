using UnityEngine;

public class alive : MonoBehaviour
{
    public int maxlife = 100;
    public int life;
    public GameObject dea;
    bool onetime = true;
    GameObject cam;
    // public int atk = 30;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        life = maxlife;
        cam = GameObject.FindWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision c){
        if(c.gameObject.tag=="ball"){
            Destroy(c.gameObject);
            life -= c.gameObject.GetComponent<bulletmove>().atk;
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
        }
        if(c.gameObject.tag=="mineatkzone"){
            // Destroy(c.gameObject);
            life -= c.gameObject.GetComponent<minedmg>().atk;
        }
        if(life<=0&&onetime){
            isGameOver();
            onetime = false;
        }
    }    

    void isGameOver(){
        GameObject d = Instantiate(dea,this.transform.position,Quaternion.EulerAngles(this.transform.localEulerAngles));
        this.transform.localScale = Vector3.zero;
        // this.gameObject.SetActive(false);
        // cam.SetActive(false);
        cam.transform.parent = d.transform;
        cam.transform.localPosition = Vector3.up*5f;
    }

    void isone(){
        onetime = true;
    }
}
