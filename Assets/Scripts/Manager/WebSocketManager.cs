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
    public List<GameObject> playerPrefab;
    private Dictionary<string, GameObject> remoteplayer = new Dictionary<string, GameObject>();
    private string playerId = "me";
    private int playerCharacter = 1;
    private string playerCommunity = "communityId";
    private PositionData playerData;

    void Start()
    {
        webSocket = new WebSocket("ws://fintribesocket.herokuapp.com");
        webSocket.Connect();
        playerData = new PositionData(player.transform.position, playerCharacter, playerId, playerCommunity, PositionData.Command.Create);
        webSocket.Send(JsonUtility.ToJson(playerData));

        webSocket.OnMessage += (sender, e) =>
        {
            PositionData playerData = JsonUtility.FromJson<PositionData>(e.Data);
            switch (playerData.command)
            {
                case PositionData.Command.Create:
                    Debug.Log("Create");
                    _actions.Enqueue(() => CreateRemotePlayer(playerData.userId, playerData.character, playerData.position, playerData.communityId));
                    break;
                case PositionData.Command.Update:
                    _actions.Enqueue(() => MoveRemotePlayer(playerData.userId, playerData.position, playerData.communityId));
                    Debug.Log("Updated");
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

    private void Update()
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

    void FixedUpdate()
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

            string positionTmp = JsonUtility.ToJson(playerData);
            webSocket.Send(positionTmp);
            timeDelta = 0;
        }
    }

    private void CreateRemotePlayer(string userId, int character, Vector3 position, string communityId)
    {
        if (!userId.Equals(playerId))
        {
            GameObject remotePlayer = Instantiate(playerPrefab[character], position, Quaternion.identity);
            remoteplayer.Add(userId, remotePlayer);
            if ((position.y == 13 && !playerCommunity.Equals(communityId)) || (position.y == 26))
            {
                remotePlayer.SetActive(false);
            }
            else
            {
                remotePlayer.SetActive(true);
            }
        }
        //community y = 13
        //myroom y = 26

    }

    private void MoveRemotePlayer(string userId, Vector3 position, string communityId)
    {
        if (!userId.Equals(playerId))
        {
            remoteplayer[userId].transform.position = position;
            if ((position.y == 13 && !playerCommunity.Equals(communityId)) || (position.y == 26))
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

[System.Serializable]
public class PositionData
{
    public PositionData(Vector3 position, int character, string userId, string communityId, Command command)
    {
        this.position = position;
        this.character = character;
        this.userId = userId;
        this.communityId = communityId;
        this.command = command;
    }

    public enum Command
    {
        Create,
        Update,
        Delete,
    }
    public Vector3 position;
    public int character;
    public string userId;
    public string communityId;
    public Command command;
}