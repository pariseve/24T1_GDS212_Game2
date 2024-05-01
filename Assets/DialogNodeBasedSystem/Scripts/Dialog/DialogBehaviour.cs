using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

namespace cherrydev
{
    public class DialogBehaviour : MonoBehaviour
    {
        [SerializeField] private GameObject triggerManager;
        [SerializeField] private float dialogCharDelay;
        [SerializeField] private List<KeyCode> nextSentenceKeyCodes;
        [SerializeField] private bool isCanSkippingText = true;

        [Space(10)]
        [SerializeField] private UnityEvent onDialogStarted;
        [SerializeField] private UnityEvent onDialogFinished;

        private DialogNodeGraph currentNodeGraph;
        private Node currentNode;
        private int maxAmountOfAnswerButtons;
        private bool isDialogStarted;
        private bool isCurrentSentenceSkipped;

        private Dictionary<string, UnityAction> externalFunctions = new Dictionary<string, UnityAction>();

        private void Start()
        {
            //if (triggerManager != null)
            //{
            //    triggerManager.SetActive(true);
            //}
        }

        private void Update()
        {
            HandleSentenceSkipping();
        }

        public void BindExternalFunction(string functionName, UnityAction function)
        {
            if (externalFunctions.ContainsKey(functionName))
            {
                externalFunctions[functionName] = function;
            }
            else
            {
                externalFunctions.Add(functionName, function);
            }
        }

        public void StartDialog(DialogNodeGraph dialogNodeGraph)
        {
            isDialogStarted = true;

            if (triggerManager != null)
            {
                triggerManager.SetActive(false);
            }

            if (dialogNodeGraph.nodesList == null)
            {
                Debug.LogWarning("Dialog Graph's node list is empty");
                return;
            }

            onDialogStarted?.Invoke();

            currentNodeGraph = dialogNodeGraph;

            DefineFirstNode(dialogNodeGraph);
            CalculateMaxAmountOfAnswerButtons();
            HandleDialogGraphCurrentNode(currentNode);
        }

        public void AddListenerToDialogFinishedEvent(UnityAction action)
        {
            onDialogFinished.AddListener(action);
        }

        public void SetCurrentNodeAndHandleDialogGraph(Node node)
        {
            currentNode = node;
            HandleDialogGraphCurrentNode(currentNode);
        }

        private void HandleDialogGraphCurrentNode(Node currentNode)
        {
            StopAllCoroutines();

            if (currentNode.GetType() == typeof(SentenceNode))
            {
                SentenceNode sentenceNode = (SentenceNode)currentNode;

                isCurrentSentenceSkipped = false;

                OnSentenceNodeActive?.Invoke();
                OnSentenceNodeActiveWithParameter?.Invoke(sentenceNode.GetSentenceCharacterName(), sentenceNode.GetSentenceText(),
                    sentenceNode.GetCharacterSprite());

                WriteDialogText(sentenceNode.GetSentenceText());
            }
            else if (currentNode.GetType() == typeof(AnswerNode))
            {
                AnswerNode answerNode = (AnswerNode)currentNode;

                int amountOfActiveButtons = 0;

                OnAnswerNodeActive?.Invoke();

                for (int i = 0; i < answerNode.childSentenceNodes.Count; i++)
                {
                    if (answerNode.childSentenceNodes[i] != null)
                    {
                        OnAnswerNodeSetUp?.Invoke(i, answerNode.answers[i]);
                        OnAnswerButtonSetUp?.Invoke(i, answerNode);

                        amountOfActiveButtons++;
                    }
                    else
                    {
                        break;
                    }
                }

                if (amountOfActiveButtons == 0)
                {
                    isDialogStarted = false;

                    onDialogFinished?.Invoke();
                    return;
                }

                OnAnswerNodeActiveWithParameter?.Invoke(amountOfActiveButtons);
            }
        }

        private void DefineFirstNode(DialogNodeGraph dialogNodeGraph)
        {
            if (dialogNodeGraph.nodesList.Count == 0)
            {
                Debug.LogWarning("The list of nodes in the DialogNodeGraph is empty");
                return;
            }

            foreach (Node node in dialogNodeGraph.nodesList)
            {
                currentNode = node;

                if (node.GetType() == typeof(SentenceNode))
                {
                    SentenceNode sentenceNode = (SentenceNode)node;

                    if (sentenceNode.parentNode == null && sentenceNode.childNode != null)
                    {
                        currentNode = sentenceNode;
                        return;
                    }
                }
            }

            currentNode = dialogNodeGraph.nodesList[0];
        }

        private void WriteDialogText(string text)
        {
            StartCoroutine(WriteDialogTextRoutine(text));
        }

        private IEnumerator WriteDialogTextRoutine(string text)
        {
            foreach (char textChar in text)
            {
                if (isCurrentSentenceSkipped)
                {
                    OnDialogTextSkipped?.Invoke(text);
                    break;
                }

                OnDialogTextCharWrote?.Invoke();

                yield return new WaitForSeconds(dialogCharDelay);
            }

            yield return new WaitUntil(CheckNextSentenceKeyCodes);

            CheckForDialogNextNode();
        }

        private void CheckForDialogNextNode()
        {
            if (currentNode.GetType() == typeof(SentenceNode))
            {
                SentenceNode sentenceNode = (SentenceNode)currentNode;

                if (sentenceNode.childNode != null)
                {
                    currentNode = sentenceNode.childNode;
                    HandleDialogGraphCurrentNode(currentNode);
                }
                else
                {
                    isDialogStarted = false;
                    onDialogFinished?.Invoke();

                    if (triggerManager != null)
                    {
                        triggerManager.SetActive(true);
                    }
                }
            }
        }

        private void CalculateMaxAmountOfAnswerButtons()
        {
            foreach (Node node in currentNodeGraph.nodesList)
            {
                if (node.GetType() == typeof(AnswerNode))
                {
                    AnswerNode answerNode = (AnswerNode)node;

                    if (answerNode.answers.Count > maxAmountOfAnswerButtons)
                    {
                        maxAmountOfAnswerButtons = answerNode.answers.Count;
                    }
                }
            }

            OnMaxAmountOfAnswerButtonsCalculated?.Invoke(maxAmountOfAnswerButtons);
        }

        private void HandleSentenceSkipping()
        {
            if (!isDialogStarted || !isCanSkippingText)
            {
                return;
            }

            if (CheckNextSentenceKeyCodes() && !isCurrentSentenceSkipped)
            {
                isCurrentSentenceSkipped = true;
            }
        }

        private bool CheckNextSentenceKeyCodes()
        {
            foreach (KeyCode keyCode in nextSentenceKeyCodes)
            {
                if (Input.GetKeyDown(keyCode))
                {
                    return true;
                }
            }
            return false;
        }

        public event Action OnSentenceNodeActive;
        public event Action<string, string, Sprite> OnSentenceNodeActiveWithParameter;
        public event Action OnAnswerNodeActive;
        public event Action<int, AnswerNode> OnAnswerButtonSetUp;
        public event Action<int> OnMaxAmountOfAnswerButtonsCalculated;
        public event Action<int> OnAnswerNodeActiveWithParameter;
        public event Action<int, string> OnAnswerNodeSetUp;
        public event Action OnDialogTextCharWrote;
        public event Action<string> OnDialogTextSkipped;
    }
}
