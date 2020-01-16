using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthMonitor : MonoBehaviour
{
    public Image[] hearts;
    // Start is called before the first frame update

    //so we're not constantly repainting. Not sure if this has any performance improvements!
    int lastHealthChange = -1;
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        if(lastHealthChange != LevelManager.currentHealth)
        {
            //don't try and create more hearts than we have
            lastHealthChange = Mathf.Min(LevelManager.currentHealth, hearts.Length);
            

            for (int i = 0; i < hearts.Length; i++) 
            {
             if (i < lastHealthChange)
                 hearts [i].color = Color.white;
             else
                 hearts [i].color = Color.black;
         }
        }   
    }
}
