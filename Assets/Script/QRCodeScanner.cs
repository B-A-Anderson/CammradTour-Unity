using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using ZXing;

public class QRCodeScanner : MonoBehaviour
{

    [SerializeField]
    private RawImage rawImageBackground;
    [SerializeField]
    private AspectRatioFitter aspectRatioFit;
    [SerializeField]
    private TextMeshProUGUI textOut;
    [SerializeField]
    private RectTransform scanZone;

    private bool isCamAvailible;
    private WebCamTexture cameraTexture;

    public static QRCodeScanner scene1;


    // Start is called before the first frame update
    void Start()
    {
        SetUpCamera();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCameraRender();
    }
    
    private void Awake()
    {
        scene1 = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void SetUpCamera()
    {
        WebCamDevice[] device = WebCamTexture.devices;

        if (device.Length == 0)
        {
            isCamAvailible = false;
            return;
        }

        for (int i = 0; i < device.Length; i++)
        {
            if (device[i].isFrontFacing == false)
            {
                cameraTexture = new WebCamTexture(device[i].name, (int)scanZone.rect.width, (int)scanZone.rect.height);
            }
        }

        cameraTexture.Play();
        rawImageBackground.texture = cameraTexture;
        isCamAvailible = true;
    }

    private void UpdateCameraRender()
    {
        if (!isCamAvailible)
        {
            return;
        }
        float ratio = (float)cameraTexture.width / (float)cameraTexture.height;
        aspectRatioFit.aspectRatio = ratio;

        int orientation = -cameraTexture.videoRotationAngle;
        rawImageBackground.rectTransform.localEulerAngles = new Vector3(0, 0, orientation);

        try
        {
            IBarcodeReader barcodeReader = new BarcodeReader();
            Result result = barcodeReader.Decode(cameraTexture.GetPixels32(), cameraTexture.width, cameraTexture.height);
            if (result != null)
            {
                textOut.text = result.Text;
                QRResultManager.QRResults = textOut.text;
            }
            else
            {
                //textOut.text = "Failed to read QR code";
            }
        }
        catch
        {
            textOut.text = "Failed in try";
        }
    }

    public void onClickChangeScene()
    {
        SceneManager.LoadScene("DisplayNoAR", LoadSceneMode.Single);
    }
}