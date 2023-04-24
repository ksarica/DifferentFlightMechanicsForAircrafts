using System.Collections;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float cameraForwardFromTarget;
    [SerializeField] private float cameraUpFromTarget;
    [SerializeField] private float cameraForwardWithSpeed;
    [SerializeField] private float smoothRate;

    private void FixedUpdate() // should be in late update, however it only works well in FixedUpdate
    {
        FollowShipWithCamera();
    }

    // NOTE THAT Vector3.up ==> GLOBAL  transform.up ==> RELATIVE TO GAMEOBJECT
    public void FollowShipWithCamera()
    {
        Vector3 cameraDestination = target.transform.position
        - (target.transform.forward * cameraForwardFromTarget)
        + (Vector3.up * cameraUpFromTarget);
        transform.position = (transform.position * smoothRate) + (cameraDestination * (1.0f - smoothRate));
        transform.LookAt(target.transform.position + target.transform.forward * cameraForwardWithSpeed);
    }

}
