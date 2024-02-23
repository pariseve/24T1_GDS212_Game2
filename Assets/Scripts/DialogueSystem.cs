using System.Collections;
using UnityEngine;
using TMPro; // Import TextMeshPro namespace

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem instance;
    public Components components;

	void Awake()
	{
		instance = this;
	}

	//add text to textbar
	public void Speak(string speech,bool additive = false, string speaker = "")
	{
		StopSpeaking();

		speaking = StartCoroutine(Speaking(speech, false, speaker));
	}

	//additional text generates alonside what is already in the textbar
	public void AdditionalSpeak(string speech,bool additive = true, string speaker = "")
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
		if(textArchitect != null && textArchitect.isContructing)
        {
			textArchitect.Stop();
        }
		speaking = null;
	}

	public bool isSpeaking { get { return speaking != null; } }
	[HideInInspector] public bool isWaitingForUserInput = false;

	public string targetSpeech = "";
	Coroutine speaking = null;
	TextArchitect textArchitect = null;
    IEnumerator Speaking(string speech, bool additive, string speaker = "")
    {
        // Ensure the text box is always activated at the beginning
        components.textBar.SetActive(true);

        if (!additive)
        {
            speechText.text = "";
        }

        // Initialize TextArchitect with the dialogue string
        textArchitect = new TextArchitect(speech, additive ? speechText.text : "");

        speakerName.text = DetermineSpeaker(speaker);
        isWaitingForUserInput = false;

        // Step through the TextArchitect's construction process
        while (textArchitect.isContructing)
        {
            if (Input.GetMouseButtonDown(0))
            {
                textArchitect.skip = true;
            }

            // Update speechText with currentText from TextArchitect
            speechText.text = textArchitect.currentText;

            yield return new WaitForEndOfFrame();
        }

        // Set speechText to the final constructed text
        speechText.text = textArchitect.currentText;

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
		string retVal = speakerName.text;//default return is the current name
		if (s != speakerName.text && s != "")
			retVal = (s.ToLower().Contains("narrator")) ? "" : s;

		return retVal;
	}

	public void Close()
    {
        StopSpeaking();
        textBar.SetActive(false);
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
