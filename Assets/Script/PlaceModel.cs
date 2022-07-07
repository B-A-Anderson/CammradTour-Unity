using Dummiesman;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class PlaceModel : MonoBehaviour
{
    static string objPath = string.Empty;
    //static string MtlPath = string.Empty;
    GameObject loadedObject;

    public GameObject Gparent;
    public TextMeshProUGUI Path;
    public TextMeshProUGUI errorMessages;

    void Start()
    {
        objPath = OutputPath();
        //MtlPath = OutputMtlPath();
        Path.text = OutputPath();
        ErrorManager.CreatingTextFile();
        DisplayModel();
    }

    void DisplayModel() {

        //file path
        if (!File.Exists(objPath))
        {
            errorMessages.text = "File doesn't exist";
            ErrorManager.WirteInFile("File doesn't exist\n");
        }
        else{
            if(loadedObject != null)
            {
                Destroy(loadedObject);
            }
            errorMessages.text = "Should have loaded the object";
            ErrorManager.WirteInFile("Should have loaded the object\n");

            //Loading 3D Model
            //loadedObject = new OBJLoader().Load(objPath);
            OBJLoader obj = new OBJLoader();

            errorMessages.text = "made the class object";
            ErrorManager.WirteInFile("made the class object\n");

            loadedObject = obj.Load(objPath);

            errorMessages.text = "Passed the Loading";
            ErrorManager.WirteInFile("Passed the Loading\n");

            //parenting
            loadedObject.transform.parent = Gparent.transform;
            loadedObject.transform.localPosition = new Vector3(0, -5, 0);

            errorMessages.text = "Passed the parenting part";
            ErrorManager.WirteInFile("Passed the parenting part\n");

            //end
            errorMessages.text = "No Errors";
            ErrorManager.WirteInFile("No Errors\n");
        }
    }

    public static string OutputPath()
    {
        return Findfile(QRResultManager.QRResults);
    }

    public static string OutputMtlPath()
    {
        return FindMtl(QRResultManager.QRResults);
    }

    private static string Findfile(string Key)
    {
        //string directory = @"\storage\emulated\0\Download";
        var directory = Application.persistentDataPath;
        int compare;
        bool inFile = false;
        string target = "", all = directory + "/" + Key;
        string path;

        foreach (string file in Directory.EnumerateFiles(directory, "*.obj"))
        {
            compare = stringCompare(file, all);
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
        { 
            path = "No Path";
            ErrorManager.WirteInFile("obj path was not found\n");
        }
        
        return path;
    }

    private static string FindMtl(string Key)
    {
        //string directory = @"\storage\emulated\0\Download";
        var directory = Application.persistentDataPath;
        int compare;
        bool inFile = false;
        string target = "", all = directory + "/" + Key;
        string path;

        foreach (string file in Directory.EnumerateFiles(directory, "*.mtl"))
        {
            compare = stringCompare(file, all);
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
        { 
            path = null;
            ErrorManager.WirteInFile("Mtl path was not found\n");
        }
        
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
