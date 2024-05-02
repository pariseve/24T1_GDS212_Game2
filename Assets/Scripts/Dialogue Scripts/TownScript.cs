using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownScript : MonoBehaviour
{
    [SerializeField] private DialogueTrigger bonnieDialogueTrigger;
    [SerializeField] private DialogueTrigger childrenDialogueTrigger;
    [SerializeField] private DialogueTrigger stallDialogueTrigger;
    [SerializeField] private DialogueTrigger theTailorTrigger;

    private PlayerPrefsManager prefsManager;

    public GameObject stallImage;
    public Sprite emptyStall;

    public GameObject bonnieTrigger;
    public GameObject bonnieImage;

    public GameObject childrenImage;
    public GameObject childrenTrigger;

    public GameObject churchTrigger;
    public GameObject alleyTrigger;

    private bool hasMetBonnie;
    private bool hasMetChildren;

    private bool hasBonnieLeft;
    private bool hasSweets;

    private bool hasOpenedAlley;

    private void Start()
    {
        prefsManager = PlayerPrefsManager.Instance;
        hasMetBonnie = prefsManager.HasMetBonnie();
        if (hasMetBonnie)
        {
            bonnieDialogueTrigger.ToggleAltDialogue(true);
        }

        hasMetChildren = prefsManager.HasMetChildren();
        hasSweets = prefsManager.HasGotSweets();

        if(hasSweets && hasMetChildren)
        {
            childrenDialogueTrigger.ToggleFinalDialogue(true);
        }
        else if (hasMetChildren && !hasSweets)
        {
            childrenDialogueTrigger.ToggleAltDialogue(true);
        }

        hasOpenedAlley = prefsManager.HasOpenedAlleyEntry();
        if (hasOpenedAlley)
        {
            AlleyTriggerEnabled();
        }

        if (PlayerPrefs.GetInt("StallSoldOut", 0) == 1)
        {
            StallSoldOut();
            //PlayerPrefs.SetInt("StallSoldOut", 0);
        }

        hasBonnieLeft = prefsManager.HasBonnieLeft();
        if (hasBonnieLeft)
        {
            BonnieLeaves();
        }
    }

    //ensures that the intial dialogue introducing bonnie does not play twice, switches to alt dialogue graph
    public void BonnieIntialComplete()
    {
        Debug.Log("Bonnie's intial dialogue is complete");
        prefsManager.SetHasMetBonnie(true);
        bonnieDialogueTrigger.ToggleAltDialogue(true);
    }

    //triggers the final dialogue graph with bonnie
    public void BonnieFinalInteraction()
    {
        Debug.Log("Bonnie can now sell sweets");
        bonnieDialogueTrigger.ToggleAltDialogue(false);
        bonnieDialogueTrigger.ToggleFinalDialogue(true);

    }

    //ensures that the intial dialogue introducing the children NPCs does not play twice, switches to alt dialogue graph
    public void ChildrenInitialComplete()
    {
        Debug.Log("Initial dialogue with children complete");
        prefsManager.SetHasMetChildren(true);
        childrenDialogueTrigger.ToggleAltDialogue(true);
    }

    //triggers the final dialogue graph with the children NPCs
    public void ChildrenFinalInteraction()
    {
        if (hasMetChildren)
        {
            Debug.Log("Final dialogue with children available");
            childrenDialogueTrigger.ToggleAltDialogue(false);
            childrenDialogueTrigger.ToggleFinalDialogue(true);
        }
        else
        {
            Debug.Log("Player must talk to children first");
        }
    }

    //updates the stall sprite and triggers the final dialogue graph with bonnie
    public void StallSoldOut()
    {
        Image imageComponent = stallImage.GetComponent<Image>();
        imageComponent.sprite = emptyStall;

        stallDialogueTrigger.ToggleAltDialogue(true);

        BonnieFinalInteraction();
    }

    //activates the trigger to the church scene and removes the stall image and trigger
    public void ChurchTriggerEnabled()
    {
        stallImage.gameObject.SetActive(false);
        //bonnieImage.gameObject.SetActive(false);
        //bonnieTrigger.gameObject.SetActive(false);

        churchTrigger.gameObject.SetActive(true);
    }

    //activates the trigger to the alleyway scene and removes the children NPC image and trigger
    public void AlleyTriggerEnabled()
    {
        Debug.Log("Alley triggered");
        childrenImage.gameObject.SetActive(false);
        childrenTrigger.gameObject.SetActive(false);
        alleyTrigger.gameObject.SetActive(true);

        prefsManager.SetHasGotSweets(false);
        prefsManager.SetOpenedAlleyEntry(true);
    }

    //the player now possesses 'sweets' and unlocks the final dialogue graph with the children NPCs
    public void GetSweets()
    {
        prefsManager.SetHasGotSweets(true);
        ChildrenFinalInteraction();
    }

    //bonnie's sprite and trigger are set to inactive (she 'leaves' the scene)
    public void BonnieLeaves()
    {
        prefsManager.SetHasBonnieLeft(true);

        bonnieImage.gameObject.SetActive(false);
        bonnieTrigger.gameObject.SetActive(false);
    }
}
