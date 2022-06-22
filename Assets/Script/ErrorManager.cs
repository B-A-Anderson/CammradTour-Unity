using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using UnityEngine;

public class ErrorManager : MonoBehaviour
{
    public static string errorsText = @"";

    public static void CreatingTextFile()
    {
        var textFile = Application.persistentDataPath + "/Errors.txt";
        if(File.Exists(textFile))
        {
           File.Delete(textFile);
        }

        using (StreamWriter sw = File.CreateText(textFile))
        {
            sw.WriteLine("Debugging CAMMRADTour:");
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

    public static void SendingEmail()
    {
        string to = "anaeleu0629@students.bowiestate.edu";
        string from = "uanaele34@gmail.com";
        MailMessage message = new MailMessage(from, to);
        message.Subject = "Error In Uzo CAMMRADTour";
        message.Body = errorsText;
        SmtpClient client = new SmtpClient("smtp.gmail.com");
        // Credentials are necessary if the server requires the client
        // to authenticate before it will send email on the client's behalf.
        client.UseDefaultCredentials = true;

        client.Send(message);
    }
}
