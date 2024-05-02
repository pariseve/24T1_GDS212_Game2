using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InnScript : MonoBehaviour
{
    [SerializeField] private DialogueTrigger euniceDialogueTrigger;
    [SerializeField] private DialogueTrigger drunkMenDialogueTrigger;
    [SerializeField] private DialogueTrigger irvingDialogueTrigger;

    private PlayerPrefsManager prefsManager;

    private bool hasMetEunice;
    private bool hasMetIrving;
    private bool hasMetRaymond;
    private bool hasMetDrunkMen;

    public GameObject irvingImage;
    public GameObject irvingTrigger;

    public GameObject albertTrigger;

    public GameObject tableImage;
    public GameObject euniceTrigger;
    public Sprite emptyTable;

    private void Start()
    {
        prefsManager = PlayerPrefsManager.Instance;

        hasMetEunice = prefsManager.HasMetEunice();
        if (hasMetEunice)
        {
            euniceDialogueTrigger.ToggleAltDialogue(true);
        }

        hasMetDrunkMen = prefsManager.HasMetDrunkMen();
        if (hasMetDrunkMen)
        {
            drunkMenDialogueTrigger.ToggleAltDialogue(true);
        }

        hasMetRaymond = prefsManager.HasMetRaymond();
        if (hasMetRaymond)
        {
            EuniceAndIrvingSwitch();
        }
    }

    //ensures that the intial dialogue introducing eunice does not play twice, switches to alt dialogue graph
    public void EuniceInitialComplete()
    {
        prefsManager.SetHasMetEunice(true);
        euniceDialogueTrigger.ToggleAltDialogue(true);
    }

    //uses playerprefs to update the state of stall component in the townscene
    public void TriggerStallSoldOut()
    {
        PlayerPrefs.SetInt("StallSoldOut", 1);
    }

    //ensures that the intial dialogue introducing the drunk NPCs does not play twice, switches to alt dialogue graph
    public void DrunkMenInitialComplete()
    {
        prefsManager.SetHasMetDrunkMen(true);
        drunkMenDialogueTrigger.ToggleAltDialogue(true);
    }

    //removes eunice image and trigger and adds irving image and trigger
    public void EuniceAndIrvingSwitch()
    {
        irvingImage.gameObject.SetActive(true);
        irvingTrigger.gameObject.SetActive(true);

        albertTrigger.gameObject.SetActive(false);
        euniceTrigger.gameObject.SetActive(false);

        Image imageComponent = tableImage.GetComponent<Image>();
        imageComponent.sprite = emptyTable;
    }
}
