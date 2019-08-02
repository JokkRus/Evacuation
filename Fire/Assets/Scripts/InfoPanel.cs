using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class InfoPanel : MonoBehaviour
{
    [SerializeField] Text dialog;
    [SerializeField] GameObject panel;
    public int Guisher = 0;
    public int Rag = 0;
    private int action;

    void Awake()
    {
        Messenger<string, int, string>.AddListener(GameEvent.DialogUpdated, Do);
        Rag = Managers.Player.save.GetComponent<SaveScript>().ra;
        Guisher = Managers.Player.save.GetComponent<SaveScript>().gu;
    }

    void Do(string t, int i, string itemName)
    {
        action = i;
        dialog.text = t;
        panel.SetActive(true);
        Time.timeScale = 0;
        if (itemName == "guisher" && Guisher > 0 || itemName == "rag" && Rag > 0)
        {
            panel.SetActive(false);
            Time.timeScale = 1;
        }
        if (itemName == "rag")
        {
            Rag++;
        }
        else if (itemName == "guisher")
        {
            Guisher++;
        }
    }

    public void ButtonAct()
    {
        if (action == 0)
        {
            panel.SetActive(false);
        }
        else if (action == 1)
        {
            SceneManager.LoadScene(0);
        }
        Time.timeScale = 1;
    }

    void Update()
    {
        
    }
}
