using Mirror;
using UnityEngine;

public class CustomNetworkRoomManager : NetworkRoomManager
{
    private bool _showStartButton;

    public override bool OnRoomServerSceneLoadedForPlayer(
        NetworkConnectionToClient conn, 
        GameObject roomPlayer,
        GameObject gamePlayer)
    {
        var playerScore = gamePlayer.GetComponent<PlayerScore>();
        playerScore.Index = roomPlayer.GetComponent<NetworkRoomPlayer>().index;
        return true;
    }
    public override void OnRoomServerPlayersReady()
    {
#if UNITY_SERVER
            base.OnRoomServerPlayersReady();
#else
        _showStartButton = true;
#endif
    }

    public override void OnGUI()
    {
        base.OnGUI();

        if (!allPlayersReady || !_showStartButton || 
            !GUI.Button(new Rect(150, 300, 120, 20), 
                "START GAME")) 
            return;
        _showStartButton = false;

        ServerChangeScene(GameplayScene);
    }

 
}
