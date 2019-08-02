using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NewRelativeMovement : MonoBehaviour
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
    private float curSpeed = 0f;
    public float stopSpeed = 15f;
    public float buffer = 2f;
    private Vector3 targetPos = Vector3.one;
    RaycastHit mouseHit;

    void Update ()
    {
        Vector3 movement = Vector3.zero;
      
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out mouseHit))
            {
                targetPos = mouseHit.point;
                curSpeed = moveSpeed;
            }
        }
        if (targetPos != Vector3.one && mouseHit.point != null && mouseHit.transform.gameObject.tag != "Player")
        {
            Vector3 normPos = new Vector3(targetPos.x, transform.position.y, targetPos.z);
            Quaternion targetRot = Quaternion.LookRotation(normPos - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Rotspeed * Time.deltaTime);
            movement = Vector3.forward * curSpeed;
            movement = transform.TransformDirection(movement);
            if (Vector3.Distance(normPos, transform.position) < buffer)
            {
                curSpeed -= stopSpeed * Time.deltaTime;
                if (curSpeed <= 0)
                {
                    targetPos = Vector3.one;
                    curSpeed = 0;
                }
            }
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
