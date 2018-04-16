 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkManager_Custom : NetworkManager {

    public InputField IPText;

    public void StartupHost() {
        SetPort();
        NetworkManager.singleton.StartHost();
    }

    public void JoinGame() {
        SetIPAddress();
        SetPort();
        NetworkManager.singleton.StartClient();
        Debug.Log("join");
    }

    void SetIPAddress() {
        string ipAddress = IPText.text;
        NetworkManager.singleton.networkAddress = ipAddress;
        Debug.Log("set ip");
    }
    void SetPort() {
        // hardcode the port
        NetworkManager.singleton.networkPort = 7777;
    }
}
