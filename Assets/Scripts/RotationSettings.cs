using UnityEngine;
[CreateAssetMenu(fileName = "New Rotation Settings",menuName = "Game/"+ nameof(RotationSettings), order = 0)]
public class RotationSettings : ScriptableObject
{
    [SerializeField] private float smoothTime;
    [SerializeField] private float sensitivity;
    [SerializeField] private float minRotationY;
    [SerializeField] private float maxRotationY;
    
    public float MinRotationY => minRotationY;
    
    public float MaxRotationY => maxRotationY;
    public float SmoothTime => smoothTime;
    public float Sensitivity => sensitivity;
}
