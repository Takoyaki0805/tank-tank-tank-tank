using UnityEngine;

public class rotateTest : MonoBehaviour
{
    public GameObject tar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.RotateAround (tar.transform.position, Vector3.up, 1f);
        
    }
}
