using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    public Transform target;
    public float rotSpeed = 1.5f;
    private float _rotY;
    private Vector3 _offset;

    void Start ()
    {
        _rotY = transform.eulerAngles.y;
        _offset = target.position - transform.position;
	}
	
	void LateUpdate ()
    {
        float Hor = Input.GetAxis("Horizontal");
        if (Hor != 0)
        {
            _rotY += Hor * rotSpeed;

        }
        else
        {
            _rotY += Input.GetAxis("Mouse X") * rotSpeed * 3;
        }

        Quaternion rotation = Quaternion.Euler(0, _rotY, 0);
        transform.position = target.position - (rotation * _offset);
        transform.LookAt(target);
	}
}
