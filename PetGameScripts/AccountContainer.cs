using System;
using UnityEngine;

public class AccountContainer : MonoBehaviour
{
    #region Variables
    public GameObject pet_;
    static public Account myAccount, saveTarget;
    public DateTime currentTime = DateTime.Now;
    public DateTime bedTime;

    #endregion
    private void OnApplicationPause(bool pause)
    {
        if(pause == true)
        {
            if(myAccount != null)
            {
            myAccount.LogoutTime = DateTime.Now.ToString();
            SaveAccount();
            }
        }
    }
    private void OnApplicationQuit()
    {
        if (myAccount != null)
        {
            myAccount.LogoutTime = DateTime.Now.ToString();
            SaveAccount();
        }
    }

    #region Functions

    public void BedtimeCheck()
    {
        DateTime now = DateTime.Now;
        string parseH = now.ToString("HH");
        string parseM = now.ToString("mm");
        int militaryBedTimeH = myAccount.bedHour;
        int nowToMilitaryTimeH = int.Parse(parseH);
        int militaryBedTimeM = myAccount.bedMinuet;
        int nowToMilitaryTimeM = int.Parse(parseM);

        if (pet_.GetComponent<PetMonster>().wokeUpThisSession == false && pet_.GetComponent<PetMonster>().myPetMonster.monLevel != 0)
        {
            if(pet_.GetComponent<PetMonster>().training != true)
            {
                if(myAccount.AMPMstring == "PM")
                {
                    militaryBedTimeH += 12;
                }
                if(militaryBedTimeH < nowToMilitaryTimeH)
                {
                    StillAsleepCheck(militaryBedTimeH, militaryBedTimeM, now);
                }
                if(militaryBedTimeH == nowToMilitaryTimeH)
                {
                    if (militaryBedTimeM <= nowToMilitaryTimeM)
                    {
                        StillAsleepCheck(militaryBedTimeH, militaryBedTimeM, now);
                    }
                    if (militaryBedTimeM > nowToMilitaryTimeM)
                    {
                        pet_.GetComponent<PetMonster>().myPetMonster.monSleeping = false;
                    }
                }
                if(militaryBedTimeH > nowToMilitaryTimeH)
                {
                    int past = nowToMilitaryTimeH - 6;

                    if(past < 0)
                    {
                        past += 24;
                    }
                    if(past < militaryBedTimeH)
                    {
                        StillAsleepCheck(militaryBedTimeH, militaryBedTimeM, now);
                    }
                    if(past == militaryBedTimeH)
                    {
                        if (militaryBedTimeM <= nowToMilitaryTimeM)
                        {
                            StillAsleepCheck(militaryBedTimeH, militaryBedTimeM, now);
                        }
                        if (militaryBedTimeM > nowToMilitaryTimeM)
                        {
                            pet_.GetComponent<PetMonster>().myPetMonster.monSleeping = false;
                        }
                    }
                }
            }
            else
            {
                return;
            }
        }
    }
    void StillAsleepCheck(int milH, int milM, DateTime now)
    {
        milH += 6;

        if(milH >= 24)
        {
            milH -= 24;
        }

        if(milH > now.Hour)
        {
            pet_.GetComponent<PetMonster>().myPetMonster.monSleeping = true;
        }
        if(milH == now.Hour)
        {
            if (milM >= now.Minute)
            {
                pet_.GetComponent<PetMonster>().myPetMonster.monSleeping = true;
            }
            else
            {
                pet_.GetComponent<PetMonster>().myPetMonster.monSleeping = false;
            }
        }
        if(milH < now.Hour)
        {

            pet_.GetComponent<PetMonster>().myPetMonster.monSleeping = false;

            /*int past = milH - 6;
            if(past < 0)
            {
                past += 24;
            }
            if(past < now.Hour)
            {
                pet_.GetComponent<PetMonster>().myPetMonster.monSleeping = true;

            }
            if(past == now.Hour)
            {
                if (milM <= now.Minute)
                {
                    pet_.GetComponent<PetMonster>().myPetMonster.monSleeping = true;
                }

                if (milM > now.Minute)
                {
                    pet_.GetComponent<PetMonster>().myPetMonster.monSleeping = false;
                }
            }*/
        }
    }
    public void WakeUp()
    {

        if(pet_.GetComponent<PetMonster>().myPetMonster.monLastWakeUp != DateTime.Now.ToString("MM/dd/yyyy"))
        { 
            Debug.Log("woke up");
            pet_.GetComponent<PetMonster>().ChangeLastWakeDate(DateTime.Now.ToString("MM/dd/yyyy"));
            if(pet_.GetComponent<PetMonster>().myPetMonster.monSleeping == true)
            {
                pet_.GetComponent<PetMonster>().myPetMonster.monSleeping = false;
            }
            pet_.GetComponent<PetMonster>().AddWakeUp();
        }
    }
    public void PetUpdate(MonsterClass pet)
    {
        myAccount.monSoul = pet;
        SaveAccount();
    }
    public void HoldAccount(Account account)
    {
        myAccount = account;
    }
    public Account AccountReference()
    {
        return myAccount;
    }
    public void ChangeBedTime(int hour, int minuet, bool newampm)
    {
        if (pet_.GetComponent<PetMonster>().myPetMonster.monSleeping == true)
        {
            WakeUp();
        }

        myAccount.bedHour = hour;
        myAccount.bedMinuet = minuet;
        myAccount.ampm = newampm;
        if (myAccount.ampm == true)
        {
            myAccount.AMPMstring = "AM";
        }
        else
        {
            myAccount.AMPMstring = "PM";
        }

        BedtimeCheck();
        SaveAccount();
    }
    public void CloseGame()
    {
        SaveAccount();
        Application.Quit();
    }
    public void SaveAccount()
    {
        WebServer.SaveAccount(myAccount);


        /*if (File.Exists(path))
        {
            string contents = File.ReadAllText(path);
            ObjWrapper wrapper = JsonUtility.FromJson<ObjWrapper>(contents);
            gameData = wrapper.gameData;
            foreach (Account acc in gameData.accounts)
            {
                if (myAccount.id == acc.id)
                {
                    saveTarget = acc;
                }
            }

            //update the target with new data.
            saveTarget.monSoul = myAccount.monSoul; 
            saveTarget.bedHour = myAccount.bedHour;
            saveTarget.bedMinuet = myAccount.bedMinuet;
            saveTarget.ampm = myAccount.ampm;
            saveTarget.AMPMstring = myAccount.AMPMstring;
            saveTarget.LogoutTime = myAccount.LogoutTime;
            //Save data.
            ObjWrapper saveWrapper = new ObjWrapper();
            saveWrapper.gameData = gameData;
            string saveContents = JsonUtility.ToJson(saveWrapper, true);
            File.WriteAllText(path, saveContents);
        }*/
    }

    #endregion

}