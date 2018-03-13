using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConnectionManager : MonoBehaviour
{
	public Text photonStatusText;
	public Text playerNameText;
	public Text photonCurrentRoomText;

	public InputField photonRoomToJoinText;



	void Awake()
	{
		// Connect to the main photon server
		if (!PhotonNetwork.connectedAndReady) PhotonNetwork.ConnectUsingSettings(Globals.VERSION);

		// create and seta random  player name
		PhotonNetwork.playerName = "Player" + Random.Range(1000, 9999);
		playerNameText.text = "PlayerName: " + PhotonNetwork.playerName;
		photonCurrentRoomText.text = "Room: (no room)";
	}

	void UpdateRoomInfo()
	{
		if (PhotonNetwork.room == null) // not in a room
		{
			photonCurrentRoomText.text = "Room: (no room)";
		} 
		else // in a room
		{
			photonCurrentRoomText.text = "Room: " + PhotonNetwork.room.Name + " (" + PhotonNetwork.room.PlayerCount + ")";
		}
	}

	// BUTTON HANDLERS

	public void ButtonHandlerCreateRoom() 
	{
		if (PhotonNetwork.connectedAndReady && photonRoomToJoinText.text.Length > 0) // check there is a name entered before creating
		{
			PhotonNetwork.CreateRoom(photonRoomToJoinText.text);
		}
	}

	public void ButtonHandlerJoinRoom() 
	{
		if (PhotonNetwork.connectedAndReady && photonRoomToJoinText.text.Length > 0) // check there is a name entered before joining
		{
			PhotonNetwork.JoinRoom (photonRoomToJoinText.text);
        }
    }

	public void ButtonHandlerLeaveRoom() 
	{
		if (PhotonNetwork.connectedAndReady) 
		{
			PhotonNetwork.LeaveRoom();
        }
    }

	// EVENT CALLBACKS

	void OnConnectedToMaster() 
	{
		Debug.Log("OnConnectedToMaster!");
		photonStatusText.text = "Status: Connected";
		UpdateRoomInfo ();
	}

	void OnFailedToConnectToPhoton()
	{
		Debug.Log("OnFailedToConnectToPhoton");
		photonStatusText.text = "Status: Connection Failed";
		UpdateRoomInfo ();
	}

	void OnCreatedRoom()
	{
		Debug.Log("OnCreatedRoom: " + PhotonNetwork.room.name);	
		UpdateRoomInfo ();
	}

	void OnPhotonCreateRoomFailed()
	{
		Debug.Log("OnPhotonCreateRoomFailed");
		UpdateRoomInfo ();
	}

	void OnJoinedRoom()
	{
		Debug.Log("OnJoinedRoom: " + PhotonNetwork.room.name);	
		UpdateRoomInfo ();
        GameObject snowboarder = PhotonNetwork.Instantiate("NetworkedSnowboarder", new Vector3(123.3915f, 0.1103587f, 31.88877f), Quaternion.identity, 0);
    }

    void OnPhotonPlayerConnected() {
		Debug.Log("OnPhotonPlayerConnected");
		UpdateRoomInfo ();
	}

	void OnPhotonPlayerDisconnected() {
		Debug.Log("OnPhotonPlayerDisconnected");
		UpdateRoomInfo ();
	}

	void OnPhotonJoinRoomFailed()
	{
		Debug.Log("OnPhotonJoinRoomFailed");
		photonStatusText.text = "Status: Join Room Failed!";
		UpdateRoomInfo ();
	}

	void OnLeftRoom()
	{
		Debug.Log("OnLeftRoom");
		photonStatusText.text = "Status: Left Room!";
		UpdateRoomInfo ();
    }
}