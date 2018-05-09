 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

/*
	This script is a customized NetworkManager in order to make a customized UI for network connection.
*/

public class NetworkManager_Custom : NetworkManager {

    public InputField IPText;

    // Set the host if facilitator app is chosen
    public void StartupHost() {
        SetPort();
        NetworkManager.singleton.StartHost();
    }

    // Join the game using specified IP address if one of the prop apps is chosen
    public void JoinGame() {
        SetIPAddress();
        SetPort();
        NetworkManager.singleton.StartClient();
        Debug.Log("join");
    }

    // Specify the IP address
    void SetIPAddress() {
        string ipAddress = IPText.text;
        NetworkManager.singleton.networkAddress = ipAddress;
        Debug.Log("set ip");
    }

    // Set the port
    void SetPort() {
        // hardcode the port
        NetworkManager.singleton.networkPort = 7777;
    }
}
