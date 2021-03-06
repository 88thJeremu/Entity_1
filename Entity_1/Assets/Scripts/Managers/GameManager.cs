using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public bool startPaused = true;
    private bool isPaused = false;
    private bool hasSimulationBegun = false;
    public int levelIndex = 0;
    private float elapsedTime = 0;
    private bool hasSuccessAppeared = false, hasFailureAppeared = false;
    public float startingScore = 0;
    private float score = 0;
    private float mustSeeObjectTimer = 0;
    private Renderer mustSeeObjectRenderer;
    private GameObject player;
    private static GameManager gameManager;

    [System.Serializable]
    public class VictoryCondition
    {
        public float time = 5;
        public float failTime = 15;
        public float minScore = 0;
        public GameObject mustSeeObject;
        public bool mustMoveAround = false;
    }
    public VictoryCondition successCondition;

    void Start()
    {
        gameManager = this;
        StartUI();
        score = startingScore;
        SetGamePause(startPaused);
    }
    
    public static GameManager GetGameManager()
    {
        if (gameManager == null)
            gameManager = FindObjectOfType<GameManager>();
        return gameManager;
    }
    
    //Maybe recycle the score to indicate the time elapsed for the leaderboard?
    public float GetScore()
    {
        return score;
    }

    public void AddScore(float points)
    {
        score += points;
    }

    /// <summary>
    /// This function is called inside the "Start" function (also in GameManager.cs).
    /// A "prefab" is the "idea" of a GameObject. You can think of it as a cookie cutter.
    /// Whereas a "GameObject" is an actual object that we see in the game.
    /// Here, we use the "prefab" or "cookie cutter" to create a "GameObject" or "cookie".
    /// There are other ways to make GameObjects in Unity3D. For example, you can simple click and drag 
    /// 3D models into the game, and use your mouse to move them around. I prefer to create most things with code instead.
    /// Here's a video about creating GameObjects:
    /// https://www.youtube.com/watch?v=4vLYzhN4UlQ&list=PLRf-PfhVvwFDWrHVWYj9tiRze0rtB5Sn0&index=5
    /// </summary>

    private void StartUI()
    {
        //GameObject prefab = Resources.Load<GameObject>("prefabs/UserInterface");
        //GameObject gameplayGameObject = Instantiate(prefab);
        //gameplayGameObject.transform.position = Vector3.zero;
    }

    public bool HasSimulationBegun()
    {
        return hasSimulationBegun;
    }

    public void SetGamePause(bool isPaused)
    {
        this.isPaused = isPaused;
        if (!isPaused && !HasSimulationBegun())
        {
            hasSimulationBegun = true;
            foreach (MovingChargedObject mco in FindObjectsOfType<MovingChargedObject>())
                mco.SetFrozenPosition(false);
        }

        foreach (ChargedObject co in FindObjectsOfType<ChargedObject>())
            co.enabled = !isPaused;
        foreach (MovingChargedObject mco in FindObjectsOfType<MovingChargedObject>())
            mco.enabled = !isPaused;
        foreach (Rigidbody rigidbody in FindObjectsOfType<Rigidbody>())
            rigidbody.constraints = isPaused ? RigidbodyConstraints.FreezeAll : 0;
    }

    public float GetVictoryTimerDuration()
    {
        return successCondition.time;
    }

    public bool GetIsPaused()
    {
        return isPaused;
    }

    private GameObject GetPlayer()
    {
        if (player == null)
            player = FindObjectOfType<FlightController>().gameObject;
        return player;
    }

    //recycle this function to indicate when player has reached goal
    public bool PlayerHasWon()
    {
        //if (successCondition.mustSeeObject != null && mustSeeObjectTimer < 1)
        //    return false;
        //if (successCondition.mustMoveAround && !PlayerHasMovedSignificantly())
        //    return false;
        //return elapsedTime > successCondition.time && score >= successCondition.minScore;
        return false;
    }

    public void PlayerWins()
    {
        FindObjectOfType<GameplayUI>().SetGameMenuMode(GameMenuModes.success);
        SoundManager.PlaySound(GameSounds.victory);
        hasSuccessAppeared = true;
        SaveScore();
        PlayerProfile.GetPlayerProfile().SetWin(levelIndex, true);
    }

    public void PlayerLoses()
    {
        FindObjectOfType<GameplayUI>().SetGameMenuMode(GameMenuModes.fail);
        SoundManager.PlaySound(GameSounds.fail);
        hasFailureAppeared = true;
    }

    //recycle this function to report failure condition
    public bool PlayerHasLost()
    {
        //return successCondition.failTime > 0 && elapsedTime > successCondition.failTime && !PlayerHasWon();
        return false;
    }

    private Renderer GetMustSeeObjectRenderer()
    {
        if (mustSeeObjectRenderer == null)
            mustSeeObjectRenderer = successCondition.mustSeeObject.GetComponent<Renderer>();
        return mustSeeObjectRenderer;
    }

    public void SaveScore()
    {
        PlayerProfile pp = PlayerProfile.GetPlayerProfile();
        if (pp.GetScore(levelIndex) < score)
            pp.SetScore(levelIndex, score);
    }

    void Update()
    {
        if (!isPaused)
        {
            elapsedTime += Time.deltaTime;
            if (!hasSuccessAppeared && PlayerHasWon())
                PlayerWins();
            if (!hasFailureAppeared && PlayerHasLost())
                PlayerLoses();
        }

        if (successCondition.mustSeeObject != null && GetMustSeeObjectRenderer().isVisible)
            mustSeeObjectTimer += Time.deltaTime;
    }
}
