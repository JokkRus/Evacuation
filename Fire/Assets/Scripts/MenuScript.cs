using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuScript : MonoBehaviour {
    [SerializeField] Image[] itemIcons;
    [SerializeField] Text[] itemText;
    [SerializeField] Text curItemText;
    [SerializeField] Button useButton;
    [SerializeField] Button equipButton;


    private string curItem;

    public void Refresh()
    {
        List<string> itemList = Managers.Inventory.GetItemList();
        for (int i = 0; i < itemIcons.Length; i++)
        {
            if (i < itemList.Count) {
                itemIcons[i].gameObject.SetActive(true);
                itemText[i].gameObject.SetActive(true);
                string item = itemList[i];
                Sprite sprite = Resources.Load<Sprite>("Icons/" + item);
                itemIcons[i].sprite = sprite;
                //itemIcons[i].SetNativeSize();
                int count = Managers.Inventory.GetItemCount(item);
                string text = "x" + count;
                // Добавить сообщение об экипе
                itemText[i].text = text;
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerClick;
                entry.callback.AddListener((BaseEventData data) =>
                {
                    ChooseItem(item);
                });
                EventTrigger trigger = itemIcons[i].GetComponent<EventTrigger>();
                trigger.triggers.Clear();
                trigger.triggers.Add(entry);
            }
            else
            {
                itemIcons[i].gameObject.SetActive(false);
                itemText[i].gameObject.SetActive(false);
            }
        }
        if (!itemList.Contains(curItem))
        {
            curItem = null;
        }
        if (curItem == null)
        {
            curItemText.gameObject.SetActive(false);
            useButton.gameObject.SetActive(false);
            equipButton.gameObject.SetActive(false);
        }
        else
        {
            curItemText.gameObject.SetActive(true);
            useButton.gameObject.SetActive(true);
            equipButton.gameObject.SetActive(true);
            curItemText.text = "Choosed: " + curItem;
        }
    }
	public void ChooseItem(string item)
    {
        curItem = item;
        Refresh();
    }
    public void OnEquip()
    {
        if (curItem == "guisher" && Managers.Inventory.equippedItemName != curItem)
        {
            Managers.Inventory.EquipGuisher(curItem);
        }
        else if (curItem == "rag" && Managers.Inventory.equippedItemName != curItem)
        {
            Managers.Inventory.EquipRag(curItem);
        }
        Refresh();
    }
    public void OnUse()
    {
        
    }
}
