using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class TurnManager : NetworkBehaviour
{
    private List<Player> players = new List<Player>();
    public void AddPlayer(Player _player)
    {
        players.Add(_player);
    }
}
