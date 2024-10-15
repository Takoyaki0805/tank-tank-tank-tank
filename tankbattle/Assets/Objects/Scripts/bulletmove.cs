using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletmove : MonoBehaviour
{
    public GameObject tar; 
    GameObject bt; 
    Rigidbody rig;
    public float speed = 100;
    // Start is called before the first frame update
    void Start()
    {
        rig = tar.GetComponent<Rigidbody>();
        bt = GameObject.FindWithTag("playerbullet");
        this.transform.localEulerAngles = bt.transform.localEulerAngles;
        Debug.Log(bt.transform.localEulerAngles);
        rig.AddForce( bt.transform.forward*speed,ForceMode.Impulse);    

    }

    // Update is called once per frame
    void Update()
    {
    }
}
