using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemScript : MonoBehaviour
{
    [SerializeField] private string name;
    public string description;

    void OnTriggerEnter(Collider coll)
    {
        Messenger<string, int, string>.Broadcast(GameEvent.DialogUpdated, description, 0, name);
        Managers.Inventory.AddItem(name);
        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "Icon.png", true);
    }
}
