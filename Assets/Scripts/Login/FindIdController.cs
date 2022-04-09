using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FindIdController : MonoBehaviour
{
    public GameObject findId;
    public TMP_InputField userName;
    public TMP_InputField phone;
    public Button findIdBtn;
    public Button findPwBtn;
    public Button exitBtn;

    // Start is called before the first frame update
    void Start()
    {
        findIdBtn.onClick.AddListener(FindIdBtnClick);
        findPwBtn.onClick.AddListener(FindPwBtnClick);
        exitBtn.onClick.AddListener(OffFindId);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void show()
    {
        findId.SetActive(true);
    }

    void FindIdBtnClick()
    {
        FindIdRequest request = new FindIdRequest
        {
            userName = userName.text,
            phone = phone.text
        };
        Debug.Log("name: " + request.userName);
        // ==== 백엔드와 통신 필요 ====
    }

    void FindPwBtnClick()
    {
        OffFindId();
        var findPw = UIManager.Instance.popupFindPw.GetComponent<FindPwController>();
        findPw.show();
    }
    void OffFindId()
    {
        findId.SetActive(false);
        var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
        popupWarn.OffWarn();
    }
}

class FindIdRequest
{
    public string userName;
    public string phone;
}
