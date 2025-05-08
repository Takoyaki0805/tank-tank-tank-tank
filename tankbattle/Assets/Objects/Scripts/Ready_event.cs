using UnityEngine;

public class Ready_event : MonoBehaviour
{
    public GameObject ready;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowReady(){
        ready.GetComponent<Session_event>().PlayerBoot();
    }
}
