using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateHost : NetworkBehaviour
{
    [SerializeField] private Button host;
    [SerializeField] private Button client;
    [SerializeField] private TMP_InputField clientName;
    [SerializeField] private TMP_InputField chat;
    [SerializeField] private NetworkManager networkManager;

    private void Awake()
    {
        host.onClick.AddListener(StartHost);
        client.onClick.AddListener(StartClient);
        Debug.Log(clientName.text);
    }

    
    private void StartClient()
    {
        networkManager.StartClient();
        chat.text += $"Connect {clientName.text}/";
    }

    [ClientRpc]
    private void StartHost()
    {
        networkManager.StartHost();
        chat.text += $"Connect {clientName.text}/";
    }
}
