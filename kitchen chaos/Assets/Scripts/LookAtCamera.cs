using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private enum CameraMode {
        lookAtNormal,
        lookAtInverted,
        cameraForward,
        cameraForwardInverted }
    [SerializeField] private CameraMode mode;

    private void LateUpdate()
    {
        switch (mode)
        {
            case CameraMode.lookAtNormal:
                transform.LookAt(Camera.main.transform);
                break;
            case CameraMode.lookAtInverted:
                Vector3 invertDir = transform.position - Camera.main.transform.position;
                transform.LookAt(invertDir);
                break;
            case CameraMode.cameraForward:
                transform.forward = Camera.main.transform.forward;
                break;
            case CameraMode.cameraForwardInverted:
                transform.forward = -Camera.main.transform.forward;
                break;
        }
    }
}
