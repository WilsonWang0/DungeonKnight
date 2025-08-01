using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalToNextRoom : MonoBehaviour
{
    public Transform teleportTarget;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && teleportTarget != null)
        {
            other.transform.position = teleportTarget.position;
            Debug.Log("Player teleported to new room.");
        }
    }
}