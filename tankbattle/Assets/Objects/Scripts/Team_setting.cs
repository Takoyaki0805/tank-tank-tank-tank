using UnityEngine;

public class Team_setting : MonoBehaviour
{
    public GameObject spawnPosition;
    bool isSetup = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTeam(){
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        // cam = GameObject.FindGameObjectWithTag("MainCamera");
        if(spawnPosition.GetComponent<flagcolor>().flagred&&isSetup){
        //    mng.GetComponent<scoreboard>().isred = true;
            camera.GetComponent<color>().isred = true;
            camera.GetComponent<color>().isblue = false;
        }
        if(spawnPosition.GetComponent<flagcolor>().flagblue&&isSetup){
        //    mng.GetComponent<scoreboard>().isblue = true; 
            camera.GetComponent<color>().isblue = true; 
            camera.GetComponent<color>().isred = false;  
        } 
        isSetup = false;
    }

    public void SetPosition(GameObject g){
        spawnPosition = g;
    }
}
