using UnityEngine;
using WebSocketSharp;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System;

public class WebSocketManager : MonoBehaviour
{
    private readonly ConcurrentQueue<Action> _actions = new ConcurrentQueue<Action>();
    private WebSocket webSocket = null;
    private Vector3 position;
    public GameObject player;
    public GameObject mainCamera;
    public List<GameObject> playerPrefab;
    private Dictionary<string, GameObject> remoteplayer = new Dictionary<string, GameObject>();
    private string playerId;
    private string playerCommunity = "tmp";
    private PositionData playerData;

    public void Start()
    {
        playerId = Manager.Instance.ID;
        Quaternion rot = Camera.main.transform.rotation;
        webSocket = new WebSocket("wss://fintribenode.herokuapp.com");
        webSocket.SslConfiguration.EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12;
        webSocket.Connect();
        playerData = new PositionData(player.transform.position, playerId, playerCommunity, PositionData.Command.Create, rot);
        webSocket.Send(JsonUtility.ToJson(playerData));

        webSocket.OnMessage += (sender, e) =>
        {
            PositionData playerData = JsonUtility.FromJson<PositionData>(e.Data);
            switch (playerData.command)
            {
                case PositionData.Command.Create:
                    _actions.Enqueue(() => CreateRemotePlayer(playerData.userId, playerData.position, playerData.communityId, playerData.rotation));
                    break;
                case PositionData.Command.Update:
                    _actions.Enqueue(() => MoveRemotePlayer(playerData.userId, playerData.position, playerData.communityId, playerData.rotation));
                    break;
                case PositionData.Command.Delete:
                    DeleteRemotePlayer(playerData.userId);
                    break;
                default:
                    Debug.Log("socket error");
                    break;
            }
        };
    }

    private double timeDelta = 0;


    public void Update()
    {
        // Work the dispatched actions on the Unity main thread
        while (_actions.Count > 0)
        {
            if (_actions.TryDequeue(out var action))
            {
                action?.Invoke();
            }
        }
    }

    public void FixedUpdate()
    {
        timeDelta += Time.deltaTime;
        if (timeDelta > 10 / 60)
        {
            if (webSocket == null)
            {
            }
            position = player.transform.position;
            playerData.position = position;
            playerData.command = PositionData.Command.Update;
            playerData.rotation = Camera.main.transform.rotation;
            if(Manager.Instance.ID != null)
            {
                playerData.userId = Manager.Instance.ID;
            }

            if(CommunityManager.Instance.CommunityID != null)
            {
                playerData.communityId = CommunityManager.Instance.CommunityID;
            }

            string positionTmp = JsonUtility.ToJson(playerData);
            webSocket.Send(positionTmp);
            timeDelta = 0;
        }
    }

    private void CreateRemotePlayer(string userId, Vector3 position, string communityId, Quaternion rotation)
    {
        int tmp = remoteplayer.Count;
        if (!userId.Equals(playerId) && !remoteplayer.ContainsKey(userId))
        {
            remoteplayer.Add(userId, Instantiate(playerPrefab[tmp % 5], position, rotation));
            if ((position.y >= 13 && !playerCommunity.Equals(communityId)) || (position.y >= 26))
            {
                Debug.Log("false activate");
                remoteplayer[userId].SetActive(false);
            }
            else
            {
                Debug.Log("activate");
                remoteplayer[userId].SetActive(true);
            }
        }
        else if (remoteplayer.ContainsKey(userId))
        {
            MoveRemotePlayer(userId, position, communityId, rotation);
        }
    }

    private void MoveRemotePlayer(string userId, Vector3 position, string communityId, Quaternion rotation)
    {
        int tmp = remoteplayer.Count;
        if (!userId.Equals(playerId))
        {
            if (!remoteplayer.ContainsKey(userId))
            {
                GameObject remotePlayer = Instantiate(playerPrefab[tmp%5], position, rotation);
                Debug.Log(remotePlayer);
                remoteplayer.Add(userId, remotePlayer);
            }
            remoteplayer[userId].transform.position = position;
            remoteplayer[userId].transform.rotation = rotation;
            Debug.Log("position updated");
            if ((position.y >= 13 && !playerCommunity.Equals(communityId)) || (position.y >= 26))
            {
                remoteplayer[userId].SetActive(false);
            }
            else
            {
                remoteplayer[userId].SetActive(true);
            }
        }
    }

    private void DeleteRemotePlayer(string userId)
    {
        if (!userId.Equals(playerId))
            remoteplayer.Remove(userId);
    }
}

[Serializable]
public class PositionData
{
    public PositionData(Vector3 position, string userId, string communityId, Command command, Quaternion rotation)
    {
        this.position = position;
        this.userId = userId;
        this.communityId = communityId;
        this.command = command;
        this.rotation = rotation;
    }

    public enum Command
    {
        Create,
        Update,
        Delete,
    }
    public Vector3 position;
    public string userId;
    public string communityId;
    public Command command;
    public Quaternion rotation;
}