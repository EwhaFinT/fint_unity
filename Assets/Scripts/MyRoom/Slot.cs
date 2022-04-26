using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    GameObject[] framelist;
    public GameObject frame;
    public Button slot;
    public RawImage img;
    public Texture art;
    void Start()
    {
//        framelist = GameObject.FindGameObjectsWithTag("MyRoomFrame");      //myroom 내 frame의 배열       
        slot.onClick.AddListener(OnClick_Slot);
    }

    public void OnClick_Slot()
    {
        //if ("Slot".Equals(gameObject.name))
        //{

        //}
        //else
        //{
            
        //}
        frame = GameObject.Find("MyRoom_Frame1");
        art = img.texture;
        
        Material[] mt = frame.GetComponent<Renderer>().materials;
        mt[1].SetTexture("_MainTex", art);
        Debug.Log("set frame img");
        frame.SetActive(true);
    }
}
