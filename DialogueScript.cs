using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueScript : MonoBehaviour
{
    public Dialogue dialogue;

    bool dialogueOn;
    TextMeshProUGUI textMesh;
    Canvas dialogCanvas;

    private void Start()
    {
        dialogCanvas = GetComponentInChildren<Canvas>();
        textMesh = dialogCanvas.GetComponentInChildren<TextMeshProUGUI>();

        dialogCanvas.enabled = false;
        dialogueOn = false;

        StartCoroutine("DialougeManager");
    }

    public void DialogueOn()
    {
        dialogueOn = true;
        StartCoroutine("DialougeManager");
        dialogCanvas.enabled = true;
    }

    internal void DialogueOff()
    {
        dialogueOn = false;
        dialogCanvas.enabled = false;
    }

    IEnumerator DialougeManager()
    {
        while(dialogueOn)
        {
            textMesh.text = dialogue.sentences[UnityEngine.Random.Range(0, dialogue.sentences.Length)];
            yield return new WaitForSeconds(3f);
        }
           
    }


}
