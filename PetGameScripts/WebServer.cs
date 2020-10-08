using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using UnityEngine;

public static class WebServer
{
    private static readonly HttpClient client = new HttpClient();
    private static string Ws = "https://peauja7tqg.execute-api.us-west-1.amazonaws.com/beta/";

    public static async void AddNewAccount(Account NewAccount)
    {

        var json = JsonUtility.ToJson(NewAccount);
        var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
        var result = await client.PutAsync(Ws + NewAccount.accountName + ".json", content);
    }
    public static bool NameUsedCheck(string nameToCheck)
    {
        string path = Ws + nameToCheck + ".json";
        bool check = RemoteFileExists(path);
        return check;
    }
    public static string CheckName(string uri)
    {
        bool RFEcheck = new bool();
        RFEcheck = RemoteFileExists(uri);

        if (RFEcheck == true)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
        else
        {
            return "Failure to Aquire File";
        }

    }
    public static bool RemoteFileExists(string url)
    {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                string answer = reader.ReadToEnd();
            if(answer.Contains("<") == true)
            {
                return false;
            }
            else
            {
                return true;
            }
            }
    }
    public static Account AccountLogIn(string _name, string _pass)
    {
        string path = Ws + _name + ".json";
        string check = CheckName(path);
        if(check != "Failure to Aquire File")
        {
            Account AttemptedAccount = Account.CreateFromJSON(check);
            if (AttemptedAccount.accountPassword == _pass)
            {
                return AttemptedAccount;
            }
            else
            {
                return null;
            }
            
        }
        else
        {
            return null;
        }

    }
    public static async void SaveAccount(Account saveTarget)
    {
        string path = Ws + saveTarget.accountName + ".json";
        string check = CheckName(path);
        if (check == "Failure to Aquire File")
        {
            Debug.Log("Sorry, we couldn't find that account.");
            return;
        }
        var json = JsonUtility.ToJson(saveTarget);
        var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
        var response = await client.PutAsync(Ws+saveTarget.accountName + ".json", content);
    }
    public static MonsterClass SearchForOpponent(string opponent)
    {
        string opponentPath = (Ws + opponent + ".json");
        string stringJson = CheckName(opponentPath);
        if(stringJson == "Failure to Aquire File")
        {
            return null;
        }

        Account jsonObj = new Account();
        MonsterClass opponentMon = new MonsterClass();
        jsonObj =Account.CreateFromJSON(stringJson);
        opponentMon = jsonObj.monSoul;

        return opponentMon;
    }
}