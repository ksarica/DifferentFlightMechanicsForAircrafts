using UnityEngine;

public enum CameraMode
{
    SmoothFollow,
    FirstPerson
}

public class CameraController : MonoBehaviour
{
    private ClassicCamera fpsCamera;
    private SmoothCameraFollow smoothCamera;
    private CameraMode currentCameraMode;

    private void Awake()
    {
        fpsCamera = GetComponent<ClassicCamera>();
        smoothCamera = GetComponent<SmoothCameraFollow>();
        currentCameraMode = CameraMode.SmoothFollow;
        fpsCamera.enabled = false;
        smoothCamera.enabled = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            SwitchCamera();
        }
        else if (Input.GetKey(KeyCode.C))
        {
            if (currentCameraMode == CameraMode.SmoothFollow)
            {
                SwitchCamera();
            }
        }
    }

    public void SwitchCamera()
    {
        if (currentCameraMode == CameraMode.SmoothFollow)
        {
            currentCameraMode = CameraMode.FirstPerson;
            fpsCamera.enabled = true;
            smoothCamera.enabled = false;
        }
        else
        {
            currentCameraMode = CameraMode.SmoothFollow;
            fpsCamera.enabled = false;
            smoothCamera.enabled = true;
        }
    }
}
