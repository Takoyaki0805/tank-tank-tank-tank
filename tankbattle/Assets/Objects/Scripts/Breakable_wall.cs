using UnityEngine;

public class Breakable_wall : MonoBehaviour
{
    public AudioClip sound;
    AudioSource audio_source;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audio_source = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //地雷の爆風にあたったら壁を破壊する
    void OnTriggerEnter(Collider c){
        if(c.gameObject.tag=="mineatkzone"){
            audio_source.PlayOneShot(sound);
            Destroy(this.gameObject);
        }
    }
}
