using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameMenuModes
{
    gameplay, toolSelect, challengeInfo, success, fail
}

public class GameplayUI : MonoBehaviour
{
    public Image selectedToolImage;
    public Text selectedToolCount;
    public Toggle hideTipsToggle;

    public GameObject selectedToolContainer, scoreContainer;
    public Canvas toolSelectCanvas, gameplayCanvas, challengeCanvas, successCanvas, failCanvas, sandboxCanvas, stopwatchCanvas;

    private GameMenuModes gameMenuMode = GameMenuModes.gameplay;

    private static Sprite[] toolSprites;
    private CreateObjectUI createObjectUI;

    void Start()
    {
        //bool isSandbox = GameManager.GetGameManager().isSandboxMode;
        int levelIndex = GameManager.GetGameManager().levelIndex;
        //if (PlayerProfile.GetPlayerProfile().GetShowTip(levelIndex, isSandbox))
        //    SetGameMenuMode(GameMenuModes.challengeInfo);
        //else
        //    SetGameMenuMode(GameMenuModes.gameplay);
        //SelectAnyAvailableTool();
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.T))
        //    SetGameMenuMode(gameMenuMode == GameMenuModes.gameplay ? GameMenuModes.toolSelect : GameMenuModes.gameplay);

        //if (Input.GetKeyDown(KeyCode.G) || Input.GetKeyDown(KeyCode.Space))
        //{
        //    SetGameMenuMode(GameMenuModes.gameplay);
        //    GameManager.GetGameManager().SetGamePause(false);
        //}

        //if (Input.GetKeyDown(KeyCode.R))
        //    RestartLevel();

        //if (Input.GetKeyDown(KeyCode.C))
        //    SetGameMenuMode(gameMenuMode == GameMenuModes.gameplay ? GameMenuModes.challengeInfo : GameMenuModes.gameplay);

        //if (Input.GetKeyDown(KeyCode.M))
        //    GotoMainMenu();

        //for (int i = 1; i < 10; i++)
        //    if (GetAlphaKeyDown(i))
        //    {
        //        Tools tool = (Tools)(i - 1);
        //        SelectToolClick(tool);
        //    }
    }

    private bool GetAlphaKeyDown(int number)
    {
        return Input.GetKeyDown((KeyCode)(48 + number));
    }

    public static void SetShowCursor(bool showCursor)
    {
        Cursor.lockState = showCursor ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = showCursor;
    }

    private CreateObjectUI GetCreateObjectUI()
    {
        if (createObjectUI == null)
            createObjectUI = FindObjectOfType<CreateObjectUI>();
        return createObjectUI;
    }

    private void RestartLevel()
    {
        GameManager.GetGameManager().SaveScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void GotoMainMenu()
    {
        GameManager.GetGameManager().SaveScore();
        SceneManager.LoadScene("Main Menu");
    }

    public void HideTipsButtonClick()
    {
        PlayerProfile pp = PlayerProfile.GetPlayerProfile();
        //bool isSandbox = GameManager.GetGameManager().isSandboxMode;

        int levelIndex = GameManager.GetGameManager().levelIndex;
        //pp.SetShowTip(levelIndex, !hideTipsToggle.isOn, isSandbox);

    }

    public void ResetSandboxClick()
    {
        SandboxManager.ClearSandboxHistory();
        SoundManager.PlaySound(GameSounds.click);
        RestartLevel();
    }

    public GameMenuModes GetGameMenuMode()
    {
        return gameMenuMode;
    }

    public void SetGameMenuMode(GameMenuModes newGameMenuMode)
    {
        int levelIndex = GameManager.GetGameManager().levelIndex;
        //bool isSandbox = GameManager.GetGameManager().isSandboxMode;

        gameMenuMode = newGameMenuMode;

        SetShowCursor(gameMenuMode != GameMenuModes.gameplay);
        gameplayCanvas.enabled = gameMenuMode == GameMenuModes.gameplay;
        MainMenu.SetUIVisibility(scoreContainer, ShouldShowScore());
        //toolSelectCanvas.enabled = gameMenuMode == GameMenuModes.toolSelect && !isSandbox;
        //sandboxCanvas.enabled = gameMenuMode == GameMenuModes.toolSelect && isSandbox;
        challengeCanvas.enabled = gameMenuMode == GameMenuModes.challengeInfo;
        successCanvas.enabled = gameMenuMode == GameMenuModes.success;
        failCanvas.enabled = gameMenuMode == GameMenuModes.fail;
        stopwatchCanvas.enabled = ShouldShowStopwatch();

        PlayerProfile pp = PlayerProfile.GetPlayerProfile();
        //hideTipsToggle.isOn = !pp.GetShowTip(levelIndex, isSandbox);
        challengeCanvas.GetComponent<ChallengeUI>().UpdateAppearance();
        FindObjectOfType<MouseLook>().enabled = gameMenuMode == GameMenuModes.gameplay;
    }

    private bool ShouldShowStopwatch()
    {
        GameManager gm = GameManager.GetGameManager();
        return gm.successCondition.time > 0 && gameMenuMode == GameMenuModes.gameplay;
    }

    private bool ShouldShowScore()
    {
        return GameManager.GetGameManager().successCondition.minScore > 0 && gameMenuMode == GameMenuModes.gameplay;
    }

    public void NextLevelButtonClick()
    {
        SoundManager.PlaySound(GameSounds.click);
        int levelIndex = GameManager.GetGameManager().levelIndex + 1;
        SceneManager.LoadScene(LevelManager.GetSceneIndexFromLevelIndex(levelIndex));
    }

    public void RestartLevelButtonClick()
    {
        SoundManager.PlaySound(GameSounds.click);
        RestartLevel();
    }

    public void ResumeGameplayButton()
    {
        SoundManager.PlaySound(GameSounds.click);
        SetGameMenuMode(GameMenuModes.gameplay);
    }

    private Sprite GetToolSprite(int toolID)
    {
        if (toolSprites == null)
        {
            int spriteCount = 6;
            toolSprites = new Sprite[spriteCount];
            for (int i = 0; i < spriteCount; i++)
            {
                string path = "sprites/tool" + i;
                toolSprites[i] = Resources.Load<Sprite>(path);
                if (toolSprites[i] == null)
                    Debug.LogError(path + " is null");

            }
        }
        return toolSprites[toolID];
    }
}
