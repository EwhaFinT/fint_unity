using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using MongoDB.Bson;
using TMPro;

public class VoteScript : MonoBehaviour
{   
    public GameObject vote;
    public Button exit;

    [Header("Proposal")]
    public TextMeshProUGUI proposer;
    public TextMeshProUGUI price;
    public TextMeshProUGUI content;

    [Header("Vote")]
    public TextMeshProUGUI YesPercent;
    public TextMeshProUGUI NoPercent;

    void Start()
    {
        // vote.SetActive(false);
        exit.onClick.AddListener(onClicked_exit);
        StartCoroutine(LoadVote());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void show()
    {
        vote.SetActive(true);
    }
    void onClicked_exit()
    {
        var boardPanel = UIManager.Instance.popupBoard.GetComponent<BoardScript>();

        vote.SetActive(false);
        boardPanel.show();
    }
    IEnumerator LoadVote()
    {
        ObjectId voteid = new ObjectId("6231edd26f3140647415ebcf");
        ObjectId CommunityId = new ObjectId("6231d5883dfcf54107e14310");

        string url = "https://fintribe.herokuapp.com/v1/check?communityId=" + CommunityId + "&voteId=" + voteid;

        Debug.Log(url);

        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        string jsonString = www.downloadHandler.text;
        var response = JsonUtility.FromJson<VoteCheckResponse>(jsonString);

        proposer.text = "제안자: "+ response.identity.ToString();
        price.text = "제안가격: " + response.resalePrice.ToString();
        content.text = response.title.ToString();

        YesPercent.text = "YES\n" + response.agreement.ToString() + " %";
        NoPercent.text = "NO\n" + response.disagreement.ToString() + " %";



        
    }
}

[Serializable]
class VoteCheckResponse
{
    public string voteId;
    public string userId;
    public string identity;
    public string title;
    public double resalePrice;
    public string startTime;
    public string endTime;
    public bool isDeleted;
    public double agreement;
    public double disagreement;
}