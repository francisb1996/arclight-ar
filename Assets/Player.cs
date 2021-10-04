using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpSpeed;
    public float speed = 1f;
    public VariableJoystick variableJoystick;
    public GameObject arCamera;
    public Canvas canvas;
    private Rigidbody rigidBody;
    private float groundedDistance = 0.02f;
    private bool isGrounded;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        variableJoystick = GameObject.FindObjectOfType<VariableJoystick>();
        canvas = GameObject.FindObjectOfType<Canvas>();
        arCamera = GameObject.Find("AR Camera");
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rigidBody.AddForce(Vector3.up * jumpSpeed);
        }
    }

    public void FixedUpdate()
    {
        Vector3 forward = new Vector3(arCamera.transform.forward.x, 0, arCamera.transform.forward.z);
        Vector3 right = new Vector3(arCamera.transform.right.x, 0, arCamera.transform.right.z);
        Vector3 direction = forward.normalized * variableJoystick.Vertical + right.normalized * variableJoystick.Horizontal;
        if (variableJoystick.Vertical != 0f && variableJoystick.Horizontal != 0f)
        {
            rigidBody.rotation = Quaternion.FromToRotation(Vector3.forward, direction);
        }
        rigidBody.velocity = new Vector3(direction.x * speed, rigidBody.velocity.y, direction.z * speed);
        isGrounded = IsGrounded();
    }

    public void OnCollisionEnter(Collision collision)
    {
        GameMenu gameMenu = canvas.GetComponent<GameMenu>();
        switch (collision.gameObject.name)
        {
            case "KillBox":
                gameMenu.Lose();
                break;
            case "WinBall":
                gameMenu.Win();
                break;
        }
    }

    private bool IsGrounded() {
        RaycastHit hit;
        Physics.Raycast(rigidBody.position, Vector3.down, out hit, float.MaxValue, 1);
        if (hit.distance <= groundedDistance)
        {
            return true;
        }
        return false;
    }
}
