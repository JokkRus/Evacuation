using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }
    public int Health { get; private set; }
    public int MaxHealth { get; private set; }
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject cameraPrefab;
    [SerializeField] Vector3 offsetCam;
    private int leader = 0;
    [SerializeField] float smokeDamage = 10f;
    public  GameObject save;
    private GameObject player;
    private GameObject camera;
    private GameObject cam;
    public GameObject ing;

    float time = 0;

    public void StartUp()
    {
        ing = GameObject.Find("Menu");
        Debug.Log("Player strarting...");
        MaxHealth = 100;
        Managers.Inventory.items = save.GetComponent<SaveScript>().Inv;
        if (save.GetComponent<SaveScript>().runGame == false)
        {
            save.GetComponent<SaveScript>().Health = 100;
            save.GetComponent<SaveScript>().gustota = 0;
            save.GetComponent<SaveScript>().location = 0;
            save.GetComponent<SaveScript>().Inv = null;
            save.GetComponent<SaveScript>().equippedItem = "";
            save.GetComponent<SaveScript>().gu = 0;
            save.GetComponent<SaveScript>().ra = 0;
        }
        if (save.GetComponent<SaveScript>().Health != null && save.GetComponent<SaveScript>().Health != 0)
        {
            Health = save.GetComponent<SaveScript>().Health;
        }
        else
        {
            Health = 100;
        }
        if (save.GetComponent<SaveScript>().location == null || save.GetComponent<SaveScript>().location >= spawnPoints.Length)  
        {
            leader = 0;
        }
        else
        {
            leader = save.GetComponent<SaveScript>().location;
        }
        GameObject guis = GameObject.Find("guisher");

        /*if (save.GetComponent<SaveScript>().Inv != null)
        {
            Managers.Inventory.items = save.GetComponent<SaveScript>().Inv;
        }*/
        player = Instantiate(playerPrefab, spawnPoints[leader].position, spawnPoints[leader].rotation);
        camera = Instantiate(cameraPrefab, player.transform.position + offsetCam, player.transform.rotation);
        camera.GetComponent<NewOrbitCamera>().target = player.transform;
        cam = player.transform.GetChild(2).gameObject;
        status = ManagerStatus.Started;
    }

    void Update()
    {
            time += Time.deltaTime;
            if (time >= 1)
            {
                time = 0;
                if (Managers.Inventory.equippedItemName == "rag")
                {
                    RagScripts rag = GameObject.Find("rag(Clone)").GetComponent<RagScripts>();
                    if (rag.ready) rag.ChangeMokrost(-5);
                    else
                    {
                        ChangeHealth(-Mathf.FloorToInt(Managers.Smoke.Gustota * smokeDamage));
                    }
                }
                else
                {
                    ChangeHealth(-Mathf.FloorToInt(Managers.Smoke.Gustota * smokeDamage));
                }
            }
    }

    public void ChangeHealth(int value)
    {
        Health += value;
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
        else if (Health <= 0)
        {
            Health = 0;
            // Тут перезапуск
            
            Messenger<string, int>.Broadcast(GameEvent.DialogUpdated, "Вы проиграли!", 1);

        }
        Messenger.Broadcast(GameEvent.HealthUpdated);
    }

    public void SaveDate()
    {
        save.GetComponent<SaveScript>().Health = Health;
        save.GetComponent<SaveScript>().gustota = Managers.Smoke.Gustota;
        save.GetComponent<SaveScript>().Inv = Managers.Inventory.items;
        save.GetComponent<SaveScript>().equippedItem = Managers.Inventory.equippedItemName;
        save.GetComponent<SaveScript>().gu = GameObject.FindGameObjectWithTag("Canvas").GetComponent<InfoPanel>().Guisher;
        save.GetComponent<SaveScript>().ra = GameObject.FindGameObjectWithTag("Canvas").GetComponent<InfoPanel>().Rag;
        Debug.Log("WORK!");
    }
}
