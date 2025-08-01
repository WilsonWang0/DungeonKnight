using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    private List<GameObject> enemiesInRoom = new List<GameObject>();

    public GameObject chestPrefab;
    private bool chestSpawned = false;
    public Transform chestSpawnPoint;
    public GameObject exitBarrier;
    public bool isFinalRoom = false;
    public GameObject portalObject;

    public bool isWinRoom = false;
    public GameObject winPanel;
    public GameObject shopPanel;
    public Transform endlessTelePoint;


    //Sounds Effects
    private AudioSource audioSource;
    public AudioClip portalOpenClip;



    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void RegisterEnemy(GameObject enemy)
    {
        enemiesInRoom.Add(enemy);
        // Debug.Log("Enemy registered!");
    }

    public void UnregisterEnemy(GameObject enemy)
    {
        enemiesInRoom.Remove(enemy);
        Debug.Log($"Enemy defeated in {name}. Remaining: {enemiesInRoom.Count}");

        if (!chestSpawned && enemiesInRoom.Count == 0)
        {
            Debug.Log("All enemies defeated!" + name);
            if (isFinalRoom)
            {
                if (portalObject != null)
                {
                    //Play Sound Effect
                    if (portalOpenClip != null && audioSource != null)
                    {
                        audioSource.PlayOneShot(portalOpenClip);
                    }

                    portalObject.SetActive(true); // Make portal visible and usable
                    Debug.Log("Portal activated in " + name);
                }
                else
                {
                    Debug.LogWarning("Portal object not assigned in final room!");
                }
            }
            else if (isWinRoom)
            {
                ShowWinScreen();
                Debug.Log("Game won!");
            }
            else {
                SpawnChest();
                chestSpawned = true;
            }
            
            if (exitBarrier != null)
            {
                exitBarrier.SetActive(false);
                Debug.Log($"Exit unlocked for {name}!");
            }
        }
    }
    void SpawnChest()
    {
        if (chestPrefab != null)
        {
            Vector3 spawnPosition = chestSpawnPoint != null ? chestSpawnPoint.position : transform.position;
            Instantiate(chestPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Chest prefab not assigned!");
        }
    }

    void ShowWinScreen()
    {
        if (winPanel != null)
        {
            shopPanel.SetActive(false);
            winPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }


}

