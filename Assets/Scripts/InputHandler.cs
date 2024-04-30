using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
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
        var bonnieDialogue = rayHit.collider.GetComponent<BonnieDialogue>();
        if (bonnieDialogue)
        {
            // Trigger the FirstInteraction function
            bonnieDialogue.FirstInteraction();
        }
    }
}
