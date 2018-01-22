using UnityEngine;
using Prototype.NetworkLobby;
using System.Collections;
using UnityEngine.Networking;

public class NetworkLobbyHook : LobbyHook 
{
    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
    {
            
        LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();
        NetworkPlayerHelper player = gamePlayer.GetComponent<NetworkPlayerHelper>();

        player.name = lobby.playerName;
        player.playerName = lobby.playerName;
        player.playerColor = lobby.playerColor;
        player.playerHealth = 10;

    }
}
