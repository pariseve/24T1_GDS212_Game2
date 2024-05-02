using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    private static PlayerPrefsManager instance;
    public static PlayerPrefsManager Instance
    {
        get
        {
            if (instance == null)
            {
                // Create a new GameObject to hold the instance
                GameObject managerObject = new GameObject("PlayerPrefsManager");
                instance = managerObject.AddComponent<PlayerPrefsManager>();
                DontDestroyOnLoad(managerObject);
            }
            return instance;
        }
    }
    public void SetHasMetBonnie(bool hasMet)
    {
        PlayerPrefs.SetInt("HasMetBonnie", hasMet ? 1 : 0);
    }

    public bool HasMetBonnie()
    {
        return PlayerPrefs.GetInt("HasMetBonnie", 0) == 1;
    }

    public void SetHasMetChildren(bool hasMet)
    {
        PlayerPrefs.SetInt("HasMetChildren", hasMet ? 1 : 0);
    }
    public bool HasMetChildren()
    {
        return PlayerPrefs.GetInt("HasMetChildren", 0) == 1;
    }

}
