using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTracker : MonoBehaviour
{
    void Start()
    {
        RoomManager room = GetComponentInParent<RoomManager>();
        if (room != null)
        {
            room.RegisterEnemy(gameObject);
        }
        else
        {
            Debug.LogWarning("Enemy couldn't find a RoomManager!");
        }
    }
}
