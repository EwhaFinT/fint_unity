using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ArtChange : MonoBehaviour
{
    public GameObject[] frame;

    public Dictionary<GameObject, string> dic;

    // Start is called before the first frame update
    void Start()
    { 
        frame = GameObject.FindGameObjectsWithTag("GalleryFrame");      //gallery내 frame의 배열
        dic = new Dictionary<GameObject, string>();
        StartCoroutine(LoadImage());
    }

    #region Singleton
    public static ArtChange Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator LoadImage()     // 서버에서 작품 리스트 받아오기
    {
        string url = "http://localhost:8080/v1/artlist";
        //string url = "https://fintribe.herokuapp.com/v1/artlist"; // TODO : 나중에 로컬 대신 배포용 해주세요

        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        string jsonString = www.downloadHandler.text;
        var response = JsonUtility.FromJson<ArtlistResponse>(jsonString);

        List<string> artId = response.artId;
        List<string> artUrl = response.paint;

        for (int i = 0; i < 3; i++)
        {
            StartCoroutine(DownloadImage(frame[i], artUrl[i]));
            dic.Add(frame[i], artId[i]);
        }

    }

    IEnumerator DownloadImage(GameObject tmp, string MediaUrl)          // 서버에서 그림 받아와서 art에 넣기
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
        {
            //액자 안 texture을 받아온 이미지로 변경
            Texture artTmp = ((DownloadHandlerTexture)request.downloadHandler).texture;
            Material[] mt = tmp.GetComponent<Renderer>().materials;
            mt[1].SetTexture("_MainTex", artTmp);
        }
    }
    //IEnumerator ImageUpload()
    //{
    //    // 이미지 base64string으로 변환
    //    FileInfo file = new FileInfo("{local_image_file_path}");
    //    byte[] byteTexture = File.ReadAllBytes(file.FullName);

    //    // 이미지 서버 post 요청
    //    string url = "https://api.imgbb.com/1/upload?key={api_key}";
    //    WWWForm form = new WWWForm();
    //    string base64string = Convert.ToBase64String(byteTexture);
    //    form.AddField("image", base64string);

    //    UnityWebRequest www = UnityWebRequest.Post(url, form);
    //    yield return www.SendWebRequest();
    //}

    //public void MaterialChange()
    //{
    //    var childrenlist = GetComponentsInChildren<ArtClick>();
        
    //    Material[] mt_left = FrameLeft.GetComponent<Renderer>().materials;
    //    Material[] mt_right = FrameRight.GetComponent<Renderer>().materials;
    //    //mt[0]은 액자틀, mt[1]은 그림
        
    //    mt_left[1].SetTexture("_MainTex", art2);
    //    mt_right[1].SetTexture("_MainTex", art3);    
    //}

}
class ArtlistResponse {
    public List<string> artId;
    public List<string> paint;
}