using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeManager : MonoBehaviour, IGameManager
{
    [SerializeField] GameObject smokePref;
    [SerializeField] Transform spawnPoint;
    private GameObject Smoke;
    public float Gustota { get; private set; }
    public ManagerStatus status { get; private set; }


    public void StartUp()
    {
        Debug.Log("Smoke manager starting...");
        Smoke = Instantiate(smokePref, spawnPoint.position, transform.rotation);
        Smoke.transform.Rotate(90, 0, 0);
        if (Managers.Player.save.GetComponent<SaveScript>().gustota != null)
        {
            Gustota = Managers.Player.save.GetComponent<SaveScript>().gustota;
        }
        else
        {
            Gustota = 0;
        }
        Messenger<float>.AddListener(GameEvent.FireUpdated, GustotaUpdate);
        status = ManagerStatus.Started;
    }

    public void OnDestroy()
    {
        Messenger<float>.RemoveListener(GameEvent.FireUpdated, GustotaUpdate);
    }

    public void GustotaUpdate(float value)
    {
        Gustota += value;
        if (Gustota >= 1f) Gustota = 1f;
        else if (Gustota <= 0) Gustota = 0f;
        Smoke.GetComponent<ParticleSystem>().startColor = new Color(0, 0, 0, Gustota);
    }

}
