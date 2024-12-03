using UnityEngine;

public class mouse : MonoBehaviour
{
    bool ishidden = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.visible = ishidden;
        Cursor.lockState = CursorLockMode.Confined;
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void cursorHide(){
        ishidden = false;
    }

    public void cursorShow(){
        ishidden = true;
    }
}
