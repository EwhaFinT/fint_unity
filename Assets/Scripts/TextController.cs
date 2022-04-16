using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TextController : MonoBehaviour
{
    private TextMeshProUGUI textVariable;

    private void Awake()
    {
        textVariable = GetComponent<TextMeshProUGUI>();

        //json data 받아오기
        Detail detail = new Detail("artist", "information");
        //string jsonData = JsonUtility.ToJson(d);
        
        //json형태로 받아온 텍스트 클래스 형태로 받아오기
        //Detail detail = JsonUtility.FromJson<Detail>(jsonData);
        //text input box 내의 메시지 수정
        textVariable.SetText("<color=blue>artist: </color>" + detail.Artist + "\nd\nd\nd\nd\nd\nd\nd\nd\nd\nd" + "<color=blue>information: </color>" + detail.Information);
        //color
        textVariable.color = Color.black;
        //font size
        textVariable.fontSize = 30;
        //font style
        //textVariable.fontStyle = FontStyles.Bold;
    }
}


    class Detail{
        private string artist;
        private string information;
        public Detail() {}
        public Detail(string artist, string information) {
            this.artist = artist;
            this.information = information;
        }

        public string Artist{
            get{return artist;}
        }
        public string Information{
            get{return information;}
        }
    }
