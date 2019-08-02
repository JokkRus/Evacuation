using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagScripts : MonoBehaviour
{
    public int Mokrost = 0;
    public bool ready = false;

    void Start()
    {
        Messenger<int>.Broadcast(GameEvent.MokrostUpdated, Mokrost);
    }

    public void ChangeMokrost(int value)
    {
        Mokrost += value;
        if (Mokrost <= 0)
        {
            Mokrost = 0;
            Destroy(gameObject);
            Managers.Inventory.equippedItemName = "";
        }
        else if (Mokrost >= 100) Mokrost = 100;
        Messenger<int>.Broadcast(GameEvent.MokrostUpdated, Mokrost);
    }
}

