using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ImageLoader : MonoBehaviour
{
    //    // 이미지 UI 최대 크기
    //    private float maxWidth = 60;
    //    private float maxHeight = 60;

    //    public void OnLoad(Image imageDrawTexture, FileInfo file)
    //    {
    //        byte[] byteTexture = File.ReadAllBytes(file.FullName);  // 파일로부터 Bytes 데이터 불러오기

    //        // Texture2D 이미지 파일 데이터 생성
    //        Texture2D texture2D = new Texture2D(0, 0);

    //        if (byteTexture.Length > 0)
    //        {
    //            texture2D.LoadImage(byteTexture);
    //        }

    //        // 이미지 크기 조절
    //        if (texture2D.width > maxWidth && texture2D.height > maxHeight)
    //        {
    //            bool width = (texture2D.width > texture2D.height) ? true : false;
    //            if (width)
    //            {
    //                float ratio = maxWidth / texture2D.width;
    //                imageDrawTexture.rectTransform.sizeDelta = new Vector2(texture2D.width * ratio, texture2D.width * ratio / texture2D.width * texture2D.height);

    //            }
    //            else
    //            {
    //                float ratio = maxHeight / texture2D.height;
    //                imageDrawTexture.rectTransform.sizeDelta = new Vector2(texture2D.height * ratio / texture2D.height * texture2D.width, texture2D.height * ratio);
    //            }

    //        }
    //        else if (texture2D.width > maxWidth)
    //        {
    //            imageDrawTexture.rectTransform.sizeDelta = new Vector2(maxWidth, maxWidth / texture2D.width * texture2D.height);
    //        }
    //        else if (texture2D.height > maxHeight)
    //        {
    //            imageDrawTexture.rectTransform.sizeDelta = new Vector2(maxHeight / texture2D.height * texture2D.width, maxHeight);
    //        }
    //        else
    //        {
    //            imageDrawTexture.rectTransform.sizeDelta = new Vector2(texture2D.width, texture2D.height);
    //        }

    //        // Texture2D -> Sprite 변환
    //        Sprite sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
    //        imageDrawTexture.sprite = sprite;
    //    }
}