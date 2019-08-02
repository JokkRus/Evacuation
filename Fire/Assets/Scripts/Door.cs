﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int smooth = 2;
    public float OpenAngle = 90f;

    private Vector3 defRot;
    private Vector3 OpenRot;

    public bool open;
    public bool enter;

    void Start()
    {
        defRot = transform.eulerAngles;
        OpenRot = new Vector3(defRot.x, defRot.y + OpenAngle, defRot.z);
    }


    void Update()
    {
        if (open)
        {
            transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, OpenRot, Time.deltaTime * smooth);
        }
        else
        {
            transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, defRot, Time.deltaTime * smooth);
        }

        if (Input.GetKeyDown(KeyCode.H) && enter == true)
        {
            open = !open;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            enter = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            enter = false;
        }
    }
}
