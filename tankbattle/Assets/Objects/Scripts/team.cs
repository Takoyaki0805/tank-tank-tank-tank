using UnityEngine;

public class team : MonoBehaviour
{
    public GameObject spawnpos;
    bool setup = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setteam(){
        GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
        // cam = GameObject.FindGameObjectWithTag("MainCamera");
        if(spawnpos.GetComponent<flagcolor>().flagred&&setup){
        //    mng.GetComponent<scoreboard>().isred = true;
            cam.GetComponent<color>().isred = true;
            cam.GetComponent<color>().isblue = false;
            Debug.Log("aaaaaaa"); 
        }
        if(spawnpos.GetComponent<flagcolor>().flagblue&&setup){
        //    mng.GetComponent<scoreboard>().isblue = true; 
            cam.GetComponent<color>().isblue = true; 
            cam.GetComponent<color>().isred = false; 
            Debug.Log("bbbbbbbb"); 
        } 
        setup = false;
    }

    public void setpos(GameObject g){
        spawnpos = g;
        // setup = true;
    }
}
