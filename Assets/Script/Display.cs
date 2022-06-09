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

    public ObjFromFile present;
    public TextMeshProUGUI textOut;
    string result;

    internal void PermissionCallbacks_PermissionDeniedAndDontAskAgain(string permissionName)
    {
        //Debug.Log($"{permissionName} PermissionDeniedAndDontAskAgain");
    }

    internal void PermissionCallbacks_PermissionGranted(string permissionName)
    {
        //Debug.Log($"{permissionName} PermissionCallbacks_PermissionGranted");
    }

    internal void PermissionCallbacks_PermissionDenied(string permissionName)
    {
        //Debug.Log($"{permissionName} PermissionCallbacks_PermissionDenied");
    }

    // Start is called before the first frame update
    void Start()
    {
        SetUpCamera();

        if (Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead))
        {
            // The user authorized use of the storage.
            present = new ObjFromFile();
            result = QRCodeScanner.scene1.QRResults;
            present.onStart(result);
            textOut.text = present.OutputPath(result);
        }
        else
        {
            bool useCallbacks = false;
            if (!useCallbacks)
            {
                // We do not have permission to use the storage.
                // Ask for permission or proceed without the functionality enabled.
                Permission.RequestUserPermission(Permission.ExternalStorageRead);
            }
            else
            {
                var callbacks = new PermissionCallbacks();
                callbacks.PermissionDenied += PermissionCallbacks_PermissionDenied;
                callbacks.PermissionGranted += PermissionCallbacks_PermissionGranted;
                callbacks.PermissionDeniedAndDontAskAgain += PermissionCallbacks_PermissionDeniedAndDontAskAgain;
                Permission.RequestUserPermission(Permission.ExternalStorageRead, callbacks);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCameraRender();
    }

    private void Awake()
    {
        scene2 = this;
        DontDestroyOnLoad(this.gameObject);
        result = QRCodeScanner.scene1.QRResults;
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
        present.deleteModel();
        SceneManager.LoadScene("Scanner", LoadSceneMode.Single);
    }
}
