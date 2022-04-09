using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FindIdController : MonoBehaviour
{
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

    void FindIdBtnClick()
    {
        FindIdRequest request = new FindIdRequest
        {
            userName = userName.text,
            phone = phone.text
        };
        Debug.Log("name: " + request.userName);
        // ==== �鿣��� ��� �ʿ� ====
    }

    void FindPwBtnClick()
    {
        OffFindId();
        UIManager.Instance.OnFindPw();
    }
    void OffFindId()
    {
        UIManager.Instance.OffFindId();
        UIManager.Instance.OffWarn();
    }
}

class FindIdRequest
{
    public string userName;
    public string phone;
}
