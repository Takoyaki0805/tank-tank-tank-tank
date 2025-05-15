using UnityEngine;

public class Team_setting : MonoBehaviour
{
    public GameObject spawnPosition;
    bool isSetup = true;
    //チームを振り分ける
    public void SetTeam(){
        string camera_tag_name = "MainCamera";
        GameObject camera = GameObject.FindGameObjectWithTag(camera_tag_name);
        if(spawnPosition.GetComponent<Flag_color>().flag_red&&isSetup){
            camera.GetComponent<Team_color>().IsRed = true;
            camera.GetComponent<Team_color>().IsBlue = false;
        }
        if(spawnPosition.GetComponent<Flag_color>().flag_blue&&isSetup){
            camera.GetComponent<Team_color>().IsBlue = true; 
            camera.GetComponent<Team_color>().IsRed = false;  
        } 
        isSetup = false;
    }

    public void SetPosition(GameObject g){
        spawnPosition = g;
    }
}
