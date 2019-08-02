using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public string SceneName;
    public int location;

    private void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Managers.Player.save.GetComponent<SaveScript>().location = location;
            Managers.Player.SaveDate();
            Managers.Player.save.GetComponent<SaveScript>().runGame = true;
            Application.LoadLevel(SceneName);
        }
    }
}
