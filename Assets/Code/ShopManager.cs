using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public GameObject shopPanel;
    public Controller playerController;
    public PlayerInventory playerInventory;

    public TMP_Text shopMessageText; // Assign in Inspector

    public void OpenShop()
    {
        shopPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void BuyHealth()
    {
        PlayerInventory inventory = playerController.GetComponent<PlayerInventory>();
        PlayerHealth health = playerController.GetComponent<PlayerHealth>();

        if (inventory != null && health != null)
        {
            if (health.currentHealth >= health.maxHealth)
            {
                StartCoroutine(ShowMessage("You already have full health!", Color.green));
                return;
            }
            else if (inventory.coins >= 5)
            {
                inventory.coins -= 5;
                inventory.UpdateUI();
                health.IncreaseHealth(5f); // Or restore health
                StartCoroutine(ShowMessage("Health recharged!", Color.green));
            }
            else
            {
                StartCoroutine(ShowMessage("Not enough coins for health!", Color.red));
            }
        }
    }



    public void UpgradeShield()
    {
        PlayerInventory inventory = playerController.GetComponent<PlayerInventory>();
        Controller controller = playerController.GetComponent<Controller>();

        if (inventory != null && controller != null)
        {
            if (controller.shieldReady)  // Add a helper if not already
            {
                StartCoroutine(ShowMessage("Shield already unlocked!", Color.green));
                return;
            }

            if (inventory.coins >= 10)
            {
                inventory.coins -= 10;
                inventory.UpdateUI();
                controller.EnableShieldAbility();
                StartCoroutine(ShowMessage("Shield unlocked! Press Space Bar to use.", Color.green));
            }
            else
            {
                StartCoroutine(ShowMessage("Not enough coins for shield!", Color.red));
            }
        }
    }


    public void BuyCircleSkill()
    {
        PlayerInventory inventory = playerController.GetComponent<PlayerInventory>();
        Controller controller = playerController.GetComponent<Controller>();

        if (inventory != null && controller != null)
        {
            if (controller.circleSkillUnlocked)
            {
                StartCoroutine(ShowMessage("Skill 1 already learned!", Color.green));
                return;
            }

            if (inventory.coins >= 10)
            {
                inventory.coins -= 10;
                inventory.UpdateUI();
                controller.circleSkillUnlocked = true;
                controller.UpdateSkillIcons();
                StartCoroutine(ShowMessage("Skill 1 unlocked! Press K to use.", Color.green));
            }
            else
            {
                StartCoroutine(ShowMessage("Not enough coins for Skill 1!", Color.red));
            }
        }
    }



    public void BuyLongRangeSkill()
    {
        PlayerInventory inventory = playerController.GetComponent<PlayerInventory>();
        Controller controller = playerController.GetComponent<Controller>();

        if (inventory != null && controller != null)
        {
            if (controller.longRangeSkillUnlokced)
            {
                StartCoroutine(ShowMessage("Skill 2 already learned!", Color.green));
                return;
            }

            if (inventory.coins >= 5)
            {
                inventory.coins -= 5;
                inventory.UpdateUI();
                controller.longRangeSkillUnlokced = true;
                controller.UpdateSkillIcons();
                StartCoroutine(ShowMessage("Skill 2 unlocked! Press L to use.", Color.green));
            }
            else
            {
                StartCoroutine(ShowMessage("Not enough coins for Skill 2!", Color.red));
            }
        }
    }



    private IEnumerator ShowMessage(string message, Color color)
    {
        if (shopMessageText != null)
        {
            shopMessageText.text = message;
            shopMessageText.color = color;
            yield return new WaitForSecondsRealtime(2.5f);
            shopMessageText.text = "";
        }
    }
}
