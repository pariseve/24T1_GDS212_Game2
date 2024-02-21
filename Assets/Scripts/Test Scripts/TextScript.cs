using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScript : MonoBehaviour
{
    DialogueSystem dialogue;
    // Start is called before the first frame update
    void Start()
    {
        dialogue = DialogueSystem.instance;
    }

    [TextArea(3, 10)]
    public string[] s = new string[]
    {
    };

    int index = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(!dialogue.isSpeaking || dialogue.isWaitingForUserInput)
            {
                if(index >= s.Length)
                {
                    return;
                }
                Speak(s[index]);
                index++;
            }
        }
    }

    void Speak(string s)
    {
        string[] parts = s.Split(":");
        string speech = parts[0];
        string speaker = (parts.Length >= 2) ? parts[1] : "";

        dialogue.Speak(speech, false, speaker);
    }
}
