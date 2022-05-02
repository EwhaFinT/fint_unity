using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommunityFrame : MonoBehaviour
{
    public GameObject frame;
    public Texture art;
    // Start is called before the first frame update
    void Start()
    {
        Material[] mt = frame.GetComponent<Renderer>().materials;
        mt[1].SetTexture("_MainTex", art);
        Debug.Log("set frame img");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
