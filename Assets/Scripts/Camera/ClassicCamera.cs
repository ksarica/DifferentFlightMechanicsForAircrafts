using UnityEngine;
public class ClassicCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 fpsOffsetPosition;
    [SerializeField] private Vector3 backViewOffsetPosition;

    private void Update()
    {
        UpdateCameraPosition();
    }

    public void UpdateCameraPosition()
    {
        if (Input.GetKey(KeyCode.C)) // Look backwards
        {
            transform.position = target.TransformPoint(backViewOffsetPosition);
            transform.LookAt(target.transform.position + target.transform.forward);
        }
        else // Use Front Cameras
        {
            transform.position = target.TransformPoint(fpsOffsetPosition);
            transform.rotation = target.rotation;
        }
    }

}