using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
/*    public Transform target;
*/
    public Vector3 offset;

    private float currentZoom = 10f;

    public float pitch = 2f;

    public float zoomSpeed = 4f;

    public float minZoom = 5f;

    public float maxZoom = 15f;

    //If an aircraft or ship yaws, it moves slightly to the side of its intended direction:
    public float yawSpeed = 100f;

    private float currentYaw = 0f;

    private void LateUpdate()
    {
/*        transform.position = target.position - offset * currentZoom;

        transform.LookAt(target.position + Vector3.up * pitch);*/
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        // return value beetween min and max values
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        currentYaw -= Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;

        // Rotate by angle
/*        transform.RotateAround(target.position, Vector3.up, currentYaw);*/
    }
}
