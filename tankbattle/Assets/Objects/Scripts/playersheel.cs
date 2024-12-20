using UnityEngine;
using System;

public class playersheel : MonoBehaviour
{
    public GameObject tar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        try{
            tar = this.gameObject.transform.parent.parent.gameObject;
        }catch(NullReferenceException e){

        }
        dissheel();
    }

    public void sheel(){
        tar.GetComponent<NewMove>().ablemove = false;
        tar.GetComponent<FireNet>().ablefire = false;
        tar.GetComponent<FireNet>().ablemine = false;
        tar.GetComponent<mousemoveNet>().ablecameramove = false;
        tar.GetComponent<wheel>().wheelable = false;
    }

    public void dissheel(){
        tar.GetComponent<NewMove>().ablemove = true;
        tar.GetComponent<FireNet>().ablefire = true;
        tar.GetComponent<FireNet>().ablemine = true;
        tar.GetComponent<mousemoveNet>().ablecameramove = true;
        tar.GetComponent<wheel>().wheelable = true;
    }
}
