using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class guisher : MonoBehaviour
{
    [SerializeField] private GameObject particle;
    public int supply = 500;
    public int Force = 500;

	void Start ()
    {
        Messenger<int>.Broadcast(GameEvent.SupplyUpdated, supply);
    }
	

	void Update ()
    {
		if (Input.GetKey(KeyCode.Mouse1) && supply > 0)
        {
            particle.GetComponent<ParticleSystem>().Simulate(1);
            supply--;
            Messenger<int>.Broadcast(GameEvent.SupplyUpdated, supply);
            particle.GetComponent<CapsuleCollider>().enabled = true;
            if (supply == 0)
            {
                particle.GetComponent<ParticleSystem>().Simulate(0);
                particle.GetComponent<CapsuleCollider>().enabled = false;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            particle.GetComponent<ParticleSystem>().Simulate(0);
            particle.GetComponent<CapsuleCollider>().enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            transform.SetParent(null);
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.left * Force);
            gameObject.GetComponent<guisher>().enabled = false;
            Managers.Inventory.equippedItemName = "";
            // Добавить очистку equippedItem
        }
	}
}
