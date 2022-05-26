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
    public TMP_InputField Date;
    public Button submit;
    public static ProposalScript instance;

    // Start is called before the first frame update
    void Start()
    {
        exit.onClick.AddListener(onClicked_exit);
        submit.onClick.AddListener(onClicked_sumbit);
        //date.onClick.AddListener(onClicked_date);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void show()
    {
        proposal.SetActive(true);
    }
    public void onClicked_exit()
    {
        var boardPanel = UIManager.Instance.popupBoard.GetComponent<BoardScript>();

        proposal.SetActive(false);
        boardPanel.show();
    }
    void onClicked_sumbit()
    {
        StartCoroutine(PostVote());
    }
    //void onClicked_date()
    //{
    //    Debug.Log("calendar button clicked");
    //    var calenderPanel = UIManager.Instance.popupCalender.GetComponent<CalendarController>();
    //    Debug.Log("calendar panel: " + calenderPanel);
    //   //calenderPanel.ShowCalendar(date);
    //}

    IEnumerator PostVote()
    {
        //string UserId = "62689f6564ebad668621db42";
        string UserId = Manager.Instance.ID;
        string CommunityId = "627f5ca702867d106384ef8f";
        //string CommunityId = CommunityManager.Instance.CommunityID;

        var time = DateTime.Now;
        Debug.Log("datetime now: "+time);

        Debug.Log("price: " + Price.text);
        Debug.Log("content:" + Content.text);

        string url = Manager.Instance.url + "v1/vote-proposal";

        ProposeRequest proposeRequest = new ProposeRequest
        {
            communityId = CommunityId,
            userId = UserId,
            title = Content.text,
            resalePrice = double.Parse(Price.text),
            endTime = Date.text
        };
        string jsonBody = JsonUtility.ToJson(proposeRequest);
        Debug.Log(proposeRequest);

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
        var response = JsonUtility.FromJson<ProposeResponse>(jsonString);
        Debug.Log("vote response: " + response);

        www.disposeUploadHandlerOnDispose = true;
        www.disposeDownloadHandlerOnDispose = true;

        var PostPopup = UIManager.Instance.popUpPostAnnouncement.GetComponent<PostPopupScript>();
        PostPopup.MakePopupWarn(status);

    }
}

[Serializable]
class ProposeRequest
{
    public string communityId;
    public string userId;
    public string title;
    public double resalePrice;
    public string endTime;
}

[Serializable]
class ProposeResponse
{
    public string proposeSuccess;
}