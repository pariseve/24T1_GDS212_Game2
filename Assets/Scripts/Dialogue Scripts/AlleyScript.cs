using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlleyScript : MonoBehaviour
{
    public void TriggerChurchEntry()
    {
        PlayerPrefs.SetInt("ChurchOpen", 1);
    }
}
