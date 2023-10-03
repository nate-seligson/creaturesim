using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    bool focused = false;
    float baseSize;
    float dragSpeed = 100f;
    float zoomSpeed = 10f;
    private Vector3 dragOrigin;
    private Camera camera;
    void Start()
    {
        camera = GetComponent<Camera>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (Input.GetMouseButton(0))
        {

            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
            Vector3 move = new Vector3(-pos.x * dragSpeed, -pos.y * dragSpeed, 0);
            transform.Translate(move * Time.deltaTime);
        }
        if (camera.orthographicSize >= 0)
        {
            camera.orthographicSize += -Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        }
        else
        {
            camera.orthographicSize = 0;
        }
    }
}
