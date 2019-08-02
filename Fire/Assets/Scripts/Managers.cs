using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(InventoryManager))]
[RequireComponent(typeof(FireManager))]
[RequireComponent(typeof(SmokeManager))]

public class Managers : MonoBehaviour
{
    public static PlayerManager Player { get; private set; }
    public static InventoryManager Inventory { get; private set; }
    public static FireManager Fire { get; private set; }
    public static SmokeManager Smoke { get; private set; }
    private List<IGameManager> list;

    void Start ()
    {
        Player = GetComponent<PlayerManager>();
        Inventory = GetComponent<InventoryManager>();
        Fire = GetComponent<FireManager>();
        Smoke = GetComponent<SmokeManager>();
        list = new List<IGameManager> {
            Player,
            Inventory,
            Fire,
            Smoke
        };
        StartCoroutine(StartManagers());
    }
	
    private IEnumerator StartManagers()
    {
        foreach (IGameManager manager in list)
        {
            manager.StartUp();
        }
        yield return null;
        int numAll = list.Count;
        int numCur = 0;
        while (numCur < numAll)
        {
            int lastNum = numCur;
            numCur = 0;
            foreach(IGameManager manager in list)
            {
                if (manager.status == ManagerStatus.Started)
                {
                    numCur++;
                }
            }
            if (numCur > lastNum)
                Debug.Log("Progress: " + numCur + "|" + numAll);
            yield return null;
        }
    }
}
