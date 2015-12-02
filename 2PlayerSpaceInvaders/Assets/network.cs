using UnityEngine;
using System.Collections;

public class network : MonoBehaviour
{
    private string gameName = "edu.gcc.wb.NetworkPongV1";  // You need to change this for your game!
    private bool refreshingHostList = false;
    private bool hostDataFound = false;
    private HostData[] hostData;

	public GameObject PLAYER;

    private bool puckInstantiated = false;
    private bool gameStarted;
    GUIStyle customButtonStyle;
    GUIStyle customMessageStyle;
    public bool useAWSserver = false;
    public string AWS_URL;
    public GameObject cameraClient;

	bool calledLoad=false;

    // Use this for initialization
    void Start()
    {


        //Modify the Master server and facilitator attributes to connect to my locally
        //hosted server instead of the Unity HQ Master Server
        if (useAWSserver)
        {
            //Use the following four lines if using the class AWS Master Server
            MasterServer.ipAddress = AWS_URL; //"54.187.184.133"; //"10.37.101.31";
            MasterServer.port = 23466;
            Network.natFacilitatorIP = AWS_URL; //"54.187.184.133"; //"10.37.101.31";
            Network.natFacilitatorPort = 50005;
        }
        Network.sendRate = 60;
    }

    //Host a server and register it to the master server
    void startServer()
    {
        Network.InitializeServer(2, 25001, !Network.HavePublicAddress());
        MasterServer.RegisterHost(gameName, "NetworkTestWB", "Testing  stuff");


        //GUILayout.EndArea ();


    }

    void exitServer()
    {
        Network.Disconnect();
    }

    void back()
    {
        Application.LoadLevel(1);
    }

    //Get the list of servers from the Master Server
    void refreshHostList()
    {
        MasterServer.RequestHostList(gameName);
        refreshingHostList = true;
        Debug.Log("Getting Host List");
    }

    //Create a player that can be controlled by the user
    void spawnPlayer()
    {
		Network.Instantiate (PLAYER, new Vector3 (0, 1, 0), Quaternion.identity, 0);
    }
    void OnPlayerConnected(NetworkPlayer player)
    {
        gameStarted = true;

        spawnPlayer();
    }

    //Messages
    void OnServerInitialized()
    {
        Debug.Log("Server initialized");
        //spawnPlayer ();
    }

    void OnConnectedToServer()
    {
        Debug.Log("Connected to server");
        spawnPlayer();
    }

    void OnPlayerDisconnected()
    {
        //Network.Disconnect (500);
        //MasterServer.UnregisterHost();
        Application.LoadLevel(0);

    }

    void OnDisconnectedFromServer()
    {
        Application.LoadLevel(0);
    }

    void OnMasterServerEvent(MasterServerEvent mse)
    {
        if (mse == MasterServerEvent.RegistrationSucceeded)
        {
            Debug.Log("Server registered");
        }
    }

    //Update functions for GUI and Per-frame update
    void OnGUI()
    {
        if (customButtonStyle == null)
        {
            customButtonStyle = new GUIStyle(GUI.skin.button);
            customButtonStyle.fontSize = (int)((float)(Screen.width) * 0.0125f);
        }
        if (!Network.isClient && !Network.isServer) {
			GUILayout.BeginArea (new Rect (Screen.width * .05f, Screen.height * .05f, Screen.width * 0.1f, Screen.height * 1f));
			if (GUILayout.Button ("Start Server", customButtonStyle)) {
				Debug.Log ("Starting Server");
				startServer ();
			}

			if (GUILayout.Button ("Back", customButtonStyle)) {
				Debug.Log ("Back to menu");
				back ();
			}

			if (GUILayout.Button ("Refresh Host List", customButtonStyle)) {
				Debug.Log ("Refreshing...");
				refreshHostList ();
			}

			if (hostDataFound) {
				Debug.Log ("Host data recieved");
				for (int i = 0; i < hostData.Length; i++) {

					if (GUILayout.Button (hostData [i].gameName, customButtonStyle)) {
						Network.Connect (hostData [i]);

					}
				}
			}
			GUILayout.EndArea ();
		} else if (Network.isServer && !gameStarted) {

			customMessageStyle = new GUIStyle (GUI.skin.label);
			customMessageStyle.fontSize = (int)((float)(Screen.width) * 0.0125f);

			GUILayout.BeginArea (new Rect (Screen.width * .05f, Screen.height * .05f, Screen.width * 0.1f, Screen.height * 1f));
			GUILayout.Label ("Waiting for player to join...", customMessageStyle);
			if (GUILayout.Button ("Quit Hosting", customButtonStyle)) {
				Debug.Log ("Exiting Server");
				exitServer ();
			}
			GUILayout.EndArea ();
		} else if (Network.isServer && gameStarted && !calledLoad){
				calledLoad = GameObject.Find("Main Camera").GetComponent<enemyController>().LoadEnemys();
		}
    }

    void Update()
    {
        //If we have started to look for available servers, look every frame until we find one.
        if (refreshingHostList)
        {
            if (MasterServer.PollHostList().Length > 0)
            {
                refreshingHostList = false;
                hostDataFound = true;
                hostData = MasterServer.PollHostList();
            }
        }


        if (!gameStarted)
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button3))
            {
                startServer();
            }
            else if (Input.GetKeyDown(KeyCode.Joystick1Button8))
            {
                Debug.Log("Refreshing...");
                refreshHostList();
            }
            else if (Input.GetKeyDown(KeyCode.Joystick1Button0))
            {
                if (hostDataFound)
                {
                    Debug.Log("Host data recieved");
                    Network.Connect(hostData[0]);
                }
            }
            else if (Input.GetKeyDown(KeyCode.Joystick1Button1))
            {
                if (Network.isServer)
                {
                    Debug.Log("Exiting Server");
                    exitServer();
                }
                else
                {
                    Debug.Log("Back to menu");
                    // back();
                }
            }
        }
    }


}


