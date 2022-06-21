using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorManager : MonoBehaviour
{
    public static void CreatingTextFile()
    {
        var textFile = Application.persistentDataPath + "/Errors.txt";
        if(File.Exists(textFile))
        {
           File.Delete(textFile);
        }

        using (StreamWriter sw = File.CreateText(textFile))
        {
            sw.WriteLine("Debugging CAMMRADTour");
        }	
    }

    public static void WirteInFile(string errors)
    {
        var textFile = Application.persistentDataPath + "/Errors.txt";
        if(File.Exists(textFile))
        {
            using (StreamWriter sw = File.AppendText(textFile))
            {
                sw.WriteLine(errors);
            }	
        }
        
    }
}
