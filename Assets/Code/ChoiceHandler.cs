using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoiceHandler : MonoBehaviour
{
    public Controller playerController;     // Drag the Character GameObject here
    public GameObject choicePanel;           // Drag the ChoicePanel here manually

    // Called when "Continue" button is clicked
    public void OnContinue()
    {
        ResumeGame();
    }

    // Called when "Restart" button is clicked
    public void OnRestart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;

        if (choicePanel != null)
        {
            choicePanel.SetActive(false);
        }
        else
        {
            Debug.LogWarning("ChoicePanel was not assigned!");
        }
    }
}
