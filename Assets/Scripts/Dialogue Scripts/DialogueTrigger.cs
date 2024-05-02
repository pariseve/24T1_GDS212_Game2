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
    [SerializeField] private bool useFinalDialogue = false;

    public void FirstInteraction()
    {
        if (useAltDialogue)
        {
            dialogBehaviour.StartDialog(altDialogue);
            BindCommonFunctions();
        }
        else if (useFinalDialogue)
        {
            dialogBehaviour.StartDialog(finalDialogue);
            BindCommonFunctions();
        }
        else
        {
            dialogBehaviour.StartDialog(initialDialogue);
            BindCommonFunctions();
        }
    }

    public void ToggleAltDialogue(bool useAlt)
    {
        useAltDialogue = useAlt;
    }

    public void ToggleFinalDialogue(bool useFinal)
    {
        useFinalDialogue = useFinal;
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
