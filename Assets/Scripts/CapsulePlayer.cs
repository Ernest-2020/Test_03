using Mirror;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterController))]
public sealed class CapsulePlayer : BasePlayer
{
    private CharacterController _characterController;
    private Rigidbody _rigidbodyPlayer;
    private Transform _transformPlayer;

    private Vector3 _playerPositionStartUseSkill;

    private float _currentDistance;
    private float _yRotation;

    [field: SyncVar] public bool isUseLunge { get; private set; }

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _rigidbodyPlayer = GetComponent<Rigidbody>();
        _transformPlayer = transform;
        isUseLunge = false;
    }

    [Command]
    public override void CmdMove(float x, float y, float z)
    {
        if (isUseLunge) return;
        var direction = new Vector3(x,y,z);
        direction = Vector3.ClampMagnitude(direction, 1f);
        direction = transform.TransformDirection(direction);
        direction *= SpeedMove;
        _characterController.SimpleMove(direction);
    }
    [Command]
    public override void CmdLunge()
    {
        if (!isUseLunge) return;
        if (_currentDistance >= MaxDistanceLunge)
        {
            _rigidbodyPlayer.constraints = RigidbodyConstraints.None |
                                           RigidbodyConstraints.FreezeRotationX | 
                                           RigidbodyConstraints.FreezeRotationZ;

            isUseLunge = false;
            return;
        }
        
        var position = _transformPlayer.position;
        _rigidbodyPlayer.constraints = RigidbodyConstraints.FreezeAll;
        _transformPlayer.position = Vector3.MoveTowards(position,
            position+_transformPlayer.forward, SpeedLunge * Time.deltaTime);
    }

    public void StopLunge()
    {
        isUseLunge = false;
        _rigidbodyPlayer.constraints = RigidbodyConstraints.None |
                                       RigidbodyConstraints.FreezeRotationX | 
                                       RigidbodyConstraints.FreezeRotationZ;
    }


    public override void Rotation(BaseCamera baseCamera, float rotationX, float rotationY)
    {
        if (isUseLunge) return;
        CmdRotationPlayer(rotationX);
        baseCamera.CameraRotation(rotationX,rotationY);
    }

    [Command]
    private void CmdRotationPlayer(float rotationX)
    {
        if (isUseLunge) return;
        _yRotation += rotationX*RotationSettings.Sensitivity;
        _transformPlayer.rotation = Quaternion.Lerp(_transformPlayer.rotation,Quaternion.Euler(0,_yRotation,0), RotationSettings.SmoothTime);
    }
    [Command]
    public override void CmdClick(bool isClick)
    {
        UseLunge(isClick);
    }
    [Command]
    public override void CmdCheckDistanceSkill()
    {
        if (!isUseLunge){return;}
        _currentDistance = Vector3.Distance(_playerPositionStartUseSkill, _transformPlayer.position);
    }

    private void UseLunge(bool isClick)
    {
        if (!isClick||isUseLunge) return;
        _currentDistance = 0;
        _playerPositionStartUseSkill = _transformPlayer.position;
        isUseLunge = true;
    }
}
