using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassZone : MonoBehaviour
{
    [SerializeField] int floor;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            int chance = Random.Range(1, Managers.Player.Health + 1);
            if (chance > 23 * floor)
            {
                Messenger<string, int>.Broadcast(GameEvent.DialogUpdated, "Поздравляем! Вы победили", 1);
            }
            else
            {
                Messenger<string, int>.Broadcast(GameEvent.DialogUpdated, "Вы проиграли", 1);

            }
        }
    }
}
