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
    public TextMeshProUGUI StakePersonal;
    public TextMeshProUGUI YesPercent;
    public TextMeshProUGUI NoPercent;
    public Button YesBtn;
    public Button NoBtn;
    //public bool temp;
    public double klay;
    public string vtId, paint_url;
    public RawImage NFT;

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
        //get paint
        GetPaint();
        //StartCoroutine(DownloadImage(paint_url));
    }
    public void onClicked_exit()
    {
        var boardPanel = UIManager.Instance.popupBoard.GetComponent<BoardScript>();

        vote.SetActive(false);
        boardPanel.show();
    }
    void onClicked_Yes()
    {
        //temp = true;
        StartCoroutine(VotePost(true));
        ReloadVote();
    }
    void onClicked_No()
    {
        //temp = false;
        StartCoroutine(VotePost(false));
        ReloadVote();
    }

    public void ReloadVote()
    {
        var boardPanel = UIManager.Instance.popupBoard.GetComponent<BoardScript>();
        boardPanel.onClicked_exit();

        StartCoroutine(LoadVote());
    }

    public void GetPaint()
    {
        NFT.texture = CommunityManager.Instance.PostPaint();
        Debug.Log("change vote paint");
    }

    //IEnumerator DownloadImage(string paint_url)          //서버에서 그림 받아와서 art에 넣기
    //{
    //    UnityWebRequest request = UnityWebRequestTexture.GetTexture(paint_url);
    //    yield return request.SendWebRequest();
    //    if (request.isNetworkError || request.isHttpError)
    //        Debug.Log(request.error);
    //    else
    //    {
    //        //인벤토리 슬롯을 해당 이미지로 변경
    //        NFT.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
    //        Debug.Log("change vote paint");
    //    }
    //}

    IEnumerator LoadVote()
    {
        //ObjectId voteid = new ObjectId("6231edd26f3140647415ebcf");
        //ObjectId CommunityId = new ObjectId("627f5ca702867d106384ef8f");
        //ObjectId UserId = new ObjectId("62689f6564ebad668621db42");
        string CommunityId = CommunityManager.Instance.CommunityID;
        string UserId = Manager.Instance.ID;

        string url = Manager.Instance.url + "v1/check?communityId=" + CommunityId + "&userId=" + UserId;

        Debug.Log(url);

        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        string jsonString = www.downloadHandler.text;
        var response = JsonUtility.FromJson<VoteCheckResponse>(jsonString);

        Debug.Log("vote check responese: " + jsonString);
        Debug.Log("----participants: " + response.voteParticipants.ToString());
        vtId = response.voteId.ToString();

        proposer.text = "제안자: "+ response.identity.ToString();
        price.text = "제안가격: " + response.resalePrice.ToString();
        content.text = response.title.ToString();

        YesPercent.text = "YES\n" + (response.agreement * 100).ToString() + " %";
        NoPercent.text = "NO\n" + (response.disagreement * 100).ToString() + " %";

        StakePersonal.text = (response.ratio * 100).ToString() + " %";

        foreach( string idt in response.voteParticipants)
        {
            if(idt == response.userId)
            {
                YesBtn.interactable = false;
                NoBtn.interactable = false;
            }
        }

    }
    IEnumerator VotePost(bool tf)
    {
        //string UserId = "62689f6564ebad668621db42";
        string UserId = Manager.Instance.ID;
        //string CommunityId = "627f5ca702867d106384ef8f";
        string CommunityId = CommunityManager.Instance.CommunityID;
        //string VoteId = "6231edd26f3140647415ebcf";

        string url = Manager.Instance.url + "v1/vote";

        VoteRequest voteRequest = new VoteRequest
        {
            userId = UserId,
            communityId = CommunityId,
            voteId = vtId,
            choice = tf,
            ratio = klay
        };
        string jsonBody = JsonUtility.ToJson(voteRequest);


        UnityWebRequest www = UnityWebRequest.Post(url, jsonBody);

        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonBody);
        www.uploadHandler = new UploadHandlerRaw(jsonToSend);
        www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();

        long status = www.responseCode;

        // string -> json
        string jsonString = www.downloadHandler.text;
        var response = JsonUtility.FromJson<VoteResponse>(jsonString);
        //var PostPopup = UIManager.Instance.popUpPostAnnouncement.GetComponent<PostPopupScript>();
        //PostPopup.MakePopupWarn(status);
        Debug.Log("vote post status: " + status);

        var PostPopup = UIManager.Instance.popUpPostAnnouncement.GetComponent<PostPopupScript>();
        PostPopup.MakePopupWarn(status);

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
    public double ratio;
    public List<string> voteParticipants;
    public VoteCheckResponse(String msg)
    {
        voteId = msg;
    }
}
//class VoteCheckResponse
//{
//    public string voteId;
//    public string userId;
//    public string identity;
//    public string title;
//    public double resalePrice;
//    public string startTime;
//    public string endTime;
//    public bool isDeleted;
//    public double agreement;
//    public double disagreement;
//    public double ratio;
//}

[Serializable]
class VoteRequest
{
    public string userId;
    public string communityId;
    public string voteId;
    public bool choice;
    public double ratio;
}

[Serializable]
class VoteResponse
{
    public string voteSuccess;
}