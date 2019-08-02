using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelativeMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    public float Rotspeed = 15f;
    public float moveSpeed = 6.0f;
    private CharacterController _charController;
    public float jumpSpeed = 18f;
    public float gravity = -10f;
    public float limitFall = -10f;
    public float minFall = -1.5f;
    private float vertSpeed;
    private ControllerColliderHit contact;
    private Animator anim;
    public float pushForce = 3f;

    void Update ()
    {
        Vector3 movement = Vector3.zero;
        float Hor = Input.GetAxis("Horizontal");
        float Vert = Input.GetAxis("Vertical");
        
        if (Hor != 0 || Vert != 0)
        {
            movement.x = Hor * moveSpeed;
            movement.z = Vert * moveSpeed;
            movement = Vector3.ClampMagnitude(movement, moveSpeed);
            Quaternion camRot = target.rotation;
            target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);
            movement = target.TransformDirection(movement);
            Quaternion direction = Quaternion.LookRotation(movement); transform.rotation = Quaternion.Lerp(transform.rotation, direction, Rotspeed * Time.deltaTime);
            target.rotation = camRot;
        }
        anim.SetFloat("speed", movement.sqrMagnitude);
        bool hitGround = false;
        RaycastHit hit;
        if (vertSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            float dist = (_charController.height + _charController.radius) / 1.9f;
            hitGround = hit.distance <= dist;
        }
        if (hitGround)
        {
            if (Input.GetButtonDown("Jump"))
            {
                vertSpeed = jumpSpeed;
                anim.SetBool("jump", true);
            }
            else
            {
                vertSpeed = minFall;
                anim.SetBool("jump", false);
            }
        }
        else
        {
            vertSpeed += gravity;
            if (vertSpeed < limitFall)
            {
                vertSpeed = limitFall;
            }
            if (_charController.isGrounded)
            {
                if (Vector3.Dot(movement, contact.normal) < 0)
                {
                    movement = contact.normal * moveSpeed;
                }
                else
                {
                    movement += contact.normal * moveSpeed;
                }
            }
        }
        movement.y = vertSpeed;
        movement *= Time.deltaTime;
        _charController.Move(movement);
    }

    void Start() {
        vertSpeed = minFall;
        _charController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
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
