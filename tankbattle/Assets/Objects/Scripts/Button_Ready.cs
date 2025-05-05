using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;

public class Button_Ready : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonSwitch(){
        GameObject target = this.gameObject.transform.parent.parent.gameObject;
        target.GetComponent<playersheel>().button();
    }

    public void ButtonSwitchFalse(){
        GameObject target = this.gameObject.transform.parent.parent.gameObject;
        target.GetComponent<playersheel>().fbutton();
    }


}

