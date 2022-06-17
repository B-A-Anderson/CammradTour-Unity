using Dummiesman;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ObjFromFile : MonoBehaviour
{
    static string objPath = string.Empty;
    string error = string.Empty;
    GameObject loadedObject;
    public GameObject Gparent;

    //public TextMeshProUGUI textOut;
    //string result = QRCodeScanner.scene1.QRResults;

    void Start() {
        
        objPath = Findfile(QRResultManager.QRResults);
        //textOut.text = objPath;

        //file path
        if (!File.Exists(objPath))
        {
            error = "File doesn't exist.";
        }else{
            if(loadedObject != null)            
                Destroy(loadedObject);
                
            loadedObject = new OBJLoader().Load(objPath);
            error = string.Empty;
            loadedObject.transform.parent = Gparent.transform;
            loadedObject.transform.localPosition = new Vector3(0, -5, 0);
        }
        
        /*
        if(!string.IsNullOrWhiteSpace(error))
        {
            GUI.color = Color.red;
            GUI.Box(new Rect(0, 64, 256 + 64, 32), error);
            GUI.color = Color.white;
        }*/
    }

    void Update()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageRead);
        }
    }

    public static string OutputPath()
    {
        return Findfile(QRResultManager.QRResults);
    }

    public void deleteModel()
    {
        Destroy(loadedObject);
        loadedObject = null;
    }
    

    private static string Findfile(string QRResults)
    {
        //string directory = @"\storage\emulated\0\Download";
        var directory = Application.persistentDataPath;
        int compare;
        bool inFile = false;
        string target = "", all = directory + "\\" + QRResults;
        string path;

        foreach (string file in Directory.EnumerateFiles(directory, "*.obj"))
        {
            compare = ObjFromFile.stringCompare(file, all);
            if(compare == 4){
                target = file;
                inFile = true;
                break;
            }
        }

        if(inFile)
        {
            path = target;
        }
        else
            path = "No Path";
        
        return path;
    }

    private static int stringCompare(string str1, string str2)
    {

        int l1 = str1.Length;
        int l2 = str2.Length;
        int lmin = l1 - l2;

        for (int i = 0; i < lmin; i++)
        {
            int str1_ch = str1[i];
            int str2_ch = str2[i];

            if (str1_ch != str2_ch)
            {
                return str1_ch - str2_ch;
            }
        }

        if (l1 != l2)
        {
            return l1 - l2;
        }
        else
        {
            return 0;
        }
    }
}
