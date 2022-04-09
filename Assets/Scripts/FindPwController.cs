using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FindPwController : MonoBehaviour
{
    public TMP_InputField identity;
    public TMP_InputField email;
    public Button findPwBtn;
    public Button findIdBtn;
    public Button exitBtn;

    // Start is called before the first frame update
    void Start()
    {
        findIdBtn.onClick.AddListener(FindIdBtnClick);
        findPwBtn.onClick.AddListener(FindPwBtnClick);
        exitBtn.onClick.AddListener(OffFindPw);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FindPwBtnClick()
    {
        FindPwRequest request = new FindPwRequest
        {
            identity = identity.text,
            email = email.text
        };
        Debug.Log("id: " + request.identity);
        // ==== 백엔드와 통신 필요 ====
    }

    void FindIdBtnClick()
    {
        OffFindPw();
        UIManager.Instance.OnFindId();
    }

    void OffFindPw()
    {
        UIManager.Instance.OffFindPw();
        UIManager.Instance.OffWarn();
    }
}

class FindPwRequest
{
    public string identity;
    public string email;
}