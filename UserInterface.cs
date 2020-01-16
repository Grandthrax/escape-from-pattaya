using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserInterface : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        TextMeshProUGUI textMesh = GetComponentInChildren<TextMeshProUGUI>();
        textMesh.text = "Level " + LevelManager.level;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
