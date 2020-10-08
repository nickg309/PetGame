using UnityEngine;
[System.Serializable]
public class Account
{
    public int id;
    public string accountName = "";
    public string accountPassword = "";
    public string AMPMstring = "";
    public string LogoutTime = "";
    public int bedHour;
    public int bedMinuet;
    public bool ampm;
    public MonsterClass monSoul;

    public static Account CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<Account>(jsonString);
    }

}