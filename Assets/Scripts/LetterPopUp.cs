using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterPopUp : MonoBehaviour
{
    public GameObject letterPopup;
    private bool letterShown;

    private void Start()
    {
        letterShown = PlayerPrefs.GetInt("TutorialShown", 0) == 1;

        if (!letterShown)
        {
            ShowTutorialPopup();
            PauseGame();
        }
    }

    private void Update()
    {
        if (letterPopup.activeSelf && Input.GetMouseButtonDown(0))
        {
            CloseTutorialPopup();
        }
    }

    public void OnExitButtonClicked()
    {
        CloseTutorialPopup();
    }

    public void ShowTutorialPopup()
    {
        letterPopup.SetActive(true);
    }

    public void CloseTutorialPopup()
    {
        letterPopup.SetActive(false);
        letterShown = true;
        PlayerPrefs.SetInt("TutorialShown", 1);
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
