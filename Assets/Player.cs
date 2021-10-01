using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpSpeed = 10f;
    public float speed = 1f;
    public VariableJoystick variableJoystick;
    public GameObject arCamera;
    public Canvas canvas;
    private Rigidbody rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public void Jump()
    {
        if (rigidBody.velocity.y == 0)
        {
            rigidBody.AddForce(Vector3.up * jumpSpeed);
        }
    }

    public void FixedUpdate()
    {
        Vector3 forward = new Vector3(arCamera.transform.forward.x, 0, arCamera.transform.forward.z);
        Vector3 right = new Vector3(arCamera.transform.right.x, 0, arCamera.transform.right.z);
        Vector3 direction = forward * variableJoystick.Vertical + right * variableJoystick.Horizontal;
        rigidBody.rotation = Quaternion.FromToRotation(Vector3.forward, direction);
        rigidBody.velocity = direction * speed;
    }

    public void OnCollisionEnter(Collision collision)
    {
        GameMenu gameMenu = canvas.GetComponent<GameMenu>();
        Debug.Log(collision.gameObject.name);
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
}
