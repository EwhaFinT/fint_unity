using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Buttons")]
    public Button voteButton;
    public Button loginButton;

    [Header("Popups")]
    public GameObject popupBoard;
    public GameObject popupVote;
    // Start is called before the first frame update
    void Start()
    {
        voteButton.onClick.AddListener(OnVote);
        loginButton.onClick.AddListener(OnLogin);
    }

    void OnLogin()
    {
        Debug.Log("On Login");
    }

    void OnVote()
    {
        popupVote.SetActive(!popupVote.activeSelf);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
