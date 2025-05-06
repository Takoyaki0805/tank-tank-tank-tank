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
        if(spawnPosition.GetComponent<Flag_color>().flag_red&&isSetup){
        //    mng.GetComponent<scoreboard>().isred = true;
            camera.GetComponent<Team_color>().IsRed = true;
            camera.GetComponent<Team_color>().IsBlue = false;
        }
        if(spawnPosition.GetComponent<Flag_color>().flag_blue&&isSetup){
        //    mng.GetComponent<scoreboard>().isblue = true; 
            camera.GetComponent<Team_color>().IsBlue = true; 
            camera.GetComponent<Team_color>().IsRed = false;  
        } 
        isSetup = false;
    }

    public void SetPosition(GameObject g){
        spawnPosition = g;
    }
}
