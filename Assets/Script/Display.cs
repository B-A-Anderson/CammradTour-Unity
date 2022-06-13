using Dummiesman;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Display : MonoBehaviour
{

    [SerializeField]
    private RawImage rawImageBackground;
    [SerializeField]
    private AspectRatioFitter aspectRatioFit;

    private bool isCamAvailible;
    private WebCamTexture cameraTexture;

    public static Display scene2;

    //public ObjFromFile present;
    public TextMeshProUGUI textOut;
    string result;

    // Start is called before the first frame update
    void Start()
    {
        SetUpCamera();

        // The user authorized use of the storage.
        //present = new ObjFromFile();
        result = QRResultManager.QRResults;
        ObjFromFile.onStart(result);
        textOut.text = ObjFromFile.OutputPath(result);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCameraRender();
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageRead);
        }
    }

    private void Awake()
    {
        scene2 = this;
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
                cameraTexture = new WebCamTexture(device[i].name);
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
    }

    public void onClickChangeScene()
    {
        SceneManager.LoadScene("Scanner", LoadSceneMode.Single);
    }
}
