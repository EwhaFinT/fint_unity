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
    public Button artInfoButton;
    public Button teleportButton;
    public Button inventoryButton;
    public Button registerButton;

    [Header("Popups - MainHall")]
    public GameObject popupAuction;
    public GameObject popupArtInfo;

    [Header("Popups - Community")]
    public GameObject popupBoard;
    public GameObject popupVote;
    public GameObject popupPost;
    public GameObject popUpProposal;
    
    [Header("Popups - etc")]
    public GameObject popupLogin;
    public GameObject popupSignup;

    public GameObject popupWarn;
    public GameObject popupFindId;
    public GameObject popupFindPw;
    public GameObject popupTeleport;

    [Header("Popups - MyRoom")]
    public GameObject myroomPanel;
    public GameObject popupInventory;
    public GameObject popupRegister;

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
        artInfoButton.onClick.AddListener(OnArtInfo);
        voteButton.onClick.AddListener(OnVote);
        loginButton.onClick.AddListener(OnLogin);
        boardButton.onClick.AddListener(OnBoard);
        inventoryButton.onClick.AddListener(OnInventory);
        registerButton.onClick.AddListener(OnRegister);
        teleportButton.onClick.AddListener(OnTeleport);

    }


    public void OnInventory()
    {
        popupInventory.SetActive(!popupInventory.activeSelf);
    }
    public void OnRegister()
    {
        popupRegister.SetActive(!popupRegister.activeSelf);
    }

    public void OnLogin()
    {
        popupLogin.SetActive(!popupLogin.activeSelf);
    }

    void OnVote()
    {
        popupVote.SetActive(!popupVote.activeSelf);
    }
    void OnBoard()
    {
        Debug.Log("OnBoard");
        popupBoard.SetActive(!popupBoard.activeSelf);

    }

    public void OnArtInfo()
    {
        popupArtInfo.SetActive(!popupArtInfo.activeSelf);
    }
    public void OnAuciton()
    {
        popupAuction.SetActive(!popupAuction.activeSelf);
    }

    public void OnTeleport()
    {
        popupTeleport.SetActive(!popupAuction.activeSelf);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
