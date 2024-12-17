using UnityEngine;

public class breakablewall : MonoBehaviour
{
    public AudioClip sound1;
    AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider c){
        if(c.gameObject.tag=="mineatkzone"){
            audioSource.PlayOneShot(sound1);
            Destroy(this.gameObject);
            // life -= c.gameObject.GetComponent<minedmg>().atk;
        }
    }
}
