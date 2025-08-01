using UnityEngine;

public class CharacterSwitcher : MonoBehaviour
{
    public GameObject character1;
    public GameObject character2;

    private bool isUsingCharacter1 = true;

    public Camera mainCamera; 


    public void SwitchCharacter()
    {
        isUsingCharacter1 = !isUsingCharacter1;

        character1.SetActive(isUsingCharacter1);
        character2.SetActive(!isUsingCharacter1);

        // Re-parent camera to the new active character
        Transform newTarget = isUsingCharacter1 ? character1.transform : character2.transform;
        mainCamera.transform.SetParent(newTarget);
        mainCamera.transform.localPosition = new Vector3(0, 0, -50); // Adjust as needed
    }


}

