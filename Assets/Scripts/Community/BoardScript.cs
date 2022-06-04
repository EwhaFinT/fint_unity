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
    public GameObject articleprev, listcontent;

    [Header("Comment")]
    public TextMeshProUGUI ReplyID;
    public TextMeshProUGUI ReplyTimestamp;
    public TextMeshProUGUI ReplyContent;
    public TMP_InputField CommentInput;
    public Button CommentRegister;
    public GameObject commentprev, commentcontent, articlepf;

    public string articleId;


    // Start is called before the first frame update
    void Start()
    {
        // board.SetActive(false);
        voteBtn.onClick.AddListener(onClicked_vote);
        BoardExitBtn.onClick.AddListener(onClicked_exit);
        WriteBtn.onClick.AddListener(onClicked_write);
        ProposalBtn.onClick.AddListener(onClicked_proposal);
        

        CommentRegister.onClick.AddListener(onClicked_comment);
        //articleId = "6231f66a15ffd20d91c1b10e";
        StartCoroutine(LoadArticleList());
    }

    // Update is called once per frame
    void Update()
    {
        //StartCoroutine(LoadArticle());
    }

    public void show()
    {
        board.SetActive(true);
        StartCoroutine(LoadArticle());
        //StartCoroutine(LoadArticleList());
    }

    public void LoadArticleUpdate()
    {
        StartCoroutine(LoadArticle());
        Debug.Log("load article updated");
    }

    IEnumerator LoadArticleList()
    {
        //ObjectId id = new ObjectId("627f5ca702867d106384ef8f");
        string CommunityId = CommunityManager.Instance.CommunityID;

        string url = Manager.Instance.url + "v1/articles?communityId=" + CommunityId;

        Debug.Log("LoadArticleList: " + url);
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        string jsonString = www.downloadHandler.text;
        var response = JsonUtility.FromJson<ArticlesResponse>(jsonString);
        //Debug.Log("board response: " + jsonString);
        //Debug.Log("response test: " + response.articles);
        
        if (response.articles.Count != 0)
        {
            Debug.Log("------------load article list change id-----------" + response.articles);
            changeArticleId(response.articles[0].articleId);
            Debug.Log("first article id: " + response.articles[0].articleId);
        }
        else { Debug.Log("aricle is empty"); }
        articleInit(response);
    }

    void articleInit(ArticlesResponse response)
    {
        if (listcontent.transform.childCount > 1)
        {
            for (int i = 1; i < listcontent.transform.childCount; i++)
            {
                Destroy(listcontent.transform.GetChild(i).gameObject);
            }
            Debug.Log("Destory clone all");
        }
        for (int i = 0; i < response.articles.Count; i++)
        {
            //GameObject prev = Instantiate(articleprev);
            //prev.transform.SetParent(content.transform, false);

            GameObject prev = Instantiate(articleprev, listcontent.transform);

            var articleBtn = prev.GetComponent<articlepr>();
            articleBtn.GetArticleInfo(response.articles[i].title, response.articles[i].createdAt, response.articles[i].articleId);
        }
    }

    public void changeArticleId(string id)
    {
        articleId = id;
        Debug.Log("****article changed: " + articleId);
        StartCoroutine(LoadArticle());
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
        StartCoroutine(LoadArticle());
        CommentInput.text = string.Empty;
    }

    IEnumerator LoadArticle()
    {
        //ArticleTitle = GetComponent<TextMeshPro>();
        //ObjectId id = new ObjectId("6231f66a15ffd20d91c1b10e");
        //ObjectId id = new ObjectId(articleId);
        //articleId = "6231f66a15ffd20d91c1b10e";

        string url = Manager.Instance.url + "v1/article?articleId=" + articleId;

        Debug.Log("***load article url: "+url);

        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        string jsonString = www.downloadHandler.text;
        var response = JsonUtility.FromJson<LoadBoardResponse>(jsonString);

        Debug.Log("json : " + jsonString);
        //Debug.Log("time: " + response.article.createdAt.ToString()); // TODO

        //DateTime timeFromJson = JsonUtility.FromJson<JsonDateTime>(response.article.createdAt);

        //ArticleTitle.text = response.article.title.ToString();
        //ArticleTimestamp.text = "작성시간| " + response.article.createdAt + "\n작성자| " + response.article.identity;
        ////ArticleTimestamp.text = "????| " + response.article.createdAt.ToString("yyyy/MM/dd HH:mm") + "\n" + "???| " + response.article.identity.ToString();
        //ArticleContent.text = response.article.content.ToString();

        Debug.Log("comment response: " + response.comments);
        commentInit(response);
    }

    void commentInit(LoadBoardResponse response)
    {
        Debug.Log("childeCount: " + commentcontent.transform.childCount);
        if (commentcontent.transform.childCount > 2)
        {
            for (int i = 2; i < commentcontent.transform.childCount; i++)
            {
                Destroy(commentcontent.transform.GetChild(i).gameObject);
            }
            Debug.Log("Destory clone all");
        }

        GameObject artic = Instantiate(articlepf, commentcontent.transform);

        var oneArticle = artic.GetComponent<articlepf>();
        bool tf = false;
        if(articleId != "62984aff7a30725036d64c4b") { tf = true; }
        Debug.Log("****articleId: " + articleId + "*****tf: " + tf);
        oneArticle.GetArticleInfo(response.article.title, response.article.createdAt, response.article.identity, response.article.content, tf);
        
        for (int i = 0; i < response.comments.Count; i++)
        {
            //GameObject prev = Instantiate(commentprev);
            //prev.transform.SetParent(commentcontent.transform, false);
            GameObject prev = Instantiate(commentprev, commentcontent.transform);
            Debug.Log("help : comment");
            var comment = prev.GetComponent<commentpr>();
            comment.GetReplyInfo(response.comments[i].identity, response.comments[i].createdAt, response.comments[i].content);
            prev.SetActive(true);
        }
    }

    IEnumerator PostComment()
    {
        string UserId = Manager.Instance.ID;
        //string ArticleId = "6231d5883dfcf54107e14364";

        string url = Manager.Instance.url + "v1/comment";

        CommentRequest commentRequest = new CommentRequest
        {
            userId = UserId,
            articleId = articleId,
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

[Serializable]
class ArticlesResponse
{ 
    public List<ArticlesResTmp> articles;
}

[Serializable]
public class ArticlesResTmp
{
    public string articleId;
    public string title;
    public string createdAt;
}