using UnityEngine;
using Mirror;
public class PlayerScore : NetworkBehaviour
{
    [SyncVar]private int _score;

    [field: SyncVar]
    public int Index { get; set; }

    private void OnGUI()
    {
        GUI.Box(new Rect(10f + (Index * 110), 10f, 100f, 25f), $"Player[{Index}]: {_score:000}");
    }

    public void ChangeScore()
    {
        _score++;
    }
}
