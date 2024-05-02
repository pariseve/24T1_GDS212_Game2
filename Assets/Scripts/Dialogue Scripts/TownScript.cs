using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownScript : MonoBehaviour
{

    [SerializeField] private DialogueTrigger bonnieDialogueTrigger;
    [SerializeField] private DialogueTrigger childrenDialogueTrigger;
    [SerializeField] private DialogueTrigger stallDialogueTrigger;

    private PlayerPrefsManager prefsManager;

    private bool hasMetBonnie;
    private bool hasMetChildren;

    private void Start()
    {
        prefsManager = PlayerPrefsManager.Instance;
        bool hasMetBonnie = prefsManager.HasMetBonnie();
        if (hasMetBonnie)
        {
            bonnieDialogueTrigger.ToggleAltDialogue(true);
        }

        bool hasMetChildren = prefsManager.HasMetChildren();
        if (hasMetChildren)
        {
            childrenDialogueTrigger.ToggleAltDialogue(true);
        }
    }


    public void bonnieIntialComplete()
    {
        prefsManager.SetHasMetBonnie(true);
        bonnieDialogueTrigger.ToggleAltDialogue(true);
    }

    public void bonnieFinalInteraction()
    {

    }

    public void childrenInitialComplete()
    {
        prefsManager.SetHasMetChildren(true);
        childrenDialogueTrigger.ToggleAltDialogue(true);
    }

    public void childrenFinalInteraction()
    {

    }
}
