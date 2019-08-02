using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }
    public string equippedItemName { get; set; }
    // private List<string> items;
    public Dictionary<string, int> items;
    private GameObject equippedItem;   
    [SerializeField] GameObject guisher;
    [SerializeField] GameObject rag;
    private int guisherPowd;
    private int ragPowd;


    public void StartUp()
    {
        Debug.Log("Inventory starting...");
        items = new Dictionary<string, int>();
        guisherPowd = 500;
        ragPowd = 0;
        status = ManagerStatus.Started;
        if (Managers.Player.save.GetComponent<SaveScript>().Inv != null)
        {
            items = Managers.Player.save.GetComponent<SaveScript>().Inv;
        }

        if (Managers.Player.save.GetComponent<SaveScript>().equippedItem != null && Managers.Player.save.GetComponent<SaveScript>().equippedItem != "")
        {
            equippedItemName = Managers.Player.save.GetComponent<SaveScript>().equippedItem;
            if (equippedItemName == "rag")
            {
                SpawnObj(equippedItemName, rag, new Vector3(0, 0.655f, 0.160f), new Vector3(8, 0, 90));
                equippedItem.GetComponent<RagScripts>().Mokrost = ragPowd;
            }
            else if (equippedItemName == "guisher")
            {
                SpawnObj(equippedItemName, guisher, new Vector3(0, 0.02f, 0.3f), new Vector3(-90, 0, 90));
                equippedItem.GetComponent<guisher>().supply = guisherPowd;
            }
        }
    }
    private void Display()
    {
        string display = "Items:";
        foreach (KeyValuePair<string, int> item in items)
        {
            display += " " + item.Key + " - " + item.Value + ",";
        }
        Debug.Log(display);
    }
    public void AddItem(string name)
    {
        if (items.ContainsKey(name))
        {
            items[name] += 1;
        }
        else
        {
            items[name] = 1;
        }
        Display();
    }
    public List<string> GetItemList()
    {
        List<string> list = new List<string>(items.Keys);
        return list;
    }
    public int GetItemCount(string name)
    {
        if (items.ContainsKey(name))
        {
            return items[name];
        }
        return 0;
    }
    public void EquipGuisher(string item)
    {
        if (equippedItemName == "rag")
        {
            AddItem(equippedItemName);
            ragPowd = equippedItem.GetComponent<RagScripts>().Mokrost;
            Destroy(equippedItem);
        }
        SpawnObj(item, guisher, new Vector3(0, 0.02f, 0.3f), new Vector3(-90, 0, 90));
        equippedItem.GetComponent<guisher>().supply = guisherPowd;
        if (items[item] > 1)
        {
            items[item]--;
        }
        else
        {
            items.Remove(item);
        }
    }
    public void EquipRag(string item)
    {
        if (equippedItemName == "guisher")
        {
            AddItem(equippedItemName);
            guisherPowd = equippedItem.GetComponent<guisher>().supply;
            Destroy(equippedItem);
        }
        SpawnObj(item, rag, new Vector3(0, 0.655f, 0.160f), new Vector3(8, 0, 90));
        equippedItem.GetComponent<RagScripts>().Mokrost = ragPowd;
        if (ragPowd > 0)
        {
            equippedItem.GetComponent<RagScripts>().ready = true;
        }
        if (items[item] > 1)
        {
            items[item]--;
        }
        else
        {
            items.Remove(item);
        }
    }

    void SpawnObj(string ItemName, GameObject pref, Vector3 pos, Vector3 rot)
    {
        equippedItemName = ItemName;
        equippedItem = Instantiate(pref, Vector3.zero, Quaternion.identity);
        GameObject obj = GameObject.FindGameObjectWithTag("Player");
        equippedItem.transform.SetParent(obj.transform);
        equippedItem.transform.localPosition = pos;
        equippedItem.transform.localEulerAngles = rot;
    }
}

