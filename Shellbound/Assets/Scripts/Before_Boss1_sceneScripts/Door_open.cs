using UnityEngine;
using DG.Tweening;

public class Door_open : MonoBehaviour
{
    public Vector3 startpos;
    private Vector3 endpos;
    private float speed = 10f;
    private Box_doorhandle gloveScript;
    private float duration = 1;

    public float openAmount = 3;

    //private Vector3 MOVE;
    private GameObject glove;

    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position;
        endpos = transform.position + transform.up * openAmount;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenDoor()
    {
        transform.DOMove(endpos, duration);
    }

    public void CloseDoor()
    {
        transform.DOKill();
        transform.DOMove(startpos, duration);
    }
}
