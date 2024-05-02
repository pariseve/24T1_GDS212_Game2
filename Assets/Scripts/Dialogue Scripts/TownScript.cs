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
        if (hasMetChildren)
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



    public void BonnieIntialComplete()
    {
        prefsManager.SetHasMetBonnie(true);
        bonnieDialogueTrigger.ToggleAltDialogue(true);
    }

    public void BonnieFinalInteraction()
    {
        Debug.Log("Bonnie can now sell sweets");
        bonnieDialogueTrigger.ToggleAltDialogue(false);
        bonnieDialogueTrigger.ToggleFinalDialogue(true);

    }

    public void ChildrenInitialComplete()
    {
        prefsManager.SetHasMetChildren(true);
        childrenDialogueTrigger.ToggleAltDialogue(true);
    }

    public void ChildrenFinalInteraction()
    {
        childrenDialogueTrigger.ToggleAltDialogue(false);
        childrenDialogueTrigger.ToggleFinalDialogue(true);
    }

    public void StallSoldOut()
    {
        Image imageComponent = stallImage.GetComponent<Image>();
        imageComponent.sprite = emptyStall;

        stallDialogueTrigger.ToggleAltDialogue(true);

        BonnieFinalInteraction();
    }

    public void ChurchTriggerEnabled()
    {
        stallImage.gameObject.SetActive(false);
        bonnieImage.gameObject.SetActive(false);
        bonnieTrigger.gameObject.SetActive(false);

        churchTrigger.gameObject.SetActive(true);
    }

    public void AlleyTriggerEnabled()
    {
        Debug.Log("Alley triggered");
        childrenImage.gameObject.SetActive(false);
        childrenTrigger.gameObject.SetActive(false);
        alleyTrigger.gameObject.SetActive(true);

        prefsManager.SetOpenedAlleyEntry(true);
    }

    public void GetSweets()
    {
        prefsManager.SetHasGotSweets(true);
        ChildrenFinalInteraction();
    }

    public void BonnieLeaves()
    {
        prefsManager.SetHasBonnieLeft(true);

        bonnieImage.gameObject.SetActive(false);
        bonnieTrigger.gameObject.SetActive(false);
    }
}
