using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public GameObject character1;
    public GameObject character2;
    public Camera mainCamera;

    private GameObject characterSelectPanel;
    private bool usingCharacter1 = true;

    void Start()
    {
        character1.SetActive(true);
        character2.SetActive(false);
        AttachCameraToCharacter(character1);
    }

    public void SwitchCharacter()
    {
        usingCharacter1 = !usingCharacter1;

        character1.SetActive(usingCharacter1);
        character2.SetActive(!usingCharacter1);

        AttachCameraToCharacter(usingCharacter1 ? character1 : character2);
    }

    void AttachCameraToCharacter(GameObject character)
    {
        if (mainCamera != null)
        {
            mainCamera.transform.SetParent(character.transform);
            mainCamera.transform.localPosition = new Vector3(0, 0, -10);  // Standard 2D camera offset
        }
    }

    public void ShowCharacterSelectPanel()
    {
        // Only find the panel when it's actually needed
        if (characterSelectPanel == null)
        {
            GameObject canvas = GameObject.Find("CharacterSelectCanvas");
            if (canvas != null)
            {
                characterSelectPanel = canvas.transform.Find("CharacterSelectPanel")?.gameObject;
            }
        }

        if (characterSelectPanel != null)
        {
            characterSelectPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("CharacterSelectPanel not found in scene!");
        }
    }
}
