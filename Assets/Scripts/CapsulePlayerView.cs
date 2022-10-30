using System.Collections;
using Mirror;
using UnityEngine;

public class CapsulePlayerView : NetworkBehaviour
{
    [SerializeField] private float delayInvulnerability;
    [SyncVar]private bool _isInvulnerability;
    [SyncVar (hook = nameof(SetColor))]private Color _playerColor;

    private Color _defaultColor;
    private CapsulePlayer _player;
    private MeshRenderer _playerMeshRenderer;
    private PlayerScore _playerScore;
    
    private void Awake()
    {
        _player = GetComponent<CapsulePlayer>();
        _playerScore = GetComponent<PlayerScore>();
        _playerMeshRenderer = GetComponent<MeshRenderer>();
        _isInvulnerability = false;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public override void OnStartServer()
    {
        var randomColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        _playerColor = randomColor;
        _defaultColor = randomColor;
    }

    private void SetColor(Color oldColor,Color newColor)
    {
        _playerMeshRenderer.material.color = newColor;
    }

    private void OnCollisionEnter(Collision other)
    {
       
        if (other.collider.TryGetComponent(out CapsulePlayerView  capsulePlayerView)&&!_isInvulnerability&&_player.isUseLunge)
        {
            if (capsulePlayerView._isInvulnerability) return;
            _player.StopLunge();
            capsulePlayerView.Collision();
            _playerScore.ChangeScore();
        }
    }
    private void Collision()
    {
        _isInvulnerability = true;
        _playerColor = Color.white;
        StartCoroutine(DelayInvulnerability());
    }
    
    private IEnumerator DelayInvulnerability()
    {
        yield return new WaitForSeconds(delayInvulnerability);
        _playerColor = _defaultColor;
        _isInvulnerability = false;
    }
}
