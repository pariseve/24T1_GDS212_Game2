using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterPopUp : MonoBehaviour
{
    public GameObject letterPopup;
    public GameObject triggerManager;
    private bool letterShown;

    private void Start()
    {
        
        letterShown = PlayerPrefs.GetInt("LetterShown", 0) == 1;

        if (!letterShown)
        {
            ShowLetterPopup();
            triggerManager.SetActive(false);
            PauseGame();
        }
    }

    private void Update()
    {
        if (letterPopup.activeSelf && Input.GetMouseButtonDown(0))
        {
            CloseLetterPopup();
        }
    }

    public void OnExitButtonClicked()
    {
        CloseLetterPopup();
    }

    public void ShowLetterPopup()
    {
        letterPopup.SetActive(true);
    }

    public void CloseLetterPopup()
    {
        letterPopup.SetActive(false);
        letterShown = true;
        triggerManager.SetActive(true);
        PlayerPrefs.SetInt("LetterShown", 1);
        UnpauseGame();
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
    }

    private void UnpauseGame()
    {
        Time.timeScale = 1f;
    }
}
