using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using System;

public class ProposalScript : MonoBehaviour
{
    public GameObject proposal;
    public Button exit;

    public TMP_InputField Price;
    public TMP_InputField Content;
    public Button date;
    public Button submit;

    // Start is called before the first frame update
    void Start()
    {
        exit.onClick.AddListener(onClicked_exit);
        submit.onClick.AddListener(onClicked_sumbit);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void show()
    {
        proposal.SetActive(true);
    }
    void onClicked_exit()
    {
        var boardPanel = UIManager.Instance.popupBoard.GetComponent<BoardScript>();

        proposal.SetActive(false);
        boardPanel.show();
    }
    void onClicked_sumbit()
    {
        StartCoroutine(PostVote());
    }

    IEnumerator PostVote()
    {
        string UserId = "6250073f634945502a92cbbe";
        string CommunityId = "6231d5883dfcf54107e14310";
        var time = DateTime.Now;
        Debug.Log("datetime now: "+time);

        Debug.Log("price: " + Price.text);
        Debug.Log("content:" + Content.text);

        string url = "https://fintribe.herokuapp.com/v1/vote-proposal";

        VoteRequest voteRequest = new VoteRequest
        {
            communityId = CommunityId,
            userId = UserId,
            title = Content.text,
            resalePrice = double.Parse(Price.text),
            endTime = "2022-06-01T12:00:00"
        };
        string jsonBody = JsonUtility.ToJson(voteRequest);
        Debug.Log(voteRequest);

        UnityWebRequest www = UnityWebRequest.Post(url, jsonBody);

        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonBody);
        www.uploadHandler = new UploadHandlerRaw(jsonToSend);
        www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();

        long status = www.responseCode;
        Debug.Log("vote post status: " + status);


        // string -> json
        string jsonString = www.downloadHandler.text;
        var response = JsonUtility.FromJson<VoteResponse>(jsonString);
        Debug.Log("vote response: " + response);

        www.disposeUploadHandlerOnDispose = true;
        www.disposeDownloadHandlerOnDispose = true;
    }
}

[Serializable]
class VoteRequest
{
    public string communityId;
    public string userId;
    public string title;
    public double resalePrice;
    public string endTime;
}

[Serializable]
class VoteResponse
{
    public string proposeSuccess;
}