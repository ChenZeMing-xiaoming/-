using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class quxianshengcheng : MonoBehaviour
{
    [SerializeField]
    AnimationCurve selfSetTweenLine;
    public Material Material;
    public string name;
    // Start is called before the first frame update
    void Start()
    {
     
    }
    [Range(0, 1)]
    public float cc;
    // Update is called once per frame
    void Update()
    {
        Debug.Log(selfSetTweenLine.Evaluate(cc));
        if (Input.GetKeyDown(KeyCode.C))
        {
            CreateCameraCaptureAndSaveLocal(512, 512, @"F:\testshader\Assets\bag", name);
        }
    }
    public void CreateCameraCaptureAndSaveLocal(  int width, int height, string path, string imageName)
    {
 

   

        // ���� Texture2D ����ȡͼ������
        Texture2D image = new Texture2D(width, height, TextureFormat.ARGB32, false);
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                image.SetPixel(i, j, new Color(selfSetTweenLine.Evaluate(i / (width * 1.0f)), 0, 0));
            }
        }
       // image.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        image.Apply();

  

        // ��鱣��·���Ƿ�Ϊ�ջ���Ч
        if (string.IsNullOrEmpty(path))
        {
            Debug.LogError("Invalid save path.");
            return;
        }

        // ����ļ��в����ڣ��򴴽��ļ���
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        Material.mainTexture = image;
        // ����ͼ�񵽱����ļ���

        byte[] bytes = image.EncodeToJPG();
        if (bytes != null)
        {
            string savePath = Path.Combine(path, imageName + ".jpg");

            try
            {
                File.Delete(savePath);
                File.WriteAllBytes(savePath, bytes);
                Debug.Log("Image saved successfully: " + savePath);
            }
            catch (Exception e)
            {
                Debug.LogError("Error saving image: " + e.Message);
            }
        }
        else
        {
            Debug.LogError("Failed to encode image to JPG.");
        }
        UnityEditor.AssetDatabase.Refresh();
    }

}
