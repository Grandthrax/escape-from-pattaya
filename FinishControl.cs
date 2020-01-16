using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishControl : MonoBehaviour
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
        
        PlayerControls controller = other.GetComponent<PlayerControls>();

        //make sure it isn't something else enterring the trigger
        if(controller != null)
        {
            LevelManager.level++;
            SceneManager.LoadScene("Level");
        }
    }

}
