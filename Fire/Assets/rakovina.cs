using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class rakovina : MonoBehaviour
{
    FireScript fire;
    [SerializeField] Slider slider;
    public int countAir = 100;
    private float Timer = 15f;
    private float Live = 0f;
    private int k;


    private void OnTriggerEnter(Collider other)
    {
                Debug.Log("fgfghgj");
                slider.value = Live;
                k = (int)fire.smokeKef;
                fire.smokeKef = 0;
                StartCoroutine("F");
    }

    IEnumerator F()
    {
        Live += Time.deltaTime;
        slider.value = Live;
        if (Live >= Timer)
        {
            fire.smokeKef = k;
        }
        yield return null;
    }
}
