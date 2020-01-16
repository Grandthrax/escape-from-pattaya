using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        PlayerControls player = other.GetComponent<PlayerControls>();

        if(player != null){
            
            //contains because food spawned will have endings like "clone"
            if(gameObject.name.Contains("durian"))
            {
                player.ChangeHealth(-1);
                
            }else{
                player.ChangeHealth(1);
            }

            Destroy(gameObject);
            
        }
    }
}
