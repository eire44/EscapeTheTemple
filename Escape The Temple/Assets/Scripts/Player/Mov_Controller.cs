using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mov_Controller : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float gravity = -9.81f;

    float mouseSensitivity = 100f;
    public Transform cameraTransform;

    private float xRotation = 0f;
    private Vector3 velocity;
    private CharacterController controller;


    public float crouchHeight = 1.0f;
    public float standHeight = 1.8f;
    public float crouchSpeed = 2.5f;
    public float standSpeed = 5f;

    public float crouchCameraY = 0.9f;
    public float standCameraY = 1.6f;
    public float crouchTransitionSpeed = 8f;

    Vector3 startPosition;
    public float fallLimit = -10f;

    void Start()
    {
        startPosition = transform.position;
        controller = GetComponent<CharacterController>();
        mouseSensitivity = PlayerPrefs.GetFloat("Sensitivity", 100f);
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            moveSpeed = 12f;
        } else
        {
            moveSpeed = 8f;
        }

        if (transform.position.y < fallLimit)
        {
            Respawn();
        }

        Move();
        Look();
        HandleCrouch();
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * moveSpeed * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void HandleCrouch()
    {
        bool isCrouching = Input.GetKey(KeyCode.LeftControl);

        float targetHeight = isCrouching ? crouchHeight : standHeight;
        float targetCamY = isCrouching ? crouchCameraY : standCameraY;
        float targetSpeed = isCrouching ? crouchSpeed : standSpeed;

        controller.height = Mathf.Lerp(controller.height, targetHeight, Time.deltaTime * crouchTransitionSpeed);
        controller.center = new Vector3(0, controller.height / 2f, 0);

        moveSpeed = targetSpeed;

        Vector3 camPos = cameraTransform.localPosition;
        camPos.y = Mathf.Lerp(camPos.y, targetCamY, Time.deltaTime * crouchTransitionSpeed);
        cameraTransform.localPosition = camPos;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }

    void Respawn()
    {
        CharacterController cc = GetComponent<CharacterController>();

        if (cc != null)
            cc.enabled = false;

        transform.position = startPosition;

        if (cc != null)
            cc.enabled = true;
    }
}
