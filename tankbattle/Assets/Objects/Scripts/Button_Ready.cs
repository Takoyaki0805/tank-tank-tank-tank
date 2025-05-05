using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class button : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void buttonswitch(){
        GameObject tar = this.gameObject.transform.parent.parent.gameObject;
        tar.GetComponent<playersheel>().button();
    }

    public void fbuttonswitch(){
        GameObject tar = this.gameObject.transform.parent.parent.gameObject;
        tar.GetComponent<playersheel>().fbutton();
    }


}

