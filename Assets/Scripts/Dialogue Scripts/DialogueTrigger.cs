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
    [SerializeField] private UnityEvent events3;

    [SerializeField] private bool useAltDialogue = false;
    [SerializeField] private bool useFinalDialogue = false;

    public void FirstInteraction()
    {
        Debug.Log("FirstInteraction called with useAltDialogue: " + useAltDialogue + ", useFinalDialogue: " + useFinalDialogue);

        if (useAltDialogue)
        {
            Debug.Log("Starting altDialogue");
            dialogBehaviour.StartDialog(altDialogue);
            BindCommonFunctions();
        }
        else if (useFinalDialogue)
        {
            Debug.Log("Starting finalDialogue");
            dialogBehaviour.StartDialog(finalDialogue);
            BindCommonFunctions();
        }
        else
        {
            Debug.Log("Starting initialDialogue");
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
        dialogBehaviour.BindExternalFunction("ExitScene", Function3);
    }

    public void Function()
    {
        events.Invoke();
    }
    public void Function2()
    {
        events2.Invoke();
    }

    public void Function3()
    {
        events3.Invoke();
    }

}
