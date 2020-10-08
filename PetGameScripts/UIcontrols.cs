//using System.Collections;
//using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class UIcontrols : MonoBehaviour
{
    #region Variables
    public GameObject pet, dayTint, nightTint;
    public InputField newOpponent;
    MonsterClass opponentMonster = new MonsterClass();
    #endregion

    private void FixedUpdate()
    {
        if(DateTime.Now.ToString("tt") == "AM" && dayTint.activeInHierarchy == false)
        {
            dayTint.SetActive(true);
            nightTint.SetActive(false);
        }
        if(DateTime.Now.ToString("tt") == "PM" && nightTint.activeInHierarchy == false)
        {
            dayTint.SetActive(false);
            nightTint.SetActive(true);
        }

    }

    #region Functions

    public void OpenWindow(GameObject window)
    {
        window.SetActive(!window.activeInHierarchy);
    }
    public void CloseWindow(GameObject window)
    {
        window.SetActive(false);
    }
    public void OpenWindowIfNotEgg(GameObject window)
    {
        if (pet.GetComponent<PetMonster>().myPetMonster.monFormID != 0)
        {
            window.SetActive(!window.activeInHierarchy);
        }
    }
    public void OpenWindowIfPetIsOK(GameObject window)
    {
        if(pet.GetComponent<PetMonster>().myPetMonster.monSleeping != true && pet.GetComponent<PetMonster>().eating != true && pet.GetComponent<PetMonster>().hungry != true && pet.GetComponent<PetMonster>().myPetMonster.monFormID != 0)
        {
            window.SetActive(!window.activeInHierarchy);
        }
    }
    public void FeedPet()
    {
        pet.GetComponent<PetMonster>().AttemptToFeed();
    }
    public void HydratePet()
    {
        pet.GetComponent<PetMonster>().AttemptToHydrate();
    }
    public void InputOpponent()
    {

        if(newOpponent.text.ToString() != AccountContainer.myAccount.accountName.ToString() && newOpponent.text.ToString().Length  != 0 && newOpponent.text.ToString().Length <= 12)
        {
            opponentMonster = WebServer.SearchForOpponent(newOpponent.text.ToString());
        }
        else
        {
            Debug.Log("name is too long or too short.");
            return;
        }
        
        if(opponentMonster != null)
        {
            gameObject.GetComponent<Arena>().CopyOpponent(opponentMonster, newOpponent.text.ToString());
        }
        else
        {
            gameObject.GetComponent<Arena>().NotFound();
        }
    }

    #endregion

}