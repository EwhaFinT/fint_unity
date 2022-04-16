using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Networking;
using System;

public class ArtChange : MonoBehaviour
{
    public string[] artURL;
    public GameObject[] frame;
    //public GameObject FrameCenter;
    //public GameObject FrameLeft;
    //public GameObject FrameRight;
    //public Material[] mts;

    [Header("Art Img")]
    public Texture art1;
    public Texture art2;
    public Texture art3;
    // Start is called before the first frame update
    void Start()
    { 
        frame = GameObject.FindGameObjectsWithTag("GalleryFrame");      //gallery내 frame의 배열
        //artURL[0] = "https://i.ibb.co/hyX44r9/flower.jpg";
        //artURL[1] = "https://i.ibb.co/TPxs5Wn/6d3feda469a1.jpg";
        //artURL[2] = "https://i.ibb.co/0BJKwM1/flower2.jpg";
        //for (int i = 0; i < frame.Length; i++)
        //    artURL[i] = //서버에서 받아오기
        for (int i=0; i<frame.Length; i++)
        {
            StartCoroutine(DownloadImage(frame[i], "https://i.ibb.co/hyX44r9/flower.jpg"));
        }
 //       MaterialChange();
        //StartCoroutine(ImageUpload());
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator DownloadImage(GameObject Frame, string MediaUrl)          //서버에서 그림 받아와서 art에 넣기
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
        {
            //액자 안 texture을 받아온 이미지로 변경
            Texture artTmp = ((DownloadHandlerTexture)request.downloadHandler).texture;
            Material[] mt = Frame.GetComponent<Renderer>().materials;
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
