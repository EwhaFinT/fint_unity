using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using MongoDB.Bson;
using TMPro;

public class BoardScript : MonoBehaviour
{
    public GameObject board;
    public Button voteBtn;
    public Button BoardExitBtn;
    public Button WriteBtn;
    public Button ProposalBtn;

    [Header("Article")]
    public TextMeshProUGUI ArticleTitle;
    public TextMeshProUGUI ArticleTimestamp;
    public TextMeshProUGUI ArticleContent;

    [Header("Comment")]
    public TextMeshProUGUI ReplyID;
    public TextMeshProUGUI ReplyTimestamp;
    public TextMeshProUGUI ReplyContent;
    public TMP_InputField CommentInput;
    public Button CommentRegister;


    // Start is called before the first frame update
    void Start()
    {
        // board.SetActive(false);
        voteBtn.onClick.AddListener(onClicked_vote);
        BoardExitBtn.onClick.AddListener(onClicked_exit);
        WriteBtn.onClick.AddListener(onClicked_write);
        ProposalBtn.onClick.AddListener(onClicked_proposal);
        StartCoroutine(LoadArticle());

        CommentRegister.onClick.AddListener(onClicked_comment);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void show()
    {
        board.SetActive(true);

    }

    void onClicked_vote()
    {
        board.SetActive(false);

        var votePanel = UIManager.Instance.popupVote.GetComponent<VoteScript>();
        votePanel.show();
    }

    void onClicked_exit()
    {
        board.SetActive(false);
    }

    void onClicked_write()
    {
        board.SetActive(false);

        var postPanel = UIManager.Instance.popupPost.GetComponent<PostScript>();
        postPanel.show();
    }

    void onClicked_proposal()
    {
        board.SetActive(false);

        var proposalPanel = UIManager.Instance.popUpProposal.GetComponent<ProposalScript>();
        proposalPanel.show();
    }

    void onClicked_comment()
    {
        StartCoroutine(PostComment());
        //CommentInput.text = "??? ?????.";
    }

    IEnumerator LoadArticle()
    {
        //ArticleTitle = GetComponent<TextMeshPro>();
        ObjectId id = new ObjectId("6231f66a15ffd20d91c1b10e");

        string url = "https://fintribe.herokuapp.com/v1/article?articleId=" + id;

        Debug.Log(url);

        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        string jsonString = www.downloadHandler.text;
        var response = JsonUtility.FromJson<LoadBoardResponse>(jsonString);

        Debug.Log("json : " + jsonString);
        Debug.Log("time: " + response.article.createdAt.ToString());

        //DateTime timeFromJson = JsonUtility.FromJson<JsonDateTime>(response.article.createdAt);

        ArticleTitle.text = response.article.title.ToString();
        ArticleTimestamp.text = "????| " + response.article.createdAt;
        //ArticleTimestamp.text = "????| " + response.article.createdAt.ToString("yyyy/MM/dd HH:mm") + "\n" + "???| " + response.article.identity.ToString();
        ArticleContent.text = response.article.content.ToString();

        Debug.Log("comment response: " + response.comments);
    }
    IEnumerator PostComment()
    {
        string UserId = "6250073f634945502a92cbbe";
        string ArticleId = "6231d5883dfcf54107e14364";

        string url = "https://fintribe.herokuapp.com/v1/comment";

        CommentRequest commentRequest = new CommentRequest
        {
            userId = UserId,
            articleId = ArticleId,
            tagUser = null,
            content = CommentInput.text,
            tagCommentId = -1
        };
        string jsonBody = JsonUtility.ToJson(commentRequest);
        UnityWebRequest www = UnityWebRequest.Post(url, jsonBody);

        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonBody);
        www.uploadHandler = new UploadHandlerRaw(jsonToSend);
        www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();

        long status = www.responseCode;
        Debug.Log("status: " + status);

        string jsonString = www.downloadHandler.text;
        var response = JsonUtility.FromJson<ArticlePostResponse>(jsonString);
        var PostPopup = UIManager.Instance.popUpPostAnnouncement.GetComponent<PostPopupScript>();
        PostPopup.MakePopupWarn(status);

        www.disposeUploadHandlerOnDispose = true;
        www.disposeDownloadHandlerOnDispose = true;
    }
}
[Serializable]
class Article
{
    public ObjectId articleId;
    public ObjectId communityId;
    public ObjectId userId;
    public string identity;
    public string title;
    public string content;
    public string createdAt;
    public string updatedAt;
    public bool isDeleted;
}

[Serializable]
class Comment
{
    public int commentId;
    public ObjectId articleId;
    public string content;
    public ObjectId userId;
    public string identity;
    public string createdAt;
    public string updatedAt;
    public bool isDeleted;
}

[Serializable]
class ReComment
{
    public int reCommentId;
    public int tagCommentId;
    public ObjectId articleId;
    public string content;
    public ObjectId userId;
    public string identity;
    public DateTime createdAt;
    public DateTime updatedAt;
    public ObjectId tagUser;
    public string tagUserIdentity;
    public bool isDeleted;
}

[Serializable]
class LoadBoardResponse
{
    public Article article;
    public string articleId;
    public List<Comment> comments;
    public List<ReComment> reComments;
}

[Serializable]
class CommentRequest
{
    public string userId;
    public string articleId;
    public string tagUser;
    public string content;
    public int tagCommentId;
}

[Serializable]
class CommentResponse
{
    public string commentSuccess;
}