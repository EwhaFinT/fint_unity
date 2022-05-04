using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PostPopupScript : MonoBehaviour
{
    public GameObject PostPopUp;
    public Button exit;

    // Start is called before the first frame update
    void Start()
    {
        exit.onClick.AddListener(onClicked_exit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MakePopupWarn(string msg)  // ???? ?????
    {
        TMP_Text message = PostPopUp.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        message.text = msg;
        PostPopUp.SetActive(true);
    }
    void onClicked_exit()
    {
        var postPanel = UIManager.Instance.popupPost.GetComponent<PostScript>();

        PostPopUp.SetActive(false);
        postPanel.onClicked_exit();
    }
}
