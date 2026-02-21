using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("HomeUIButtons")]
    public Button grid2x2Button;
    public Button grid2x3Button;
    public Button grid4x4Button;
    public Button grid5x6Button;
    public Button quitButton;

    [Header("GameUIButtons")]
    public Button HomeButton;
    public Button retryButton;
    public Button retryButtonCompletedUI;

    [Header("Scores")]
    public TextMeshProUGUI totalMatchesTxt;
    public TextMeshProUGUI totalTurnTxt;
    public TextMeshProUGUI totalTurnCompletedUI;

    private int gridX = 0;
    private int gridY = 0;

    [Header("UI_Window")]
    public GameObject homeUI;
    public GameObject gameUI;
    public GameObject completedUI;

    private void Awake()
    {
        GameManager.Instance.updateScores += UpdateScore;
        GameManager.Instance.onGameEnded += OnGameCompleted;
    }

    void Start()
    {
        grid2x2Button.onClick.AddListener(() => Update2X2GridLayout());
        grid2x3Button.onClick.AddListener(() => Update2X3GridLayout());
        grid4x4Button.onClick.AddListener(() => Update4X4GridLayout());
        grid5x6Button.onClick.AddListener(() => Update5X6GridLayout());
        HomeButton.onClick.AddListener(() => OnHomeButtonClicked());
        retryButton.onClick.AddListener(() => OnRetryButtonClicked());
        retryButtonCompletedUI.onClick.AddListener(() => OnRetryButtonClicked());
        quitButton.onClick.AddListener(() => OnQuitPressed());
    }

    void Update2X2GridLayout()
    {
        gridX = 2;
        gridY = 2;
        AudioManager.Instance.PlayButtonFx();
        SwitchToGameUI();
        GameManager.Instance.onGameStarted.Invoke(gridX, gridY);
    }

    void Update2X3GridLayout()
    {
        gridX = 2;
        gridY = 3;
        AudioManager.Instance.PlayButtonFx();
        SwitchToGameUI();
        GameManager.Instance.onGameStarted.Invoke(gridX, gridY);
    }
    void Update4X4GridLayout()
    {
        gridX = 4;
        gridY = 4;
        AudioManager.Instance.PlayButtonFx();
        SwitchToGameUI();
        GameManager.Instance.onGameStarted.Invoke(gridX, gridY);
    }
    void Update5X6GridLayout()
    {
        gridX = 5;
        gridY = 6;
        AudioManager.Instance.PlayButtonFx();
        SwitchToGameUI();
        GameManager.Instance.onGameStarted.Invoke(gridX, gridY);
    }

    protected void SwitchToGameUI()
    {
        gameUI.SetActive(true);
        homeUI.SetActive(false);
    }

    protected void SwitchToHomeUI()
    {
        homeUI.SetActive(true);
        gameUI.SetActive(false);
    }

    void OnHomeButtonClicked()
    {
        AudioManager.Instance.PlayButtonFx();
        SwitchToHomeUI();
        GameManager.Instance.onRestartGame.Invoke();
    }
    void OnGameCompleted()
    {
        completedUI.SetActive(true);
    }

    void OnRetryButtonClicked()
    {
        AudioManager.Instance.PlayButtonFx();
        completedUI.SetActive(false);
        GameManager.Instance.onRestartGame.Invoke();
        GameManager.Instance.onGameStarted.Invoke(gridX, gridY);
    }

    public void UpdateScore(int turns , int matches)
    {
        totalMatchesTxt.text = "Matches : " + matches+" / "+ (gridX * gridY) / 2;
        totalTurnTxt.text = "Turns : " + turns;
        totalTurnCompletedUI.text = totalTurnTxt.text;
    }

    void OnQuitPressed()
    {
        Application.Quit();
    }
}
