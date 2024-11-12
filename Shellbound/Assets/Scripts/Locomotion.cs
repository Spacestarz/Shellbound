using UnityEngine;

public class Locomotion : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]
    int speed;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(Vector3 dir)
    {
        rb.velocity = dir;
    }
}
