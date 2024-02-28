using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector2 rotation = Vector2.zero;
    public float speed = 3;
    public float minYAngle = -30f;
    public float maxYAngle = 30f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      rotation.y += Input.GetAxis("Mouse X");
      rotation.x += -Input.GetAxis("Mouse Y");

      rotation.x = Mathf.Clamp(rotation.x, minYAngle, maxYAngle);

      transform.eulerAngles = (Vector2)rotation * speed;
    }
}