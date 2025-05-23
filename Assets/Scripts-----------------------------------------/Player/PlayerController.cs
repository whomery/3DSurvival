using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerController : MonoBehaviour

{
    [Header("MoverMent")]
    public float moveSpeed;
    public float jumpPower;
    private Vector2 curMovementInput;
    public LayerMask groundLaterMask;
    public LayerMask JumpBoard;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;
    private Vector2 mouseDelta;
    public bool canLook = true;

    public Action inventory;
    private Rigidbody _rigidbody;
    // Start is called before the first frame update


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
            Move();
    }
    private void LateUpdate()
    {
        if (canLook)
        {
            CameraLook();
        }
    }

    void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y;

        _rigidbody.velocity = dir;
    }

    void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot,minXLook,maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);

    }




    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
      mouseDelta = context.ReadValue<Vector2>();
    }
    int AddJumpPower;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("JumpBoard"))
        {
            AddJumpPower = 100;
           _rigidbody.AddForce(Vector2.up * (jumpPower + AddJumpPower), ForceMode.Impulse);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("JumpBoard"))
        {
            AddJumpPower = 0;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
 

            if (context.phase == InputActionPhase.Started && IsGrounded())
        {

         
            _rigidbody.AddForce(Vector2.up * (jumpPower), ForceMode.Impulse);
            
        }
    }
    bool IsGrounded()
    {
        Ray[] rays = new Ray[4] 
        {

           new Ray(transform.position + (transform.forward * 0.5f) + (transform.up * 0.01f), Vector3.down),
           new Ray(transform.position + (-transform.forward * 0.5f) + (transform.up * 0.01f), Vector3.down),
           new Ray(transform.position + (transform.right * 0.5f) + (transform.up * 0.01f), Vector3.down),
           new Ray(transform.position + (-transform.right * 0.5f) + (transform.up * 0.01f), Vector3.down)
        };
        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i],0.1f,groundLaterMask))
            {
                return true;
            }
        }
        return false;
    }
    public void Onlnventory(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            inventory?.Invoke();
            ToggleCursor();
        }
    }
    void ToggleCursor()
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;

    }




}
