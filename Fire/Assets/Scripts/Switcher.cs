using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switcher : MonoBehaviour
{
    public GameObject cam;
    public GameObject camSpawn;
    public GameObject camThirdSpawn;
    public GameObject ff;

    void Start ()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        camSpawn = GameObject.Find("FirstPCam");
        ff = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube));
        ff.transform.position = Vector3.zero;
    }
	

	void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Y))
        {
            if (ff.activeSelf)
            {
                ff.SetActive(false);
                OnFirst();
            }
            else
            {
                ff.SetActive(true);
                OnThird();
            }
        }
	}

    void OnFirst()
    {
        cam.transform.SetParent(transform);
        cam.transform.position = camSpawn.transform.position;
        cam.transform.rotation = transform.rotation;
        cam.GetComponent<FirstPersonController>().enabled = true;
        cam.GetComponent<NewOrbitCamera>().enabled = false;
        gameObject.GetComponent<NewRelativeMovement>().enabled = false;
        gameObject.GetComponent<Movement>().enabled = true;
        gameObject.GetComponent<FirstPersonController>().enabled = true;
    }

    void OnThird()
    {
        cam.transform.parent = null;
        cam.transform.position = camThirdSpawn.transform.position;
        cam.GetComponent<FirstPersonController>().enabled = false;
        cam.GetComponent<NewOrbitCamera>().enabled = true;
        gameObject.GetComponent<NewRelativeMovement>().enabled = true;
        gameObject.GetComponent<Movement>().enabled = false;
        gameObject.GetComponent<FirstPersonController>().enabled = false;
    }
}
