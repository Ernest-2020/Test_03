using UnityEngine;

public class CameraController:IExecute
{
    private readonly Transform _player;
    private readonly Transform _transformCamera;
    private readonly BaseCamera _baseCamera;
    private readonly Vector3 _offset;

    public CameraController(BaseCamera baseCamera,Transform player, Transform transformCamera)
    {
        _baseCamera = baseCamera;
        _player = player;
        _transformCamera = transformCamera;
        transformCamera.LookAt(_player.transform);
        _offset = baseCamera.transform.position - player.position;
    }

    public void Execute()
    {
        //_baseCamera.Following(_player,_transformCamera,_offset);
    }
}
