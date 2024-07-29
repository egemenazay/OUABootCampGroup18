using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class MyNetworkRoomManager : NetworkRoomManager
{
    public override void OnRoomServerPlayersReady()
    {
        // Tüm oyuncular hazır olduğunda oyunu başlat
        if (allPlayersReady)
        {
            ServerChangeScene(GameplayScene);
        }
    }
}
