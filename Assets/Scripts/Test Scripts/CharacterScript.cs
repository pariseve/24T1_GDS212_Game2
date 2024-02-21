using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    public Character Irving;
    // Start is called before the first frame update
    void Start()
    {
        Irving = CharacterManager.instance.GetCharacter("Irving", enableCreatedCharacterOnStart: false);
    }

    public string[] speech;
    int i = 0;

    public Vector2 moveTarget;
    public float moveSpeed;
    public bool smooth;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(i < speech.Length)
            {
                Irving.Speak(speech[i], "Irving");

            }
            else
            {
                DialogueSystem.instance.Close();
            }
            i++;
        }
        if (Input.GetMouseButtonDown(1))
        {
            Irving.MoveTo(moveTarget, moveSpeed, smooth);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Irving.StopMoving(true);
        }
    }
}
