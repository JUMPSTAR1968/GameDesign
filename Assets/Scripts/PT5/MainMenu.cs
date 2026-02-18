using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button newGameButton;
    [SerializeField] Button quitButton;
    [SerializeField] Button optionsButton;
    [SerializeField] Button backButton;

    [SerializeField] GameObject mainMenuObj;
    [SerializeField] GameObject OptionsObj;


    private void Start() 
    {
        newGameButton.onClick.AddListener(TransitionToNewScene);
        quitButton.onClick.AddListener(QuitGame);
        optionsButton.onClick.AddListener(OpenOptions);
        backButton.onClick.AddListener(CloseOptions);
    }

    void TransitionToNewScene() => SceneManager.LoadScene(1);
    void OpenOptions() 
    { 
        mainMenuObj.SetActive(false);
        OptionsObj.SetActive(true);
    }
    void CloseOptions() 
    { 
        mainMenuObj.SetActive(true);
        OptionsObj.SetActive(false);
    }

    void QuitGame() => Application.Quit();

}
