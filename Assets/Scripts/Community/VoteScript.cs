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
    public Button YesBtn;
    public Button NoBtn;
    public bool temp;

    void Start()
    {
        // vote.SetActive(false);
        exit.onClick.AddListener(onClicked_exit);
        StartCoroutine(LoadVote());
        YesBtn.onClick.AddListener(onClicked_Yes);
        NoBtn.onClick.AddListener(onClicked_No);
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
    void onClicked_Yes()
    {
        temp = true;
        StartCoroutine(VotePost());
    }
    void onClicked_No()
    {
        temp = false;
        StartCoroutine(VotePost());
    }
    IEnumerator LoadVote()
    {
        ObjectId voteid = new ObjectId("6231edd26f3140647415ebcf");
        //ObjectId CommunityId = new ObjectId("6231d5883dfcf54107e14310");
        ObjectId CommunityId = new ObjectId("6231f585aeee2e2cc44bfa90");

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
    IEnumerator VotePost()
    {
        string UserId = "62689f6564ebad668621db42";
        string CommunityId = "6231f585aeee2e2cc44bfa90";
        string VoteId = "6231edd26f3140647415ebcf";

        string url = "https://fintribe.herokuapp.com/v1/vote";

        VoteRequest voteRequest = new VoteRequest
        {
            userId = UserId,
            communityId = CommunityId,
            voteId = VoteId,
            choice = temp

        };
        string jsonBody = JsonUtility.ToJson(voteRequest);


        UnityWebRequest www = UnityWebRequest.Post(url, jsonBody);

        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonBody);
        www.uploadHandler = new UploadHandlerRaw(jsonToSend);
        www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();

        long status = www.responseCode;
        Debug.Log("status: " + status);


        // string -> json
        string jsonString = www.downloadHandler.text;
        var response = JsonUtility.FromJson<VoteResponse>(jsonString);
        //var PostPopup = UIManager.Instance.popUpPostAnnouncement.GetComponent<PostPopupScript>();
        //PostPopup.MakePopupWarn(status);
        Debug.Log("vote post status: " + status);

        www.disposeUploadHandlerOnDispose = true;
        www.disposeDownloadHandlerOnDispose = true;
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

[Serializable]
class VoteRequest
{
    public string userId;
    public string communityId;
    public string voteId;
    public bool choice;
}

[Serializable]
class VoteResponse
{
    public string voteSuccess;
}