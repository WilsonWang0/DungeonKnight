using UnityEngine;

public class Chest : MonoBehaviour
{
    private Animator chestAnimator;
    public AudioClip openSound;
    private AudioSource audioSource;
    private bool isOpened = false;
    public int coinAmount = 15;

    private GameObject choicePanel;
    private GameObject characterSelectPanel;

    void Start()
    {
        chestAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        // Find ChoicePanel
        GameObject choiceCanvas = GameObject.Find("ChoiceCanvas");
        if (choiceCanvas != null)
        {
            choicePanel = choiceCanvas.transform.Find("ChoicePanel")?.gameObject;
        }

        // Find CharacterSelectPanel
        GameObject characterCanvas = GameObject.Find("CharacterSelectCanvas");
        if (characterCanvas != null)
        {
            characterSelectPanel = characterCanvas.transform.Find("CharacterSelectPanel")?.gameObject;
        }

        // Deactivate both
        if (choicePanel != null) choicePanel.SetActive(false);
        if (characterSelectPanel != null) characterSelectPanel.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isOpened && other.CompareTag("Player"))
        {
            isOpened = true;
            chestAnimator.SetTrigger("OpenChest");
            audioSource.PlayOneShot(openSound);

            PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
            if (playerInventory != null)
            {
                playerInventory.AddCoins(coinAmount);

                // ✅ Show coin message
                UIManager uiManager = FindObjectOfType<UIManager>();
                if (uiManager != null)
                {
                    uiManager.ShowChestMessage($"You just earned {coinAmount} coins.");
                }
            }

            if (choicePanel != null) choicePanel.SetActive(true);

            if (characterSelectPanel != null)
            {
                characterSelectPanel.SetActive(true);  // Only enable once
            }

            Time.timeScale = 0f;
        }
    }


}
