using UnityEngine;

public class alive : MonoBehaviour
{
    public int maxlife = 100;
    int life;
    // public int atk = 30;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        life = maxlife;        
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
        if(life<=0){
            isGameOver();
        }
    }

    void isGameOver(){
        this.transform.localScale = Vector3.zero;
        this.gameObject.SetActive(false);
    }
}
