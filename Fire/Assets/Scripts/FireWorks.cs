using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWorks : MonoBehaviour
{
    [SerializeField] private ParticleSystem fireWorks;
    private float tim = 0f;

    private void Start()
    {
        fireWorks.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            fireWorks.Play();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            tim += Time.deltaTime;
            if (tim >= 3f)
            {
                Messenger<string, int>.Broadcast(GameEvent.DialogUpdated, "Поздравляем! Вы победили", 1);
            }
        }
    }
}
