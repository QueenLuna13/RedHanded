using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector2 rotation = Vector2.zero;
    public float speed = 3;
    public float minYAngle = -30f;
    public float maxYAngle = 30f;
    public bool useMouseInput = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (useMouseInput)
        {
            rotation.y += Input.GetAxis("Mouse X");
            rotation.x += -Input.GetAxis("Mouse Y");

            rotation.x = Mathf.Clamp(rotation.x, minYAngle, maxYAngle);

            transform.eulerAngles = (Vector2)rotation * speed;
        }
        else
        {
            rotation.y += Input.GetAxis("RightStickX");
            rotation.x += -Input.GetAxis("RightStickY");

            rotation.x = Mathf.Clamp(rotation.x, minYAngle, maxYAngle);
        }

        transform.eulerAngles = (Vector2)rotation * speed;
    }
}
