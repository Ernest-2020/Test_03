using Mirror;
using UnityEngine;

public sealed class PlayerManager : NetworkBehaviour
{
    [SerializeField] private BasePlayer basePlayer;
    [SerializeField] private BaseCamera cameraPrefab;
    
    private BaseCamera _baseCamera;
    private ListExecuteObject _listExecuteObject;
    private InputController _inputController;
    private CameraController _cameraController;
    private CapsulePlayerController _capsulePlayerController;

    private void Awake()
    {
        _listExecuteObject = new ListExecuteObject();
    }

    public override void OnStartLocalPlayer()
    {
        InitPlayer();
    }

    private void CreatePlayerCamera()
    {
        if (_baseCamera) return;
        var baseCamera = Instantiate(cameraPrefab,basePlayer.transform.position,Quaternion.identity,transform);
        _baseCamera = baseCamera;
    }

    private void InitPlayer()
    {
        if (!isClient&&!isLocalPlayer) return;
        
        CreatePlayerCamera();
        _inputController = new InputController(basePlayer,_baseCamera);
        _cameraController = new CameraController(_baseCamera,basePlayer.transform,_baseCamera.transform);
        _capsulePlayerController = new CapsulePlayerController(basePlayer);
        _listExecuteObject.AddExecuteObject(_cameraController);
        _listExecuteObject.AddExecuteObject(_inputController);
        _listExecuteObject.AddExecuteObject(_capsulePlayerController);
    }

    private void Update()
    {
        if (_listExecuteObject == null||_listExecuteObject.Length ==-1) return;
        for (var i = 0; i < _listExecuteObject.Length; i++)
        {
            var listExecuteObject = _listExecuteObject[i];

            listExecuteObject?.Execute();
        }
    }
}