using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController controller;
    private float MoveSpeed = 5f;
    //
    public float jumpSpeed = 18f;
    public float gravity = -10f;
    public float limitFall = -10f;
    public float minFall = -1.5f;
    private ControllerColliderHit contact;
    public float pushForce = 3f;
    //
    private float vertSpeed;

    void Start ()
    {
        controller = gameObject.GetComponent<CharacterController>();
        vertSpeed = minFall;
    }
	

	void Update ()
    {
        float deltaX = Input.GetAxis("Horizontal") * MoveSpeed;
        float deltaZ = Input.GetAxis("Vertical") * MoveSpeed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, MoveSpeed);
        movement = transform.TransformDirection(movement);
        bool hitGround = false;
        RaycastHit hit;
        if (vertSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            float dist = (controller.height + controller.radius) / 1.9f;
            hitGround = hit.distance <= dist;
        }
        if (hitGround)
        {
            if (Input.GetButtonDown("Jump"))
            {
                vertSpeed = jumpSpeed;
            }
            else
            {
                vertSpeed = minFall;
            }
        }
        else
        {
            vertSpeed += gravity;
            if (vertSpeed < limitFall)
            {
                vertSpeed = limitFall;
            }
            if (controller.isGrounded)
            {
                if (Vector3.Dot(movement, contact.normal) < 0)
                {
                    movement = contact.normal * MoveSpeed;
                }
                else
                {
                    movement += contact.normal * MoveSpeed;
                }
            }
        }
        movement.y = vertSpeed;
        movement *= Time.deltaTime;
        controller.Move(movement);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        contact = hit;
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body != null && body.isKinematic == false)
        {
            body.velocity = hit.moveDirection * pushForce;
        }
    }
}
