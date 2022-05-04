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



    // Start is called before the first frame update
    void Start()
    {
        // board.SetActive(false);
        voteBtn.onClick.AddListener(onClicked_vote);
        BoardExitBtn.onClick.AddListener(onClicked_exit);
        WriteBtn.onClick.AddListener(onClicked_write);
        ProposalBtn.onClick.AddListener(onClicked_proposal);
        StartCoroutine(LoadArticle());

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
        ArticleTimestamp.text = "작성일자| " + response.article.createdAt.ToString("yyyy/MM/dd HH:mm") + "\n" + "작성자| " + response.article.identity.ToString();
        ArticleContent.text = response.article.content.ToString();

        Debug.Log("comment response: " + response.comments);
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
    public DateTime createdAt;
    public DateTime updatedAt;
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
    public DateTime createdAt;
    public DateTime updatedAt;
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
struct JsonDateTime
{
    public long value;
    public static implicit operator DateTime(JsonDateTime jdt)
    {
        Debug.Log("Converted to time");
        return DateTime.FromFileTimeUtc(jdt.value);
    }
    public static implicit operator JsonDateTime(DateTime dt)
    {
        Debug.Log("Converted to JDT");
        JsonDateTime jdt = new JsonDateTime();
        jdt.value = dt.ToFileTimeUtc();
        return jdt;
    }
}