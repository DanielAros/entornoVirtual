using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    // public float sensibilidad = 100f;
    // public float suavizado = 2f;

    // private Vector2 deltaSmooth;
    // private Vector2 deltaCurrent;

    public float sensitivity = 400f;
    public Transform playerBody;
    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // deltaCurrent = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        // deltaSmooth = Vector2.Lerp(deltaSmooth, deltaCurrent, 1f / suavizado);

        // transform.Rotate(Vector3.up * deltaSmooth.x * Time.deltaTime * sensibilidad);
        // transform.Rotate(Vector3.left * deltaSmooth.y * Time.deltaTime * sensibilidad);

        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
