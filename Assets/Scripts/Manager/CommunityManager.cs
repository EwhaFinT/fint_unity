using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class CommunityManager : MonoBehaviour
{
    public string CommunityID;
    public Dictionary<string, string> nftaddress = new Dictionary<string, string>();
    // Start is called before the first frame update

    public GameObject frame;
    public string paint_url;
    public Texture art;
    #region Singleton
    public static CommunityManager Instance;

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

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetFrameArt()
    {
        GetPaint();
        StartCoroutine(DownloadImage(paint_url));
    }

    public void GetPaint()
    {
        paint_url = nftaddress[CommunityID];
    }

    IEnumerator DownloadImage(string paint_url)          //서버에서 그림 받아와서 art에 넣기
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(paint_url);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
        {
            art = ((DownloadHandlerTexture)request.downloadHandler).texture;
            Material[] mt = frame.GetComponent<Renderer>().materials;
            mt[1].SetTexture("_MainTex", art);
        }
    }

    public Texture PostPaint()
    {
        return art;
    }
}
