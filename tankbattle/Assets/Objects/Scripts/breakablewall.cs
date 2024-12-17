using UnityEngine;

public class breakablewall : MonoBehaviour
{
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
            Destroy(this.gameObject);
            // life -= c.gameObject.GetComponent<minedmg>().atk;
        }
    }
}
