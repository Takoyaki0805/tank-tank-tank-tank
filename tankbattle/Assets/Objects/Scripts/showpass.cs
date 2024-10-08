using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class showpass : MonoBehaviour
{
    public GameObject tar;
    public TMP_Text sub;
    // Start is called before the first frame update
    void Start()
    {
        tar = GameObject.FindWithTag("NET");
    }

    // Update is called once per frame
    void Update()
    {
        sub.SetText(tar.GetComponent<RelayTest>().joinCode);
    }
}
