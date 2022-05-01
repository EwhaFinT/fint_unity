using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ArtChange : MonoBehaviour
{
    public string[] artURL;
    public GameObject[] frame;
    // Start is called before the first frame update
    void Start()
    { 
        frame = GameObject.FindGameObjectsWithTag("GalleryFrame");      //gallery�� frame�� �迭
        string[] art = {"https://i.ibb.co/hyX44r9/flower.jpg", "https://i.ibb.co/Wtm6xfZ/cute-cat.jpg", "https://i.ibb.co/BL1VJQw/image.jpg",
            "https://i.ibb.co/7JQ4FM7/red-flower.jpg", "https://i.ibb.co/6wz86xj/purple-flower.jpg", "https://i.ibb.co/jTJzz8G/green-flower.jpg"};
              //    artURL[i] = //�������� �޾ƿ��� (�迭�� ��)

            for (int i=0; i<frame.Length; i++)
        {
            StartCoroutine(DownloadImage(frame[i], art[i]));
//            StartCoroutine(DownloadImage(frame[i], "https://i.ibb.co/hyX44r9/flower.jpg"));
        }
 //       MaterialChange();
        //StartCoroutine(ImageUpload());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator DownloadImage(GameObject Frame, string MediaUrl)          //�������� �׸� �޾ƿͼ� art�� �ֱ�
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
        {
            //���� �� texture�� �޾ƿ� �̹����� ����
            Texture artTmp = ((DownloadHandlerTexture)request.downloadHandler).texture;
            Material[] mt = Frame.GetComponent<Renderer>().materials;
            mt[1].SetTexture("_MainTex", artTmp);
            
        }
    }
    //IEnumerator ImageUpload()
    //{
    //    // �̹��� base64string���� ��ȯ
    //    FileInfo file = new FileInfo("{local_image_file_path}");
    //    byte[] byteTexture = File.ReadAllBytes(file.FullName);

    //    // �̹��� ���� post ��û
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
    //    //mt[0]�� ����Ʋ, mt[1]�� �׸�
        
    //    mt_left[1].SetTexture("_MainTex", art2);
    //    mt_right[1].SetTexture("_MainTex", art3);    
    //}

}
