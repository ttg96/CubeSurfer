using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    [SerializeField]
    private Text currentScore;
    [SerializeField]
    private Text currentMultiplier;
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

    //Update score on game UI
    public void UpdateScore(int score) {
        currentScore.text = "Score: " + score;
    }

    //Update multiplier on game UI
    public void UpdateMultiplier(int multi) {
        currentMultiplier.text = "Multiplier: x" + multi;
    }

    //Update highscore on main screen
    public void UpdateHighscores() {
        highScore.text = "Current score: " + PlayerPrefs.GetInt("HighScore");
    }

    //Update score on game over screen
    public void UpdateFinalScores(int score) {
        finalScore.text = "Final Score: " + score;
        PlayerPrefs.SetInt("HighScore", PlayerPrefs.GetInt("HighScore") + score);
    }

    //Pause game
    public void PauseGame(bool state) {
        if (state) {
            Time.timeScale = 0;
        } else {
            Time.timeScale = 1;
        }
    }

    //Exit game to the main menu
    public void ExitGameToMenu() {
        PauseGame(true);
        canvasPanel.SetActive(true);
        LoadMenu();
    }

    //Load the main menu
    public void LoadMenu() {
        mainMenu.SetActive(true);
        levelSelect.SetActive(false);
        gameOverScreen.SetActive(false);
        UpdateHighscores();
    }

    //Load the level select screen
    public void LoadLevelScreen() {
        mainMenu.SetActive(false);
        levelSelect.SetActive(true);
        UpdateLevelSelection();
    }

    //Update which levels the player can select based on player prefs
    public void UpdateLevelSelection() {
        int unlocked = PlayerPrefs.GetInt("UnlockedLevels");
        for (int i = 0; i < levelButtons.Length; i++) {
            if(i <= unlocked) {
                levelButtons[i].interactable = true;
            }
        }
    }

    //Load selected level
    public void LoadSelectedLevel(int level) {
        PauseGame(false);
        canvasPanel.SetActive(false);
        levelBuilder.LevelSelect(level-1);
        levelSelect.SetActive(false);
        inGameUI.SetActive(true);
    }

    //Load a randomly generated level
    public void LoadRandomLevel() {
        PauseGame(false);
        canvasPanel.SetActive(false);
        levelBuilder.LoadLevel();
        levelSelect.SetActive(false);
        inGameUI.SetActive(true);
    }

    //Restart current level
    public void Restart() {
        canvasPanel.SetActive(false);
        gameOverScreen.SetActive(false);
        inGameUI.SetActive(true);
        levelBuilder.RestartLevel();
        PauseGame(false);
    }

    //Load the next level
    public void LoadNextLevel() {
        PauseGame(false);
        canvasPanel.SetActive(false);
        gameOverScreen.SetActive(false);
        inGameUI.SetActive(true);
        levelBuilder.NextLevel();
    }

    //Game over UI
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

    //Play selected sound clip
    public void PlayAudioClip(int clip) {
        source.PlayOneShot(clips[clip]);
    }

}
