using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    public RoomManager roomManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerRoomTracker tracker = other.GetComponent<PlayerRoomTracker>();
            if (tracker != null)
            {
                tracker.currentRoom = roomManager;
                Debug.Log("Entered room: " + roomManager.name);
            }
        }
    }
}

