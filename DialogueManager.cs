using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private string[] sentances;
    
    

    internal void StartDialogue(Dialogue dialogue)
    {
        sentances = dialogue.sentences;
    }
}
