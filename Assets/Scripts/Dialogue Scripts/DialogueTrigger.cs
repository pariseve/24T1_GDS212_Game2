using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using cherrydev;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogBehaviour dialogBehaviour;
    [SerializeField] private DialogNodeGraph initialDialogue;
    [SerializeField] private DialogNodeGraph altDialogue;
    [SerializeField] private DialogNodeGraph finalDialogue;

    [SerializeField] private UnityEvent events;
    [SerializeField] private UnityEvent events2;

    [SerializeField] private bool useAltDialogue = false;

    public void FirstInteraction()
    {
        if (useAltDialogue)
        {
            dialogBehaviour.StartDialog(altDialogue);
        }
        else
        {
            dialogBehaviour.StartDialog(initialDialogue);
        }
    }

    public void ToggleDialogue(bool useAlt)
    {
        useAltDialogue = useAlt;
    }

    public void BindCommonFunctions()
    {
        dialogBehaviour.BindExternalFunction("initialComplete", Function);
        dialogBehaviour.BindExternalFunction("function", Function2);
    }

    public void Function()
    {
        events.Invoke();
    }
    public void Function2()
    {
        events2.Invoke();
    }

}
