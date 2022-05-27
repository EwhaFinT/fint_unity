using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using System.Text.RegularExpressions;

public class SignupController : MonoBehaviour
{
    public GameObject signup;
    public TMP_InputField identity;
    public TMP_InputField password;
    public TMP_InputField passwordConfirm;
    public TMP_InputField userName;
    public TMP_InputField phone;
    public TMP_InputField email;
    public Button checkDupIdBtn;
    public Button signupBtn;
    public Button exitBtn;

    bool dupCheck = false;

    void Start()
    {
        checkDupIdBtn.onClick.AddListener(CheckDupIdClick);
        signupBtn.onClick.AddListener(SignupBtnClick);
        exitBtn.onClick.AddListener(OffSignup);
    }

    void Update()
    {
        
    }

    public void show()
    {
        signup.SetActive(true);
    }

    void CheckDupIdClick()
    {
        StartCoroutine(IdCheck());
    }

    void SignupBtnClick()
    {
        if(CheckValidation())
        {
            StartCoroutine(Signup());
        }
    }

    bool CheckValidation()
    {
        if (!CheckId()) return false;
        if (!CheckPassword()) return false;
        if (!CheckPasswordConfirm()) return false;
        if (!CheckName()) return false;
        if (!CheckPhone()) return false;
        if (!CheckEmail()) return false ;
        return true;
    }

    bool CheckId()
    {
        if (identity.text.Length < 1)
        {
            var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
            popupWarn.MakePopupWarn("酒捞叼甫 涝仿窍技夸.");
            return false;
        }
        if(!dupCheck)
        {
            var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
            popupWarn.MakePopupWarn("酒捞叼 吝汗 犬牢阑\n柳青秦林技夸.");
            return false;
        }
        return true;
    }

    bool CheckPassword()
    {
        string val = password.text;
        if (val.Length < 1)
        {
            var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
            popupWarn.MakePopupWarn("厚剐锅龋甫 涝仿窍技夸.");
            return false;
        }
        Regex eng = new Regex(@"[a-zA-Z]");
        Regex num = new Regex(@"[0-9]");
        if (val.Length < 8 || !eng.IsMatch(val) || !num.IsMatch(val))
        {
            var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
            popupWarn.MakePopupWarn("康巩, 箭磊甫 窍唱 捞惑 器窃茄\n8磊府 厚剐锅龋甫 涝仿窍技夸.");
            return false;
        }
        return true;
    }

    bool CheckPasswordConfirm()
    {
        if (password.text != passwordConfirm.text)
        {
            var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
            popupWarn.MakePopupWarn("厚剐锅龋啊 老摹窍瘤 臼嚼聪促.");
            return false;
        }
        return true;
    }

    bool CheckName()
    {
        string val = userName.text;
        if (val.Length < 1)
        {
            var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
            popupWarn.MakePopupWarn("捞抚阑 涝仿窍技夸.");
            return false;
        }
        Regex kor = new Regex(@"^[啊-芌]+$");
        if (val.Length < 2 || val.Length > 5 || !kor.IsMatch(val))
        {
            var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
            popupWarn.MakePopupWarn("2臂磊 捞惑 5臂磊 捞窍\n茄臂父 涝仿 啊瓷钦聪促.");
            return false;
        }
        return true;
    }

    bool CheckPhone()
    {
        string val = phone.text;
        if (val.Length < 1)
        {
            var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
            popupWarn.MakePopupWarn("傈拳锅龋甫 涝仿窍技夸.");
            return false;
        }
        Regex num = new Regex(@"^01[016789][^0][0-9]{2,3}[0-9]{3,4}$");
        if(!num.IsMatch(val))
        {
            var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
            popupWarn.MakePopupWarn("棵官福瘤 臼篮\n傈拳锅龋 涝聪促.");
            return false;
        }
        return true;
    }
    bool CheckEmail()
    {
        string val = email.text;
        if (val.Length < 1)
        {
            var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
            popupWarn.MakePopupWarn("捞皋老阑 涝仿窍技夸.");
            return false;
        }
        Regex mail = new Regex(@"[0-9a-zA-Z]([-_\.]?[0-9a-zA-Z])*@[0-9a-zA-Z]([-_\.]?[0-9a-zA-Z])*\.[a-zA-Z]{2,3}");
        if(!mail.IsMatch(val))
        {
            var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
            popupWarn.MakePopupWarn("棵官福瘤 臼篮\n捞皋老 林家 涝聪促..");
            return false;
        }
        return true;
    }

    void OffSignup()
    {
        var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
        popupWarn.OffWarn();
        signup.SetActive(false);
    }

    IEnumerator IdCheck() // 酒捞叼 吝汗 八荤 夸没
    {
        string url = Manager.Instance.url + "v1/check-id?identity=" + identity.text;

        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        // string -> json
        string jsonString = www.downloadHandler.text;
        var response = JsonUtility.FromJson<IdCheckResponse>(jsonString);

        if (response.idCheckSuccess == 0)    // 酒捞叼 吝汗
        {
            var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
            popupWarn.MakePopupWarn("吝汗等 酒捞叼啊 粮犁钦聪促.");
        }
        else                                // 荤侩且 荐 乐绰 酒捞叼
        {
            var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
            popupWarn.MakePopupWarn("荤侩 啊瓷茄 酒捞叼涝聪促.");
            dupCheck = true;
        }
    }

    IEnumerator Signup() // 酒捞叼 吝汗 八荤 夸没
    {
        string url = Manager.Instance.url + "v1/signup";

        SignupRequest signupRequest = new SignupRequest
        {
            identity = identity.text,
            password = password.text,
            name = userName.text,
            phone = phone.text,
            email = email.text
        }; 
        string jsonBody = JsonUtility.ToJson(signupRequest);

        UnityWebRequest www = UnityWebRequest.Post(url, jsonBody);
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonBody);
        www.uploadHandler = new UploadHandlerRaw(jsonToSend);
        www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();

        // string -> json
        string jsonString = www.downloadHandler.text;
        var response = JsonUtility.FromJson<SignupResponse>(jsonString);

        if (response.signupSuccess == 0)    // 雀盔啊涝 角菩
        {
            var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
            popupWarn.MakePopupWarn("吝汗等 酒捞叼啊 粮犁钦聪促.");
            dupCheck = false;
        }
        else                                // 雀盔啊涝 己傍
        {
            OffSignup();
            UIManager.Instance.OnLogin();
        }
    }
}

class IdCheckResponse
{
    public int idCheckSuccess;
}

class SignupRequest
{
    public string identity;
    public string password;
    public string name;
    public string phone;
    public string email;
}

class SignupResponse
{
    public int signupSuccess;
}