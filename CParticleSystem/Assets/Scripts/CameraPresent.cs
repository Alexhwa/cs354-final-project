using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraPresent : MonoBehaviour
{
    public KeyCode toggleKey;
    public KeyCode speedUpKey;
    public KeyCode speedDownKey;

    private bool toggled;
    public CinemachineVirtualCamera virtCam;
    private CinemachineTrackedDolly pathBody;

    public float panSpeed;
    private float stepSize = .005f;

    // Start is called before the first frame update
    void Start()
    {
        pathBody = virtCam.GetCinemachineComponent<CinemachineTrackedDolly>();
        pathBody.m_PathPosition = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            toggled = !toggled;
        }
        if (Input.GetKey(speedUpKey))
        {
            panSpeed += stepSize;
        }
        if (Input.GetKey(speedDownKey))
        {
            panSpeed -= stepSize;
        }
        if (toggled) {
            pathBody.m_PathPosition += panSpeed;
        }
    }
}
