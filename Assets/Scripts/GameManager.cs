using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using System.Globalization;
using UnityEngine.UI;

public class GameManager : NetworkBehaviour
{
    [SyncVar]
    public int globalJacks;

    public Text globalJacksText;
    public Text jacksText;

    public GameObject jackPrefab;

    public void Start()
    {
        if (isServer)
        {
            for (int i = 0; i < 5; i++)
            {
                Vector2 position = new Vector2(UnityEngine.Random.Range(-5f, 5f), UnityEngine.Random.Range(-5f, 5f));
                GameObject prefab = Instantiate(jackPrefab, position, Quaternion.identity);
                NetworkServer.Spawn(prefab);
            }
        }
    }



    // Start is called before the first frame update
    public void StopGame()
    {
        if (NetworkServer.active && NetworkClient.isConnected)
        {
            NetworkManager.singleton.StopHost();
        }

        else if (NetworkClient.isConnected)
        {
            NetworkManager.singleton.StopClient();
        }

        else if (NetworkServer.active)
        {
            NetworkManager.singleton.StopServer();
        }
    }

}
