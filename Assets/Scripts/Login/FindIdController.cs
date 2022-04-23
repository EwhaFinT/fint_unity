using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
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
        StartCoroutine(FindId());
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

    IEnumerator FindId()
    {
        string url = "https://fintribe.herokuapp.com/v1/find-id?name=" + userName.text + "&phone=" + phone.text;

        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        // string -> json
        string jsonString = www.downloadHandler.text;
        var response = JsonUtility.FromJson<FindIdResponse>(jsonString);

        if (response.identity == null)    // 아이디 찾기 실패
        {
            var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
            popupWarn.MakePopupWarn("해당 정보를 가진\n사용자가 존재하지 않습니다.");
        }
        else
        {
            var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
            popupWarn.MakePopupWarn(userName.text + "님의 아이디는\n" + response.identity + " 입니다.");
        }
    }
}

class FindIdResponse
{
    public string identity;
}
