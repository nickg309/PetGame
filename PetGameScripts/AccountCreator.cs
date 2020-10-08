using UnityEngine.UI;
using UnityEngine;

public class AccountCreator : MonoBehaviour
{
    #region Variables

    public GameObject CreationPanel, NameTakenText;
    string chosenName, chosenpPass;
    public InputField _UAN, _UAP;

    #endregion

    #region Functions

    public void InitializeAccount()
    {
        if (NameTakenText.activeInHierarchy == true)
        {
            NameTakenText.SetActive(false);
        }

        chosenName = _UAN.text;
        chosenpPass = _UAP.text;

        if (chosenName.Length > 18 || chosenName.Length == 0)
        {
            NameTakenText.SetActive(true);
            return;
        }

        bool isNameTaken = WebServer.NameUsedCheck(chosenName);

        if (isNameTaken == true)
        {
            NameTakenText.SetActive(true);
            return;
        }
        else
        {
            NameTakenText.SetActive(false);
        }

        Account newAccount = new Account();

        newAccount.accountName = chosenName;
        newAccount.accountPassword = chosenpPass;
        newAccount.ampm = false;
        newAccount.AMPMstring = "PM";
        newAccount.bedHour = 10;
        newAccount.bedMinuet = 0;

        WebServer.AddNewAccount(newAccount);
        
        CreationPanel.SetActive(false);
    }

    #endregion
}