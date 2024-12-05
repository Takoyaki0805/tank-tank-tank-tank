using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using TMPro;

public class UIsys : NetworkBehaviour
{
    public GameObject tar;
    public GameObject[] mark;
    int bullet = 0;
    int mine = 0; 
    int maxhp = 0;
    int hp = 0;
    float bulletcharge = 0f;
    float nowbulletcharge = 0f;
    public FireNet code;
    public alive hpcode;
    public TMP_Text txt;
    public Slider hpbar;
    public Slider chargebar;
    GameObject g;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        try{
            // if(IsOwner){
                g = tar.transform.parent.parent.gameObject;
            // }
        }catch(MissingReferenceException e){
            return;
        }
        code = g.GetComponent<FireNet>();
        hpcode = g.GetComponent<alive>();
            // if(IsOwner){
                bullet = code.havbullet;
                mine = code.havmine;
                nowbulletcharge = code.chargetime;
                bulletcharge = code.bulletcharge;
                hp = hpcode.life;
                maxhp = hpcode.maxlife;
                hpbar.value = (float)hp/(float)maxhp;
                chargebar.value = (float)nowbulletcharge/(float)bulletcharge;
                Debug.Log(hp/maxhp);
                if(bullet==0){
                    foreach(GameObject j in mark){
                        j.SetActive(false);
                    }
                }
                if(bullet==1){
                    mark[0].SetActive(true);
                    mark[1].SetActive(false);
                }
                if(bullet==2){
                    foreach(GameObject j in mark){
                        j.SetActive(true);
                    }
                }
                txt.SetText(mine+"");
            // }   
    }
}
