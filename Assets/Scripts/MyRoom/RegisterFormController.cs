using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using System;
using System.Text.RegularExpressions;
using UnityEngine.Networking;
using System.IO;
using Newtonsoft.Json.Linq;

public class RegisterFormController : MonoBehaviour
{
    string path, artUrl, artId;
    string auction_date = "";
    public Text filePath;
    bool success = false;

    // 사용자 입력값
    public TMP_InputField title;           // 작품명
    public TMP_InputField author;          // 작가
    public TMP_InputField reservedPrice;   // 경매 시작가
    public Text auctionDate;               // 경매 일자
    public TMP_InputField description;     // 작품 설명
    //public Button btn_right, btn_left;
    //public GameObject firstPanel, secondPanel;
    public GameObject warningWindow;    // 경고창
    public GameObject calendar;

    public Button btn_close, btn_FindFile, btn_upload, btn_pickDate;
    void Start()
    {
        btn_close.onClick.AddListener(Onclicked_close);
        btn_FindFile.onClick.AddListener(OpenExplorerButtonClick);
        btn_upload.onClick.AddListener(RegisterButtonClick);
        btn_pickDate.onClick.AddListener(ShowCalendar);
        //warningWindow = GameObject.Find("WarningWindow");
        //warningWindow.SetActive(false);
    }
    public void Onclicked_close()
    {
        gameObject.SetActive(false);
    }


    public void ShowCalendar()
    {
        calendar.SetActive(true);
    }

    bool CheckValidation()  // 유효성 검사
    {
        var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
        string today = DateTime.UtcNow.ToString("yyyy-MM-dd");

        if (path == null || path.Length < 4)
        {
            popupWarn.MakePopupWarn("작품 파일을 선택하세요");
            return false;
        }
        if (title.text.Length < 2)
        {
            popupWarn.MakePopupWarn("작품명은\n2글자 이상이어야 합니다.");
            return false;
        }
        if (author.text.Length < 2)
        {
            popupWarn.MakePopupWarn("작가 이름은\n2글자 이상이어야 합니다.");
            return false;
        }
        if (reservedPrice.text.Length < 1)
        {
            popupWarn.MakePopupWarn("경매 시작가를 입력하세요.");
            return false;
        }
        if (auction_date == "")
        {
            popupWarn.MakePopupWarn("경매 일자를 선택하세요.");
            return false;
        }
        if(description.text.Length < 3)
        {
            popupWarn.MakePopupWarn("작품 설명은\n3글자 이상이어야 합니다.");
            return false;

        }
        return true;
    }

    void RegisterButtonClick()   // 작품 등록 버튼 클릭시
    {
        if (CheckValidation())   // 유효성 검사 통과
        {
            SendNewFileInfo();

            // ==== 서버와 통신하는 부분====
            StartCoroutine(ImageUpload(path));
        }
    }


    public void OpenExplorerButtonClick()   // 파일 탐색기 버튼 클릭시
    {
        // (pc 기준으로 구현한 것, 모바일로 할 경우 변경해야 할 듯)
        path = EditorUtility.OpenFilePanel("내 작품 등록하기", "", "jpg");
        GetImage();
        //StartCoroutine(ImageUpload(path));
    }

    IEnumerator ImageUpload(string path)
    {
        Debug.Log("start upload img");
        // 이미지 base64string으로 변환
        FileInfo file = new FileInfo(path);
        byte[] byteTexture = File.ReadAllBytes(file.FullName);

        // 이미지 서버 post 요청
        string url = "https://api.imgbb.com/1/upload?key=8bb1d0057129896bc0546e05caf616b4";
        WWWForm form = new WWWForm();
        string base64string = Convert.ToBase64String(byteTexture);
        form.AddField("image", base64string);

        UnityWebRequest www = UnityWebRequest.Post(url, form);
        www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        yield return www.SendWebRequest();

        string jsonString = www.downloadHandler.text;
        var data = JObject.Parse(jsonString);

        artUrl = data["data"]["url"].ToString();         //get art url part from jsonString

        Debug.Log("complete upload img");

        StartCoroutine(SendReguisterInfo());
    }

    IEnumerator SendReguisterInfo()
    {
        
        string url = Manager.Instance.url + "v1/upload";

        
        //DateTime tmp = new DateTime(2022, 05, 23);
        //Debug.Log(tmp);
        //Debug.Log(tmp.Date);
        double.TryParse(reservedPrice.text, out double price);
        UploadRequest uploadRequest = new UploadRequest
        {
            userId = Manager.Instance.ID,
            painter = author.text,
            artName = title.text,
            detail = description.text,
            price = price,
            paint = artUrl,
//            paint = "",       //for test (not upload)
            auctionDate = auction_date
        };

        string jsonBody = JsonUtility.ToJson(uploadRequest);

        UnityWebRequest www = UnityWebRequest.Post(url, jsonBody);
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonBody);
        www.uploadHandler = new UploadHandlerRaw(jsonToSend);
        www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();

        string jsonString = www.downloadHandler.text;
        var response = JsonUtility.FromJson<UploadResponse>(jsonString);

        if (response.artId == null)
        {
            var popupWarn = UIManager.Instance.popupWarn.GetComponent<PopupWarnController>();
            success = false;
            popupWarn.MakePopupWarn("작품 업로드 실패");
        }
        else
        {

            var panel = UIManager.Instance.popupSuccess.GetComponent<PopupSuccessController>();
            panel.MakePopupMessage("작품 업로드에 성공하였습니다.");
            artId = response.artId;
            InitializeForm();           //작품 등록폼 초기화
            Debug.Log("complete register");


            Onclicked_close();
        }
            
    }

    void GetImage()     // 작품 선택시 버튼 text 변경하기
    {
        if(path != null)
        {
            filePath.text = path;
        }
    }

    public void GetDate(string date)
    {
        auction_date = date+ "T00:00:00";
        Debug.Log(auction_date);
    }

    void InitializeForm()   // 작품 등록 폼 초기화
    {
        path = null;
        filePath.text = "파일 탐색";
        ClearInputField(title);
        ClearInputField(author);
        ClearInputField(description);
        ClearInputField(reservedPrice);
        auctionDate.text = "경매 일자 선택";
    }

    void ClearInputField(TMP_InputField inputField)
    {
        inputField.text = "";
    }

    void SendNewFileInfo()
    {
        GameObject emptyGameObject = GameObject.Instantiate(
                new GameObject(filePath.text),
                this.transform.position,
                Quaternion.identity
                );
        emptyGameObject.transform.SetParent(this.transform);
    }
}
class UploadRequest
{
    public string userId;
    public string painter; // 작가
    public string artName; // 작품명
    public string detail;
    public double price; // 경매가
    public string paint; // 이미지 url 주소
    public string auctionDate;
}

class UploadResponse
{
    public string artId;
}