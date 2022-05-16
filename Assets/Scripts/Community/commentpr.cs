using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class commentpr : MonoBehaviour
{
    public TextMeshProUGUI replyId;
    public TextMeshProUGUI timestamp;
    public TextMeshProUGUI replyContent;

    // Start is called before the first frame update
    void Start()
    {

    }
    public void GetReplyInfo(string userIdentity, string timestamp, string content)
    {
        this.replyId.text = userIdentity;
        this.timestamp.text = timestamp;
        this.replyContent.text = content;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
