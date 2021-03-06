using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChallengeUI : MonoBehaviour
{
    public Text title, goal, tip;
    
    public void UpdateAppearance()
    {
        Level level;
        int levelIndex = FindObjectOfType<GameManager>().levelIndex;
        level = LevelManager.GetLevel(levelIndex);

        title.text = level.title;
        goal.text = level.goal;
        tip.text = level.tip;
    }
}
