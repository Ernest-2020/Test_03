using System;
using UnityEngine;

public class CapsulePlayerCamera : BaseCamera
{
    [SerializeField] private RotationSettings rotationSettings;
    [SerializeField] private Transform transformCameraContainer;

    private float _xRotation;
    private float _yRotation;
    private void Awake()
    {
        transformCameraContainer = transform;
    }

    public override void CameraRotation(float rotationX, float rotationY)
    {
        _xRotation += rotationX * rotationSettings.Sensitivity;
        _yRotation -= rotationY * rotationSettings.Sensitivity;
        _yRotation = Mathf.Clamp(_yRotation, rotationSettings.MinRotationY, rotationSettings.MaxRotationY);

        transformCameraContainer.rotation = Quaternion.Lerp
        (
            transformCameraContainer.rotation,
            Quaternion.Euler(_yRotation, _xRotation, 0.0f), rotationSettings.SmoothTime
        );
    }

    public override void Following(Transform player, Transform cameraTransform, Vector3 offset)
    {

        cameraTransform.position = player.position - offset;
    }
}
