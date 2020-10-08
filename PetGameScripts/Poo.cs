/*using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;*/
using UnityEngine;

public class Poo : MonoBehaviour
{
    public GameObject poo1, poo2, poo3;
    public GameObject pet;
    public int pooCount;
    bool criedAboutIt = false;

    public void Poop()
    {
        if (pet.GetComponent<PetMonster>().training != true)
        {
            if (pooCount < 3)
            {
                pooCount++;
                switch (pooCount)
                {
                    case 1:
                        poo1.SetActive(true);
                        pet.GetComponent<PetMonster>().LowerHungerTimer();
                        break;
                    case 2:
                        poo1.SetActive(false);
                        poo2.SetActive(true);
                        pet.GetComponent<PetMonster>().LowerHungerTimer();
                        break;
                    case 3:
                        poo1.SetActive(false);
                        poo2.SetActive(false);
                        poo3.SetActive(true);
                        pet.GetComponent<PetMonster>().LowerHungerTimer();
                        break;
                    default:
                        poo1.SetActive(false);
                        poo2.SetActive(false);
                        poo3.SetActive(true);
                        break;
                }
            }
            else
            {
                if (criedAboutIt != true)
                {
                    pet.GetComponent<PetMonster>().AddCry();
                    criedAboutIt = true;
                }
            }
        }
        else
        {
            if (pooCount < 3)
            {
                pooCount++;
                HidePoop();
            }
            else
            {
                if (criedAboutIt != true)
                {
                    pet.GetComponent<PetMonster>().AddCry();
                    criedAboutIt = true;
                }
            }
        }
    }
    public void CleanPoop()
    {
        pooCount = 0;
        criedAboutIt = false;
        poo2.SetActive(false);
        poo3.SetActive(false);
        poo1.SetActive(false);
    }
    public void HidePoop() 
    {
        if(poo1.activeInHierarchy == true)
        {
            poo1.SetActive(false);
        }
        if (poo2.activeInHierarchy == true)
        {
            poo2.SetActive(false);
        }
        if(poo3.activeInHierarchy == true)
        {
            poo3.SetActive(false);
        }
    }
    public void RevealPoop()
    {
        if (pooCount == 1 && poo1.activeInHierarchy == false)
        {
            poo1.SetActive(true);
        }
        if (pooCount == 2 && poo2.activeInHierarchy == false)
        {
            poo2.SetActive(true);
        }
        if (pooCount == 3 && poo3.activeInHierarchy == false)
        {
            poo3.SetActive(true);
        }
    }
}