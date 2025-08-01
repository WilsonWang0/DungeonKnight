using UnityEngine;
using UnityEngine.UI;
using TMPro; // Ensure you have TextMeshPro package installed

public class KillCount : MonoBehaviour
{
    public static KillCount Instance; 

    public TMP_Text killText;
    private int killCount = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddKill()
    {
        killCount++;
        killText.text = "Kills: " + killCount;
    }
}