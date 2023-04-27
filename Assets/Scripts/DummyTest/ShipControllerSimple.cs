using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControllerSimple : MonoBehaviour
{
    [SerializeField] private float mouseXRotSpeed;
    [SerializeField] private float mouseYRotSpeed;

    [SerializeField] private float shipSpeed;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.back * mouseXRotSpeed * Input.GetAxis("Mouse X") * Time.deltaTime, Space.Self);
        transform.Rotate(Vector3.left * mouseYRotSpeed * Input.GetAxis("Mouse Y") * Time.deltaTime, Space.Self);
        if (Input.GetKey("space"))
            transform.Translate(Vector3.forward * shipSpeed * Time.deltaTime);
    }
}
