using System.Collections;
using UnityEngine;
using TMPro; // Import TextMeshPro namespace

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem instance;
    public Components components;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Shows text when character is talking
    public void Speak(string speech,bool additive = false, string speaker = "")
    {
        StopSpeaking();
        speaking = StartCoroutine(Speaking(speech,false, speaker));
    }

    //add dialogue to pre existing text in the text box
    public void AdditionalSpeak(string speech, bool additive = false, string speaker = "")
    {
        StopSpeaking();
        speechText.text = targetSpeech;
        speaking = StartCoroutine(Speaking(speech, true, speaker));
    }
    public void StopSpeaking()
    {
        if (isSpeaking)
        {
            StopCoroutine(speaking);
        }
        speaking = null;
    }

    [HideInInspector] public bool isWaitingForUserInput = false;

    public bool isSpeaking { get { return speaking != null; } }

    string targetSpeech = "";
    Coroutine speaking = null;
    IEnumerator Speaking(string speech,bool additive, string speaker = "")
    {
        components.textBar.SetActive(true);
        targetSpeech = speech;
        if (!additive)
        {
            speechText.text = "";
        }
        else
        {
            targetSpeech = speechText.text + targetSpeech;
        }
        components.speechText.text = "";
        components.speakerName.text = DetermineSpeaker(speaker);

        while (components.speechText.text != targetSpeech)
        {
            components.speechText.text += targetSpeech[components.speechText.text.Length];
            yield return new WaitForEndOfFrame();
        }

        // Text finishes
        isWaitingForUserInput = true;
        while (isWaitingForUserInput)
        {
            yield return new WaitForEndOfFrame();
        }
        StopSpeaking();
    }

    string DetermineSpeaker(string s)
    {
        string retVal = speakerName.text; //defaults to current name
        if (s != speakerName.text && s != "")
        {
            retVal = (s.ToLower().Contains("Narrator")) ? "" : s;
        }
        return retVal;
    }

    [System.Serializable]
    public class Components
    {
        // All the stuff relating to dialogue
        public GameObject textBar;
        public TextMeshProUGUI speakerName;
        public TextMeshProUGUI speechText;
    }

    public GameObject textBar { get { return components.textBar; } }
    public TextMeshProUGUI speakerName { get { return components.speakerName; } }
    public TextMeshProUGUI speechText { get { return components.speechText; } }
}
