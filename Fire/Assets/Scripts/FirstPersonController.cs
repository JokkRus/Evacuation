using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{

    public enum Rot
    {
        MouseX = 0,
        MouseY = 1, 
        MouseXandY = 2
    }

    public Rot axes = Rot.MouseXandY;
    public float SensX = 50f;
    public float SensY = 50f;

    public float minY = -45f;
    public float maxY = 45f;
    private float rotX = 0f;
    RaycastHit hit;

    void Start ()
    {
		
	}
	

	void Update ()
    {
		if (axes == Rot.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * SensX * Time.deltaTime, 0);
        }
        else if (axes == Rot.MouseY)
        {
            rotX -= Input.GetAxis("Mouse Y") * SensY * Time.deltaTime;
            rotX = Mathf.Clamp(rotX, minY, maxY);
            float rotY = transform.localEulerAngles.y;
            transform.localEulerAngles = new Vector3(rotX, rotY, 0);
        }
        else
        {

        }

        /*if (Physics.Raycast(transform.position, transform.forward, out hit, 2))
        {
            if (hit.transform.tag == "DOOR")
            {
                if (Input.GetKeyDown(KeyCode.H))
                {
                    hit.transform.GetComponent<Door>().DoorOpen();
                }
            }
        }*/
	}
}
