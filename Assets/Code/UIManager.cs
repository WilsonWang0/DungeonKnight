using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text chestMessageText;  // Drag your UI Text here in the Inspector

    public void ShowChestMessage(string message, float duration = 5f)
    {
        StopAllCoroutines();  // Stop any existing message
        StartCoroutine(ShowMessageCoroutine(message, duration));
    }

    private IEnumerator ShowMessageCoroutine(string message, float duration)
    {
        chestMessageText.text = message;
        chestMessageText.gameObject.SetActive(true);

        yield return new WaitForSeconds(duration);

        chestMessageText.gameObject.SetActive(false);
    }
}

