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
        string today = DateTime.Now.ToString("yyyy-MM-dd");

        if (path == null || path.Length < 4)
        {
            MakeWarningWindow("작품 파일을 선택하세요");
            return false;
        }
        if (title.text.Length < 2)
        {
            MakeWarningWindow("작품명은\n2글자 이상이어야 합니다.");
            return false;
        }
        if (author.text.Length < 2)
        {
            MakeWarningWindow("작가 이름은\n2글자 이상이어야 합니다.");
            return false;
        }
        if (reservedPrice.text.Length < 1)
        {
            MakeWarningWindow("경매 시작가를 입력하세요.");
            return false;
        }
        //if (!Regex.IsMatch(auctionDate.text, @"^(20|21)\d{2}-(0[1-9]|1[012])-(0[1-9]|[12][0-9]|3[0-1])$"))
        //{
        //    MakeWarningWindow("경매 일자를 선택하세요.");
        //    return false;
        //}
        if (String.Compare(auctionDate.text, today) != 1)
        {
            MakeWarningWindow("경매 일자는\n익일 이후여야 합니다.");
            return false;
        }
        if(description.text.Length < 3)
        {
            MakeWarningWindow("작품 설명은\n3글자 이상이어야 합니다.");
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

            // ==== NFT API 사용해서 NFT 주소도 만들어야 함 ====

            // 작품 성공적으로 등록시 작품 등록 폼 닫음
            if (success == true)
            {
                InitializeForm();
                gameObject.SetActive(false);
            }

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
            auctionDate = "2022-05-10"
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
            MakeWarningWindow("작품 업로드 실패");
            success = false;
        }
        else
        {
            artId = response.artId;
            success = true;
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
        string tmp_date = date;
        Debug.Log(date);
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

    void MakeWarningWindow(string msg)  // 경고창 만들기
    {
        TMP_Text message = warningWindow.transform.GetChild(0).GetComponent<TMP_Text>();
        message.text = msg;
        warningWindow.SetActive(true);
    }

    public void WarningWindowCloseButtonClick()     // 경고창 닫기
    {
        warningWindow.SetActive(false);
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