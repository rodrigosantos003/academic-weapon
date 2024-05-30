using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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

        _startButton.RegisterCallback<ClickEvent>(OnPlayGameClick);
        _quitButton.RegisterCallback<ClickEvent>(OnQuitGameClick);
    }

    private void OnPlayGameClick(ClickEvent evt)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("save");
    }

    private void OnQuitGameClick(ClickEvent evt)
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        Application.Quit();
    }
}
