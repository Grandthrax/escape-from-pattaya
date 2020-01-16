using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) | Input.GetKeyDown(KeyCode.Return))
        {
            LevelManager.level = 1;
            LevelManager.currentHealth = LevelManager.maxHealth;
            SceneManager.LoadScene("Level");
        }   
    }
}
