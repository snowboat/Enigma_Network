using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkTest : NetworkBehaviour {
    public GameObject clock;
    public GameObject radio;
    public GameObject phone;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Space))
            CmdSpawnObjects();

   
            if (Input.GetKeyDown(KeyCode.C))
                RpcSendMessage("Request from Server");
        
    }

    void OnConnectedToServer()
    {
        Debug.Log("new client join!");
    }

    [Command]
    void CmdSpawnObjects()
    {
        GameObject propClock = Instantiate(clock);
        GameObject propRadio = Instantiate(radio);
        GameObject propPhone = Instantiate(phone);

        NetworkServer.SpawnWithClientAuthority(propClock, connectionToClient);
        NetworkServer.SpawnWithClientAuthority(propRadio, connectionToClient);
        NetworkServer.SpawnWithClientAuthority(propPhone, connectionToClient);

        if (NetworkServer.connections != null)
        {
            foreach (NetworkConnection p in NetworkServer.connections)
            {
                Debug.Log(p.connectionId);
                p.logNetworkMessages = true;
             
            }
        }
    }

    [ClientRpc]
    void RpcSendMessage(string message)
    {

            Debug.Log(message);
            GameObject test = GameObject.FindGameObjectWithTag("Player");
            test.GetComponent<Renderer>().material.color = new Color(0, 0, 255);
        
    }

    

}
