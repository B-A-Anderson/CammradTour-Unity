using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using UnityEngine;

public class ErrorManager : MonoBehaviour
{
    public static string errorsText = @"";

    public static void CreatingTextFile()
    {
        var textFile = Application.persistentDataPath + "/Errors.txt";
        if(!File.Exists(textFile))
        {
           File.WriteAllText(textFile, "Debugging: \n\n");
        }
    }

    public static void WirteInFile(string errors)
    {
        var textFile = Application.persistentDataPath + "/Errors.txt";
        if(File.Exists(textFile))
        {
            File.AppendAllText(textFile, errors + "\t" + System.DateTime.Now + "\n");
        }
    }

    public static void SendingEmail()
    {
        SmtpClient client = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential()
            {
                UserName = "UzoTestThing@gmail.com",
                Password = "testing1?"
            }
        };

        MailAddress FromEmail = new MailAddress("UzoTestThing@gmail.com");
        MailAddress ToEmail = new MailAddress("anaeleu0629@students.bowiestate.edu");
        MailMessage message = new MailMessage()
        {
            From = FromEmail,
            Subject = "Testing SMTP",
            Body = "Hi, Just testing this thing"
        };
        message.To.Add(ToEmail);

        client.SendCompleted += Client_SendCompleted;
        client.SendMailAsync(message);
            
    }

    private static void Client_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
    {
        if(e.Error != null)
        {
            //Console.WriteLine("Failed sending: {0}", e.Error.Message);
            return;
        }
        //Console.WriteLine("Sent Successfully");

        throw new NotImplementedException();
    }
}
