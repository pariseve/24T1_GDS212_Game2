using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownScript : MonoBehaviour
{

    DialogueTrigger dialogueTrigger;

    private void Start()
    {
        dialogueTrigger = FindAnyObjectByType<DialogueTrigger>();
    }

    public void bonnieIntialComplete()
    {
        dialogueTrigger.ToggleDialogue(true);
        PlayerPrefs.SetInt("HasAlreadySpokenToBonnie", 1);
    }

    public void childrenInitialComplete()
    {
        dialogueTrigger.ToggleDialogue(true);
        PlayerPrefs.SetInt("HasAlreadySpokenToChildren", 1);
    }
}
