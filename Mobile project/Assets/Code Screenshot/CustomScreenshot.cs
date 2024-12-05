using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class CustomScreenshot : MonoBehaviour
{
    public string gameName = "Awesome Game";
    public RawImage showImag;
    private byte[] currentTexture;
    private string currentFilePath;

    public GameObject showImagePanel;
    public GameObject capturePanel;
    public GameObject savedImagePanel;

    public string ScreenshotName()
    {
        return $"{gameName}_{System.DateTime.Now:yyy-MM-dd_HH-mm-ss}.png";
    }

    public void Capture()
    {
        StartCoroutine(TakeScreenshot());
        capturePanel.SetActive(false);
        
    }
    
    private IEnumerator TakeScreenshot()
    {
        EnadleCaptureUI(false);
        yield return new WaitForEndOfFrame();
        int width = Screen.width;
        int height = Screen.height;
        Texture2D screenshot = new Texture2D(width, height, TextureFormat.RGB24, false);
        
        screenshot.ReadPixels(new Rect(0,0,width,height),0, 0);
        screenshot.Apply();
        
        currentFilePath = Path.Combine(Application.temporaryCachePath, "temp.img.png.jpg");
        currentTexture = screenshot.EncodeToJPG();
        File.WriteAllBytes(currentFilePath,currentTexture);
        ShowImge();
        capturePanel.SetActive(false);
        EnadleCaptureUI(true);
        //Tp avoid memory leaks
        Object.Destroy(screenshot);

    }

    public void ShowImge()
    {
        capturePanel.SetActive(false);
        int width = Screen.width;
        int height = Screen.height;
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
        tex.LoadImage(currentTexture);
        showImag.material.mainTexture = tex;
        showImagePanel.SetActive(true);
    }
    
    public void ShareImage()
    {
        new NativeShare().AddFile(currentFilePath).SetSubject("Subject goes here").SetText("Hello world!").SetUrl("https://githum.com/yasirkula/UnitNativeShare").SetCallback((result, shareTarget) => Debug.Log($"Share result: {result}, selected app: {shareTarget}")).Share();
    }

    public void EnadleCaptureUI(bool isactive)
    {
        capturePanel.SetActive(isactive);
    }

    public void capturePanelCod()
    {
        capturePanel.SetActive(false);
    }

    public void SaveToGallery()
    {
        NativeGallery.Permission permission = NativeGallery.SaveImageToGallery(currentFilePath,gameName,ScreenshotName(),
            (isSuccess, path) =>
            {
                Debug.Log($"Media save result: {isSuccess} {path}");
                if (isSuccess)
                {
                    savedImagePanel.SetActive(true);
                    #if UNITY_EDITOR
                    string editorFilePath = Path.Combine(Application.persistentDataPath, ScreenshotName());
                    File.WriteAllBytes(editorFilePath,currentTexture);
                    #endif
                }
            });
        Debug.Log("Permission result: " + permission);
    }
    
    
}
