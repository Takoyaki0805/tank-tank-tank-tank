using UnityEngine;

public class Breakable_wall : MonoBehaviour
{
    public AudioClip sound;
    AudioSource audio_source;
    string mine_area_tag = "mineatkzone";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audio_source = this.gameObject.GetComponent<AudioSource>();
    }
    //地雷の爆風にあたったら壁を破壊する
    void OnTriggerEnter(Collider c){
        if(c.gameObject.tag == mine_area_tag){
            audio_source.PlayOneShot(sound);
            Destroy(this.gameObject);
        }
    }
}
