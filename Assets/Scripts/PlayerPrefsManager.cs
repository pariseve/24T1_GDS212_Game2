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

    public void SetHasMetEunice(bool hasMet)
    {
        PlayerPrefs.SetInt("HasMetEunice", hasMet ? 1 : 0);
    }

    public bool HasMetEunice()
    {
        return PlayerPrefs.GetInt("HasMetEunice", 0) == 1;
    }

    public void SetHasMetDrunkMen(bool hasMet)
    {
        PlayerPrefs.SetInt("HasMetDrunkMen", hasMet ? 1 : 0);
    }

    public bool HasMetDrunkMen()
    {
        return PlayerPrefs.GetInt("HasMetDrunkMen", 0) == 1;
    }

    public void SetHasMetRaymond(bool hasMet)
    {
        PlayerPrefs.SetInt("HasMetRaymond", hasMet ? 1 : 0);
    }

    public bool HasMetRaymond()
    {
        return PlayerPrefs.GetInt("HasMetRaymond", 0) == 1;
    }

    public void SetOpenedAlleyEntry(bool hasOpened)
    {
        PlayerPrefs.SetInt("HasOpenedAlley", hasOpened ? 1 : 0);
    }

    public bool HasOpenedAlleyEntry()
    {
        bool temp;
        temp = PlayerPrefs.GetInt("HasOpenedAlley", 0) == 1;
        return temp;
    }

    public void SetOpenedChurchEntry(bool hasOpened)
    {
        PlayerPrefs.SetInt("HasOpenedChurch", hasOpened ? 1 : 0);
    }
    public bool HasOpenedChurchEntry()
    {
        return PlayerPrefs.GetInt("HasOpenedChurch", 0) == 1;
    }

    public void SetHasGotSweets(bool hasGot)
    {
        PlayerPrefs.SetInt("HasSweets", hasGot ? 1 : 0);
    }

    public bool HasGotSweets()
    {
        return PlayerPrefs.GetInt("HasSweets", 0) == 1;
    }

    public void SetHasBonnieLeft(bool hasLeft)
    {
        PlayerPrefs.SetInt("BonnieHasLeft", hasLeft ? 1 : 0);
    }

    public bool HasBonnieLeft()
    {
        return PlayerPrefs.GetInt("BonnieHasLeft", 0) == 1;
    }

}
