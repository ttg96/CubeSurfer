using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    [SerializeField]
    private Text currentScore;
    [SerializeField]
    private Text finalScore;
    [SerializeField]
    private Text highScore;
    [SerializeField]
    private GameObject inGameUI;
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject levelSelect;
    [SerializeField]
    private GameObject canvasPanel;
    [SerializeField]
    private GameObject gameOverScreen;
    [SerializeField]
    private GameObject nextLevel;
    [SerializeField]
    private Button[] levelButtons;
    [SerializeField]
    private AudioClip[] clips;
    private AudioSource source;
    private LevelBuilder levelBuilder;


    private void Start() {
        PauseGame(true);
        levelBuilder = GetComponent<LevelBuilder>();
        source = GetComponent<AudioSource>();
        if (!PlayerPrefs.HasKey("UnlockedLevels")) PlayerPrefs.SetInt("UnlockedLevels", 0);
        if (!PlayerPrefs.HasKey("HighScore")) PlayerPrefs.SetInt("HighScore", 0);
        UpdateHighscores();
    }

    public void UpdateScore(int score) {
        currentScore.text = "Score: " + score;
    }

    public void UpdateHighscores() {
        highScore.text = "Current score: " + PlayerPrefs.GetInt("HighScore");
    }

    public void UpdateFinalScores(int score) {
        finalScore.text = "Final Score: " + score;
    }

    public void PauseGame(bool state) {
        if (state) {
            Time.timeScale = 0;
        } else {
            Time.timeScale = 1;
        }
    }

    public void ExitGameToMenu() {
        PauseGame(true);
        canvasPanel.SetActive(true);
        LoadMenu();
    }

    public void LoadMenu() {
        mainMenu.SetActive(true);
        levelSelect.SetActive(false);
        gameOverScreen.SetActive(false);
        UpdateHighscores();
    }

    public void LoadLevelScreen() {
        mainMenu.SetActive(false);
        levelSelect.SetActive(true);
        UpdateLevelSelection();
    }

    public void UpdateLevelSelection() {
        int unlocked = PlayerPrefs.GetInt("UnlockedLevels");
        for (int i = 0; i < levelButtons.Length; i++) {
            if(i <= unlocked) {
                levelButtons[i].interactable = true;
            }
        }
    }

    public void LoadSelectedLevel(int level) {
        PauseGame(false);
        canvasPanel.SetActive(false);
        levelBuilder.LevelSelect(level-1);
        levelSelect.SetActive(false);
        inGameUI.SetActive(true);
    }

    public void LoadRandomLevel() {
        PauseGame(false);
        canvasPanel.SetActive(false);
        levelBuilder.LoadLevel();
        levelSelect.SetActive(false);
        inGameUI.SetActive(true);
    }

    public void Restart() {
        canvasPanel.SetActive(false);
        gameOverScreen.SetActive(false);
        inGameUI.SetActive(true);
        levelBuilder.RestartLevel();
        PauseGame(false);
    }

    public void LoadNextLevel() {
        PauseGame(false);
        canvasPanel.SetActive(false);
        gameOverScreen.SetActive(false);
        inGameUI.SetActive(true);
        levelBuilder.NextLevel();
    }

    public void GameOver(int score, bool finished) {
        PauseGame(true);
        inGameUI.SetActive(false);
        canvasPanel.SetActive(true);
        gameOverScreen.SetActive(true);
        UpdateFinalScores(score);
        if (finished) {
            PlayAudioClip(2);
            nextLevel.SetActive(true);
            PlayerPrefs.SetInt("UnlockedLevels", PlayerPrefs.GetInt("UnlockedLevels") + 1);
        } else {
            nextLevel.SetActive(false);
        }
    }

    public void PlayAudioClip(int clip) {
        source.PlayOneShot(clips[clip]);
    }

}
