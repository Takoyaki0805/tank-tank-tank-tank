using UnityEngine;

public class Readypipe : MonoBehaviour
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

    public void showready(){
        ready.GetComponent<ReadySet>().boot();
    }
}
