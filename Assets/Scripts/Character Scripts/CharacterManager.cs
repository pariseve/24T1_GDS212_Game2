using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance;

    //panel to attach character sprites to
    public RectTransform characterPanel;

    //list of all characters in current scene
    public List<Character> characters = new List<Character>();

    public Dictionary<string, int> characterDictionary = new Dictionary<string, int>();

    private void Awake()
    {
        instance = this;
    }
  
    public Character GetCharacter(string characterName, bool createCharacterIfNonexistent = true, bool enableCreatedCharacterOnStart = true)
    {
        int index = -1;
        if(characterDictionary.TryGetValue(characterName, out index))
        {
            return characters [index];
        }
        else if (createCharacterIfNonexistent)
        {
            return CreateCharacter(characterName, enableCreatedCharacterOnStart);
        }

        return null;
    }

    public Character CreateCharacter(string characterName, bool enableOnStart = true)
    {
        Character newCharacter = new Character(characterName, enableOnStart);

        characterDictionary.Add(characterName, characters.Count);
        characters.Add(newCharacter);

        return newCharacter;
    }

    public class CharacterPositions
    {
        public Vector2 farLeft = new Vector2(0, 0);
        public Vector2 farRight = new Vector2(0, 0);
        public Vector2 centre = new Vector2(0, 0);
    }
}
