using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class articlepr : MonoBehaviour
{
    public Button article;
    public TextMeshProUGUI title;
    public TextMeshProUGUI timestamp;
    public string articleId;

    // Start is called before the first frame update
    void Start()
    {
        article.onClick.AddListener(Onclicked_Article);
    }
    public void GetArticleInfo(string title, string createdAt, string articleId)
    {
        this.title.text = title;
        this.timestamp.text = createdAt;
        this.articleId = articleId;
    }
    public void Onclicked_Article()
    {
        Debug.Log("article clicked");
        Debug.Log("article info: " + title.text + timestamp.text + "articleId: " + articleId);

        var boardPanel = UIManager.Instance.popupBoard.GetComponent<BoardScript>();
        boardPanel.changeArticleId(articleId);
        boardPanel.LoadArticleUpdate();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
