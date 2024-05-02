using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] private CursorManager cursorManager;

    private void Awake()
    {
        mainCamera = Camera.main;
    }
    private void Update()
    {
        ChangeCursor();    }

    void ChangeCursor()
    {
        RaycastHit2D hit = Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (hit.collider != null)
        {
            //Debug.Log("Mouse entered object: " + hit.collider.gameObject.name);

            if (hit.collider.CompareTag("LookableObject"))
            {
                cursorManager.SetCursor(CursorManager.CursorType.Look);
            }
            else if (hit.collider.CompareTag("TalkableObject"))
            {
                cursorManager.SetCursor(CursorManager.CursorType.Talk);
            }
            else if (hit.collider.CompareTag("GoNorth"))
            {
                cursorManager.SetCursor(CursorManager.CursorType.North);
            }
            else if (hit.collider.CompareTag("GoEast"))
            {
                cursorManager.SetCursor(CursorManager.CursorType.East);
            }
            else if (hit.collider.CompareTag("GoWest"))
            {
                cursorManager.SetCursor(CursorManager.CursorType.West);
            }
            else if (hit.collider.CompareTag("GoSouth"))
            {
                cursorManager.SetCursor(CursorManager.CursorType.South);
            }
            else
            {
                cursorManager.SetCursor(CursorManager.CursorType.Default);
            }
        }
        else
        {
            // Reset cursor to default when not hovering over any object
            cursorManager.SetCursor(CursorManager.CursorType.Default);
        }
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        var rayHit = Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!rayHit.collider) return;

        Debug.Log(rayHit.collider.gameObject.name);
        if (rayHit.collider.GetComponent<SceneManagement>())
        {
            rayHit.collider.GetComponent<SceneManagement>().GoToScene();
        }
        var bonnieDialogue = rayHit.collider.GetComponent<DialogueTrigger>();
        if (bonnieDialogue)
        {
            // Trigger the FirstInteraction function
            bonnieDialogue.FirstInteraction();
        }
    }
}
