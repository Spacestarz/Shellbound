
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField]
    Transform cameraPosition;
   
    void Awake()
    {
        Application.targetFrameRate = 120;
    }
}
