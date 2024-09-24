using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mousemove : MonoBehaviour
{
    public GameObject tar;
    public GameObject cam;
    public GameObject atk;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Mouse X");
        cam.transform.RotateAround (tar.transform.position, Vector3.up, h*Time.deltaTime*speed);
        // if(IsOwner){
            atk.transform.RotateAround (tar.transform.position, Vector3.up, h*Time.deltaTime*speed);
        // }
    }

    void Awake(){
        cam = GameObject.FindWithTag("MainCamera");
    }
}
