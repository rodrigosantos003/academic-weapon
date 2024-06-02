using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private UIDocument _document;
    private Button _startButton;
    private Button _quitButton;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();
        _startButton = _document.rootVisualElement.Q<Button>("StartButton");
        _quitButton = _document.rootVisualElement.Q<Button>("QuitButton");

        _startButton.RegisterCallback<ClickEvent>(StartGame);
        _quitButton.RegisterCallback<ClickEvent>(QuitGame);
    }

    private void StartGame(ClickEvent evt)
    {
        SceneManager.LoadScene("PlayingScene");
    }

    private void QuitGame(ClickEvent evt)
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        Application.Quit();
    }
}
