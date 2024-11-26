using UnityEngine;
using DG.Tweening;
using TMPro.EditorUtilities;
using System.Security.Cryptography;
using System;

public class RotateCamera : MonoBehaviour
{
    public float sensitivityX;
    public float sensitivityY;

    public Transform orientation;

    public float xRotation;
    public float yRotation;

    public GameObject Sliceboard;

    bool startedLooking;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Harpoon.hasCaught && !startedLooking)
        {
            startedLooking = true;
            transform.DOLookAt(Sliceboard.transform.position, 0.5f).OnComplete(UpdateRotation);
        }
        else if (!Harpoon.hasCaught)
        {
            GetMouseInput();
        }
    }

    private void UpdateRotation()
    {
        xRotation = transform.localRotation.eulerAngles.x;
        yRotation = transform.localRotation.eulerAngles.y;
    }

    void GetMouseInput()
    {
        if (startedLooking)
        {
            startedLooking = false;
        }

        if (!Harpoon.hasCaught)
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivityX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivityY;

            yRotation += mouseX;
            xRotation -= mouseY;

            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            yRotation %= 360;

            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }
}
