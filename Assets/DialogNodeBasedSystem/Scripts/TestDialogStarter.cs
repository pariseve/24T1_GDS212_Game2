using UnityEngine;
using UnityEngine.SceneManagement;
using cherrydev;

public class TestDialogStarter : MonoBehaviour
{
    [SerializeField] private DialogBehaviour dialogBehaviour;
    [SerializeField] private DialogNodeGraph dialogGraph;

    private bool hasReadTownIntro;
    private bool hasReadInnIntro;

    private void Start()
    {
        hasReadTownIntro = PlayerPrefs.GetInt("HasReadIntroToTown", 0) == 1;
        hasReadInnIntro = PlayerPrefs.GetInt("HasReadIntroToInn", 0) == 1;
        dialogBehaviour.BindExternalFunction("Test", DebugExternal);

        string currentSceneName = SceneManager.GetActiveScene().name;

        switch (currentSceneName)
        {
            case "TownScene":
                TownSceneIntro();
                break;
            case "InnScene":
                InnSceneIntro();
                break;
        }


        TownSceneIntro();
    }

    private void TownSceneIntro()
    {
        if (hasReadTownIntro)
        {
            Debug.Log("Has already read town intro");
        }
        else
        {
            dialogBehaviour.StartDialog(dialogGraph);
            hasReadTownIntro = true;
            PlayerPrefs.SetInt("HasReadIntroToTown", 1);
        }
    }

    private void InnSceneIntro()
    {
        if (hasReadInnIntro)
        {
            Debug.Log("Has already read inn intro");
        }
        else
        {
            dialogBehaviour.StartDialog(dialogGraph);
            hasReadInnIntro = true;
            PlayerPrefs.SetInt("HasReadIntroToInn", 1);
        }
    }


    private void DebugExternal()
    {
        Debug.Log("External function works!");
    }
}