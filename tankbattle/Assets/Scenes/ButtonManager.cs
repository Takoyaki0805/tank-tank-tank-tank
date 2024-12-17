using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public AudioSource sound;
   void start()
   {
    sound = GetComponent<AudioSource>();
   }   
    public void tappedButton()
    {
        sound.PlayOneShot(sound.clip);
        Invoke(nameof(next),0.5f);
    }
    void next()
    {
        SceneManager.LoadScene("test");
    }
    private void OnDestroy(){
        CancelInvoke();
    }
}