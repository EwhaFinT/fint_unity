using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class articlepf : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI timestamp;
    public TextMeshProUGUI content;

    // Start is called before the first frame update
    void Start()
    {

    }
    public void GetArticleInfo(string title, string timestamp, string identity, string content)
    {
        this.title.text = title;
        this.timestamp.text = "작성일자| " + timestamp + "\n작성자| " + identity;
        this.content.text = content;

        Debug.Log("article prefab: " + title + timestamp + identity + content);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
