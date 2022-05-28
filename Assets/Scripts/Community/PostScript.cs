using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MongoDB.Bson;
using UnityEngine.Networking;
using TMPro;
using System;

public class PostScript : MonoBehaviour
{   
    public GameObject post;
    public Button exit;

    public TMP_InputField Title;
    public TMP_InputField Content;
    public Button submit;

    // Start is called before the first frame update
    void Start()
    {
        // post.SetActive(false);
        exit.onClick.AddListener(onClicked_exit);
        submit.onClick.AddListener(onClicked_sumbit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void show()
    {
        post.SetActive(true);
    }
    public void onClicked_exit()
    {
        var boardPanel = UIManager.Instance.popupBoard.GetComponent<BoardScript>();

        post.SetActive(false);
        boardPanel.show();
    }
    void onClicked_sumbit()
    {
        StartCoroutine(Post_Article());
        var boardPanel = UIManager.Instance.popupBoard.GetComponent<BoardScript>();

    }
    IEnumerator Post_Article()
    {
        //string UserId = "6250073f634945502a92cbbe";
        //string CommunityId = "6231f585aeee2e2cc44bfa90";
        string UserId = Manager.Instance.ID;
        string CommunityId = CommunityManager.Instance.CommunityID;

        //string Identity = "userIdentity";

        Debug.Log("title: " + Title.text);
        Debug.Log("content:" + Content.text);

        string url = Manager.Instance.url + "v1/article";

        PostRequest postRequest = new PostRequest
        {
            userId = UserId,
            title = Title.text,
            content = Content.text,
            communityId = CommunityId
            //identity = Identity
        }; 
        string jsonBody = JsonUtility.ToJson(postRequest);


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
        var response = JsonUtility.FromJson<ArticlePostResponse>(jsonString);
        var PostPopup = UIManager.Instance.popUpPostAnnouncement.GetComponent<PostPopupScript>();
        PostPopup.MakePopupWarn(status);

        www.disposeUploadHandlerOnDispose = true;
        www.disposeDownloadHandlerOnDispose = true;
    }
}

[Serializable]
class PostRequest
{
    public string userId;
    public string title;
    public string content;
    public string communityId;
}

[Serializable]
class ArticlePostResponse
{
    public string postSuccess;
}