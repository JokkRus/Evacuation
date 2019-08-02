using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public GameObject[] gam;
    public GameObject k;

    public void StartBut()
    {
        SceneManager.LoadScene("game");
    }

    public void AboutBut()
    {
        for (int i = 0; i < gam.Length; i++)
        { gam[i].SetActive(false); }
        k.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
