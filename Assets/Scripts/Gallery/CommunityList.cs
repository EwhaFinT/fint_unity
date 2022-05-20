using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;

public class CommunityList : MonoBehaviour
{

    public Button btn_close;
    public GameObject listPrefab, content;
//    string userId;
    // Start is called before the first frame update
    void Start()
    {
//        userId = Manager.Instance.ID;
        btn_close.onClick.AddListener(panelClose);
    }

    public void panelStart()
    {
        gameObject.SetActive(true);
        StartCoroutine(LoadCommunityList());
    }

    public void panelClose()
    {
        gameObject.SetActive(false);
    }

    void ListInit(CommnunityResponse response)
    {
        //content Initialization
        if (content.transform.childCount > 1)
        {
            for (int i = 1; i < content.transform.childCount; i++)
            {
                var a = content.transform.GetChild(i).gameObject;
                Destroy(a);
            }
            Debug.Log("Destory CommunityList clone all");
        }

        for (int i = 0; i < response.communityList.Count; i++)
        {
            GameObject community = Instantiate(listPrefab);
            community.transform.SetParent(content.transform, false);

            var community_btn = community.GetComponent<CommunityButton>();
            community_btn.GetCommunityInfo(response.communityList[i].communityId, response.communityList[i].artName);
        }
    }

    IEnumerator LoadCommunityList()
    {

        // api 烹脚 何盒 矫累

        string url = Manager.Instance.url + "v1/communities?userId="+ Manager.Instance.ID;
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        string jsonString = www.downloadHandler.text;
        //        var response = JsonUtility.FromJson<CommnunityResponse>(jsonString);
        var response = JsonUtility.FromJson<CommnunityResponse>("{\"communityList\":" + jsonString + "}");
        // api 烹脚 何盒 场
        ListInit(response);
    }
}

[Serializable]
class communityListResponse
{
    public string communityId;
    public string artName;
}

[Serializable]
class CommnunityResponse
{
    public List<communityListResponse> communityList;
}


