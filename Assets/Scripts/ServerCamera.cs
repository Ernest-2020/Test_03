using Mirror;
using UnityEngine;

public class ServerCamera : NetworkBehaviour
{
    [SerializeField] private new GameObject camera;
    private void Start()
    {
        camera.SetActive(isServerOnly);
    }


}
