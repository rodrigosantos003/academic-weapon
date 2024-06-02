using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIDocumentManager : MonoBehaviour
{
    private bool _isPaused = false;
    private bool _isGameOver = false;
    [SerializeField] private UIDocument _pauseMenu;
    [SerializeField] private UIDocument _winScreen;
    [SerializeField] private UIDocument _gameOverScreen;
    
    private Button _resumeButtonPauseMenu;
    private Button _restartButtonPauseMenu;
    private Button _quitButtonPauseMenu;

    private Button _restartButtonWinScreen;
    private Button _quitButtonWinScreen;
    
    private Button _restartButtonGameOverScreen;
    private Button _quitButtonGameOverScreen;
    
    void Start()
    {
        _pauseMenu.rootVisualElement.style.display = DisplayStyle.None;
        _winScreen.rootVisualElement.style.display = DisplayStyle.None;
        _gameOverScreen.rootVisualElement.style.display = DisplayStyle.None;
        
        _resumeButtonPauseMenu = _pauseMenu.rootVisualElement.Q<Button>("ResumeButton");
        
        _restartButtonPauseMenu = _pauseMenu.rootVisualElement.Q<Button>("RestartButton");
        _restartButtonWinScreen = _winScreen.rootVisualElement.Q<Button>("RestartButton");
        _restartButtonGameOverScreen = _gameOverScreen.rootVisualElement.Q<Button>("RestartButton");
        
        _quitButtonPauseMenu = _pauseMenu.rootVisualElement.Q<Button>("QuitButton");
        _quitButtonWinScreen = _winScreen.rootVisualElement.Q<Button>("QuitButton");
        _quitButtonGameOverScreen = _gameOverScreen.rootVisualElement.Q<Button>("QuitButton");
        
        _resumeButtonPauseMenu.RegisterCallback<ClickEvent>(ResumeGame);
        
        _restartButtonPauseMenu.RegisterCallback<ClickEvent>(RestartGame);
        _restartButtonWinScreen.RegisterCallback<ClickEvent>(RestartGame);
        _restartButtonGameOverScreen.RegisterCallback<ClickEvent>(RestartGame);
        
        _quitButtonPauseMenu.RegisterCallback<ClickEvent>(QuitGame);
        _quitButtonWinScreen.RegisterCallback<ClickEvent>(QuitGame);
        _quitButtonGameOverScreen.RegisterCallback<ClickEvent>(QuitGame);
    }
    
    public void GameOver(bool playerWon)
    {
        Time.timeScale = 0;
        _isGameOver = true;
        if(playerWon)
            _winScreen.rootVisualElement.style.display = DisplayStyle.Flex;
        else
            _gameOverScreen.rootVisualElement.style.display = DisplayStyle.Flex;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !_isGameOver)
        {
            if(_isPaused)
            {
                Time.timeScale = 1;
                _pauseMenu.rootVisualElement.style.display = DisplayStyle.None;
            }
            else
            {
                Time.timeScale = 0;
                _pauseMenu.rootVisualElement.style.display = DisplayStyle.Flex;
            }
            
            _isPaused = !_isPaused;
        }
    }
    
    public void RestartGame(ClickEvent evt)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    
    public void QuitGame(ClickEvent evt)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    
    public void ResumeGame(ClickEvent evt)
    {
        Time.timeScale = 1;
        _pauseMenu.rootVisualElement.style.display = DisplayStyle.None;
    }
}
