using System;
//using System.Collections;
//using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Clock : MonoBehaviour
{

    #region Variables

    public Text timeDisplay, hours, mins, aMpM, CurrentBedtime;
    public GameObject dayTint, nightTint, am, pm, amBT, pmBT, gameHandler, currentAm, currentPm;
    int fixedHour;
    public int bedTimeHour, bedTimeMinuet;
    bool AM, ampmBedtime;

    #endregion

    private void Awake()
    {
        bedTimeHour = gameHandler.GetComponent<AccountContainer>().AccountReference().bedHour;
        bedTimeMinuet = gameHandler.GetComponent<AccountContainer>().AccountReference().bedMinuet;
        hours.text = gameHandler.GetComponent<AccountContainer>().AccountReference().bedHour.ToString("00");
        mins.text = gameHandler.GetComponent<AccountContainer>().AccountReference().bedMinuet.ToString("00");
        AM = gameHandler.GetComponent<AccountContainer>().AccountReference().ampm;

        if (AM == true)
        {
            amBT.SetActive(true);
            pmBT.SetActive(false);
        }
        else
        {
            amBT.SetActive(false);
            pmBT.SetActive(true);
        }

    }

    private void FixedUpdate()
    {
        UpdateClock();
    }

    #region Functions

    void UpdateClock() 
    {

        if (DateTime.Now.Hour >= 13)
        {
            fixedHour = DateTime.Now.Hour - 12;
        }
        else
        {
            fixedHour = DateTime.Now.Hour;
        }
        if(fixedHour != 0)
        {
            timeDisplay.text = fixedHour + ":" + DateTime.Now.Minute.ToString("00");
        }
        else
        {
            timeDisplay.text = 12 + ":" + DateTime.Now.Minute.ToString("00");
        }
        if (DateTime.Now.Hour <= 12)
        {
            pm.SetActive(false);
            am.SetActive(true);
        }
        else
        {
            pm.SetActive(true);
            am.SetActive(false); 
        }
        CurrentBedtime.text = (bedTimeHour.ToString() + ":" + bedTimeMinuet.ToString("00"));

        AM = gameHandler.GetComponent<AccountContainer>().AccountReference().ampm;

        if(AM == true)
        {
            currentAm.SetActive(true);
            currentPm.SetActive(false);
        }
        else
        {
            currentAm.SetActive(false);
            currentPm.SetActive(true);
        }
    }
    public void AmPmSwitch()
    {
        amBT.SetActive(!amBT.activeInHierarchy);
        pmBT.SetActive(!amBT.activeInHierarchy);
    }
    public void BedTimeIncreaseInputControlHour(Text target)
    {
        int intConversion;
        intConversion = int.Parse(target.text); 
        intConversion += 1;

        if(intConversion > 12)
        {
            intConversion = 1;
        }
        
        target.text = intConversion.ToString("00");
    }
    public void BedTimeDecreaseInputControlHour(Text target)
    {
        int intConversion;
        intConversion = int.Parse(target.text);
        intConversion -= 1;

        if (intConversion < 1)
        {
            intConversion = 12;
        }

        target.text = intConversion.ToString("00");
    }
    public void BedTimeIncreaseInputControlMin(Text target)
    {
        int intConversion;
        intConversion = int.Parse(target.text);
        intConversion += 5;

        if (intConversion >= 60)
        {
            intConversion = 0;
        }

        target.text = intConversion.ToString("00");
    }
    public void BedTimeDecreaseInputControlMin(Text target)
    {
        int intConversion;
        intConversion = int.Parse(target.text);
        intConversion -= 5;

        if (intConversion < 0)
        {
            intConversion = 55;
        }

        target.text = intConversion.ToString("00");
    }
    public void SaveNewBedtime()
    {
        bedTimeHour = int.Parse(hours.text);
        bedTimeMinuet = int.Parse(mins.text);
        if(amBT.activeInHierarchy == true)
        {
            ampmBedtime = true;
        }
        else
        {
            ampmBedtime = false;
        }
        gameHandler.GetComponent<AccountContainer>().ChangeBedTime(bedTimeHour, bedTimeMinuet, ampmBedtime);
    }

    #endregion

}