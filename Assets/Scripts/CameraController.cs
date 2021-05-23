using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineCamera;
    private float zoomAmount;
    private float targetZoomAmount;
    public float movementSpeed = 30f;
    public float zoomSpeed = 2f;
    private void Start()
    {
        zoomAmount = cinemachineCamera.m_Lens.OrthographicSize;
        targetZoomAmount = zoomAmount;
    }
    void Update()
    {
        Move();
        Zoom();
    }
    //This function allows us the move the camera horizontally and vertically using the arrow keys.
    //Player can also move the camera by moving the mouse near the screen edge.
    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        float edgeScrollingSize = 30f;
        if (Input.mousePosition.x > Screen.width - edgeScrollingSize)
            x = 1f;
        if (Input.mousePosition.x < edgeScrollingSize)
            x = -1f;
        if (Input.mousePosition.y > Screen.height - edgeScrollingSize)
            y = 1f;
        if (Input.mousePosition.y < edgeScrollingSize)
            y = -1f;
        Vector3 moveDirection = new Vector3(x, y).normalized;
        transform.position += moveDirection * movementSpeed * Time.deltaTime;
    }
    //This function allows us to zoom the camera in and out using the mousewheel.
    // I made it so when we zoom in and out zooming is happening smoothly rather than instant.
    //It also looks the zoom values so we cant zoom in or out too much.
    void Zoom()
    {
        //Zoom calculation.
        targetZoomAmount -= Input.mouseScrollDelta.y * zoomSpeed;

        //Clamping the zoom values.
        float minZoom = 5f;
        float maxZoom = 20f;
        targetZoomAmount = Mathf.Clamp(targetZoomAmount, minZoom, maxZoom);

        //Smoothing the zoom.
        zoomAmount = Mathf.Lerp(zoomAmount, targetZoomAmount, Time.deltaTime * 5f);
        cinemachineCamera.m_Lens.OrthographicSize = zoomAmount;
    }
}
