using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Buttons")]
    public Button voteButton;
    public Button loginButton;
    public Button boardButton;
    [Header("Popups")]
    public GameObject popupBoard;
    public GameObject popupVote;
    public GameObject popupLogin;
    public GameObject popupSignup;
    public GameObject popupWarn;
    public GameObject popupFindId;
    public GameObject popupFindPw;


    #region Singleton
    public static UIManager Instance;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        voteButton.onClick.AddListener(OnVote);
        loginButton.onClick.AddListener(OnLogin);
        boardButton.onClick.AddListener(OnBoard);
        popupWarn.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(OffWarn);
    }

    public void OnLogin()
    {
        popupLogin.SetActive(!popupLogin.activeSelf);
    }

    public void OnSignup()
    {
        popupSignup.SetActive(true);
    }

    public void OnFindId()
    {
        popupFindId.SetActive(true);
    }

    public void OnFindPw()
    {
        popupFindPw.SetActive(true);
    }

    public void OffLogin()
    {
        popupLogin.SetActive(false);
    }

    public void OffSignup()
    {
        popupSignup.SetActive(false);
    }

    public void OffFindId()
    {
        popupFindId.SetActive(false);
    }

    public void OffFindPw()
    {
        popupFindPw.SetActive(false);
    }

    public void OffWarn()
    {
        popupWarn.SetActive(false);
    }


    void OnVote()
    {
        popupVote.SetActive(!popupVote.activeSelf);
    }
    void OnBoard()
    {
        popupBoard.SetActive(!popupBoard.activeSelf);
    }

    public void PopupWarn(bool active, string msg)
    {
        if (active)
        {
            MakePopupWarn(msg);
        }
        popupWarn.SetActive(active);

    }
    void MakePopupWarn(string msg)  // ���â �����
    {
        TMP_Text message = popupWarn.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        message.text = msg;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
