using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{

    void Awake()
    {
        Managers.Player.save.GetComponent<SaveScript>().Health = 100;
        Managers.Player.save.GetComponent<SaveScript>().gustota = 0;
        Managers.Player.save.GetComponent<SaveScript>().location = 0;
    }


    void Update()
    {
        
    }
}


