using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public enum CursorType
    {
        Default,
        Look,
        Talk,
        North,
        East,
        West,
        South
    }

    [System.Serializable]
    public struct CursorInfo
    {
        public CursorType type;
        public Texture2D texture;
        public Vector2 hotspot;
    }

    [SerializeField] private CursorInfo[] cursorInfos;

    private CursorType currentCursorType = CursorType.Default;

    void Start()
    {
        SetCursor(CursorType.Default);
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void SetCursor(CursorType type)
    {
        Debug.Log("Setting cursor to: " + type);
        CursorInfo cursorInfo = GetCursorInfo(type);
        if (cursorInfo.texture != null)
        {
            Cursor.SetCursor(cursorInfo.texture, cursorInfo.hotspot, CursorMode.Auto);
            currentCursorType = type;
        }
        else
        {
            Debug.LogWarning("Cursor texture not found for type: " + type);
        }
    }


    private CursorInfo GetCursorInfo(CursorType type)
    {
        foreach (CursorInfo info in cursorInfos)
        {
            if (info.type == type)
            {
                return info;
            }
        }
        return new CursorInfo(); // Return an empty cursor info if not found
    }
}
