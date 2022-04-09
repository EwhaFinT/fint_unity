using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportPanel : MonoBehaviour
{
    public GameObject teleportPanel;
    public Vector3 targetPos_mainHall;
    public Vector3 targetPos_myRoom;
    public Vector3 targetPos_community;
    public Button btn1, btn2, btn3, btn_close;

    // Start is called before the first frame update
    private void Start()
    {
        btn1.onClick.AddListener(Onclicked_mainHall);       //mainHall buton
        btn2.onClick.AddListener(Onclicked_myroom);         //MyRoom button
        btn3.onClick.AddListener(Onclicked_Community);      //communityRoom button
        btn_close.onClick.AddListener(Onclicked_close);
        teleportPanel.SetActive(false);         //시작할 때 창 감추기
    }

    public void panelStart()
    {
        Debug.Log("show teleport panel");
        teleportPanel.SetActive(true);
    }

    public void Onclicked_mainHall()
    {
        Debug.Log("clicked mainhall btn");
        var player = GameObject.Find("Player").GetComponent<PlayerController>();

        if (player != null)
        {
            player.SetMove(targetPos_mainHall);
            teleportPanel.SetActive(false);
        }
    }

    public void Onclicked_myroom()
    {
        var player = GameObject.Find("Player").GetComponent<PlayerController>();

        if (player != null)
        {
            player.SetMove(targetPos_myRoom);
            teleportPanel.SetActive(false);
        }
    }

    public void Onclicked_Community()
    {
        var player = GameObject.Find("Player").GetComponent<PlayerController>();

        if (player != null)
        {
            player.SetMove(targetPos_community);
            teleportPanel.SetActive(false);
        }
    }
    public void Onclicked_close()
    {
        teleportPanel.SetActive(false);
    }
}
