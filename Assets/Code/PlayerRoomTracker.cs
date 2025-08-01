using System;
using UnityEngine;

public class PlayerRoomTracker : MonoBehaviour
{
    private RoomManager _currentRoom;
    public event Action<RoomManager> OnRoomChanged;

    public RoomManager currentRoom
    {
        get => _currentRoom;
        set
        {
            if (_currentRoom != value)
            {
                _currentRoom = value;
                OnRoomChanged?.Invoke(_currentRoom);
            }
        }
    }
}
