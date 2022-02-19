using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToolSelectUI : MonoBehaviour
{
    public GameObject[] toolContainers;
    private Text[] counterTexts;

    private Text[] GetCountTexts()
    {
        if (counterTexts == null)
        {
            counterTexts = new Text[toolContainers.Length];
            for (int i = 0; i < toolContainers.Length; i++)
            {
                foreach (GameObject child in ParentChildFunctions.GetAllChildren(toolContainers[i]))
                    if (child.name.Equals("count"))
                    {
                        counterTexts[i] = child.GetComponent<Text>();
                        break;
                    }
            }
        }
        return counterTexts;
    }
}
