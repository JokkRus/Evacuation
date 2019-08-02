using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] Text hpText;
    [SerializeField] Slider hpSlider;
    [SerializeField] Text airText;
    [SerializeField] Slider airSlider;
    [SerializeField] GameObject menu;

    void Awake()
    {
        Messenger.AddListener(GameEvent.HealthUpdated, OnHealthUpdated);
        Messenger<int>.AddListener(GameEvent.SupplyUpdated, OnSupplyUpdated);
        Messenger<int>.AddListener(GameEvent.MokrostUpdated, OnMokrostUpdated);
    }

    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.HealthUpdated, OnHealthUpdated);
        Messenger<int>.RemoveListener(GameEvent.SupplyUpdated, OnSupplyUpdated);
        Messenger<int>.RemoveListener(GameEvent.MokrostUpdated, OnMokrostUpdated);
    }

    // Use this for initialization
    void Start()
    {
        OnHealthUpdated();
        menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            menu.SetActive(!menu.activeSelf);
            menu.GetComponent<MenuScript>().Refresh();
        }
    }
    public void OnHealthUpdated()
    {
        hpText.text = "HP: " + Managers.Player.Health;
        hpSlider.value = Managers.Player.Health;
    }

    public void OnSupplyUpdated(int supply)
    {
        airText.text = "AIR: " + supply / 5;
        airSlider.value = supply / 5;
    }
    public void OnMokrostUpdated(int value)
    {
        airText.text = "Mok: " + value;
        airSlider.value = value;
    }
}
