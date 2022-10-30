using UnityEngine;

public abstract class BaseCamera : MonoBehaviour
{
    public abstract void CameraRotation(float rotationX,float rotationY);

    public abstract void Following(Transform player,Transform cameraTransform, Vector3 offset);

}
