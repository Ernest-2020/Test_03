using Mirror;
using UnityEngine;

public abstract class BasePlayer : NetworkBehaviour
{
    [SerializeField] private RotationSettings rotationSettings;
    [SerializeField] private float maxDistanceLunge;
    
    [SyncVar][SerializeField] private float syncSpeedMove;
    [SyncVar][SerializeField] private float syncSpeedLunge;

    private float _speedMove;
    private float _speedLunge;

    private void Start()
    {
        if (!isServer) return;
        _speedMove = syncSpeedMove;
        _speedLunge = syncSpeedLunge;
    }

    protected RotationSettings RotationSettings => rotationSettings;
    
    protected float MaxDistanceLunge => maxDistanceLunge;
    protected float SpeedMove => _speedMove;
    protected float SpeedLunge => _speedLunge;
    
    public abstract void CmdMove(float x, float y,float z);
    public abstract void CmdLunge();
    public abstract void Rotation(BaseCamera baseCamera, float rotationX,float rotationY);
    public abstract void CmdClick(bool isClick);
    public abstract void CmdCheckDistanceSkill();
}
