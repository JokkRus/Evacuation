using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeScript : MonoBehaviour {
    private float gustota = 0f;
    private float time = 0f;
    // Use this for initialization
    void Start () {
        Messenger.AddListener(GameEvent.FireUpdated, Gustota);
    }
	
	// Update is called once per frame
	void Update ()
    {
        time += Time.deltaTime;
        if (time >= 1)
        {
            time = 0;
            Managers.Player.ChangeHealth(-Mathf.FloorToInt(gustota * 15));
        }
	}

    public void Gustota()
    {
        gustota += 0.01f;
        gameObject.GetComponent<ParticleSystem>().startColor = new Color(0, 0, 0, gustota);
    }
}
