using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtChange : MonoBehaviour
{
    public GameObject FrameCenter;
    public GameObject FrameLeft;
    public GameObject FrameRight;
    public Material[] mts;

    [Header("Art Img")]
    public Texture art1;
    public Texture art2;
    public Texture art3;
    // Start is called before the first frame update
    void Start()
    {
        GetArt();
        MaterialChange();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MaterialChange()
    {
        var childrenlist = GetComponentsInChildren<ArtClick>();
        Material[] mt_center = FrameCenter.GetComponent<Renderer>().materials;
        Material[] mt_left = FrameLeft.GetComponent<Renderer>().materials;
        Material[] mt_right = FrameRight.GetComponent<Renderer>().materials;
        //mt[0]은 액자틀, mt[1]은 그림
        mt_center[1].SetTexture("_MainTex", art1);
        mt_left[1].SetTexture("_MainTex", art2);
        mt_right[1].SetTexture("_MainTex", art3);    
    }

    public void GetArt()
    {
        //서버에서 그림 받아와서 art에 넣기
    }
}
