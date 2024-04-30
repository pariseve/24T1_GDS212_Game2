using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cherrydev;

public class BonnieDialogue : MonoBehaviour
{
    [SerializeField] private DialogBehaviour dialogBehaviour;
    [SerializeField] private DialogNodeGraph introdialogGraph;

    public void FirstInteraction()
    {
        dialogBehaviour.StartDialog(introdialogGraph);
    }
}
