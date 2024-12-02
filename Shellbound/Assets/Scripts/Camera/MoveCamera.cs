
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField]
    Transform cameraPosition;
   
    // Start is called before the first frame update
    void Awake()
    {
        Application.targetFrameRate = 120;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = cameraPosition.position;
    }
}
