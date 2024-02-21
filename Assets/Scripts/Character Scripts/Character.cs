using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Character 
{
    DialogueSystem dialogue;

    public string characterName;
    [HideInInspector] public RectTransform root;
    private Image characterImage;

    public bool enabled { get { return root.gameObject.activeInHierarchy; } set { root.gameObject.SetActive(value); } }

    public Vector2 anchorPadding { get { return root.anchorMax - root.anchorMin; } }

    public void Speak(string speech,  string characterName, bool add = false)
    {
        if (!enabled)
        {
            enabled = true;
        }
        if (!add)
        {
            dialogue.Speak(speech, false, characterName);
        }
        else
        {
            dialogue.AdditionalSpeak(speech, true, characterName);
        }
    }

    Vector2 targetPosition;
    Coroutine moving;
    bool isMoving { get { return moving != null; } }
    public void MoveTo(Vector2 Target, float speed, bool smooth = true)
    {
        StopMoving();
        moving = CharacterManager.instance.StartCoroutine(Moving(Target, speed, smooth));
    }

    public void StopMoving(bool arriveAtTargetImmediately = false)
    {
        if (isMoving)
        {
            CharacterManager.instance.StopCoroutine(moving);
            if (arriveAtTargetImmediately)
            {
                SetPosition(targetPosition);
            }
        }
        moving = null;
    }

    public void SetPosition(Vector2 target)
    {
        targetPosition = target;
        Vector2 padding = anchorPadding;

        float maxX = 1f - padding.x;
        float maxY = 1f - padding.y;

        Vector2 minAnchorTarget = new Vector2(maxX * targetPosition.x, maxY * targetPosition.y);

        root.anchorMin = minAnchorTarget;
        root.anchorMax = root.anchorMin + padding;

    }

    IEnumerator Moving(Vector2 target, float speed, bool smooth)
    {
        targetPosition = target;
        Vector2 padding = anchorPadding;

        float maxX = 1f - padding.x;
        float maxY = 1f - padding.y;

        Vector2 minAnchorTarget = new Vector2(maxX * targetPosition.x, maxY * targetPosition.y);
        speed *= Time.deltaTime;

        while(root.anchorMin != minAnchorTarget)
        {
            root.anchorMin = (!smooth) ? Vector2.MoveTowards(root.anchorMin, minAnchorTarget, speed) : Vector2.Lerp(root.anchorMin, minAnchorTarget, speed);
            root.anchorMax = root.anchorMin + padding;
            yield return new WaitForEndOfFrame();
        }

        StopMoving();
    }

    public Character(string _name, bool enableOnStart = true)
    {
        CharacterManager cm = CharacterManager.instance;
        GameObject prefab = Resources.Load<GameObject>("Character/Character[" + _name + "]");
        GameObject ob = GameObject.Instantiate(prefab, cm.characterPanel);

        root = ob.GetComponent<RectTransform>();
        ob.name = _name;

        characterImage = ob.GetComponent<Image>();

        dialogue = DialogueSystem.instance;

        enabled = enableOnStart;
    }
}
