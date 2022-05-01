using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class FindPwController : MonoBehaviour
{
    public GameObject findPw;
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

    public void show()
    {
        findPw.SetActive(true);
    }

    void FindPwBtnClick()
    {
        StartCoroutine(FindPw());
    }

    void FindIdBtnClick()
    {
        OffFindPw();
        var findid = UIManager.Instance.popupFindId.GetComponent<FindIdController>();
        findid.show();
    }

    void OffFindPw()
    {
        var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
        popupWarn.OffWarn();
        findPw.SetActive(false);
    }

    IEnumerator FindPw()
    {
        string url = "https://fintribe.herokuapp.com/v1/find-pw?identity=" + identity.text + "&email=" + email.text;

        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        // string -> json
        string jsonString = www.downloadHandler.text;
        var response = JsonUtility.FromJson<FindPwResponse>(jsonString);

        if (!response.emailSuccess)    // ��й�ȣ ã�� ����
        {
            var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
            popupWarn.MakePopupWarn("�ش� ������ ��ġ�ϴ�\n����ڰ� �����ϴ�.");
        }
        else
        {
            var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
            popupWarn.MakePopupWarn("�ش� �̸��� �ּҷ�\n�ӽ� ��й�ȣ�� �����߽��ϴ�.");
        }
    }
}

class FindPwResponse
{
    public bool emailSuccess;
}