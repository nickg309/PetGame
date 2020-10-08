using System.Collections;
//using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class PetMonster : MonoBehaviour
{

    #region Variables

    public MonsterClass myPetMonster = new MonsterClass();
    public AnimatorCodex myPetsAnimator = new AnimatorCodex();
    public RuntimeAnimatorController EggHatch;
    public GameObject gameHandler, food, drink, poo, rebornButton;
    public bool training, hungry, eating, idling, overFed, wokeUpThisSession = false, hungerCall = false, attacking;
    public DateTime hungerTimer, pooTimer;
    TimeSpan evolveTime;
    int overEatingCount = 0;
    bool refusing;
    Animator animator;


    #endregion

    private void Awake()
    {
        hungerTimer = DateTime.Now.AddMinutes(30);
        pooTimer = DateTime.Now.AddMinutes(15);
        animator = gameObject.GetComponent<Animator>();
        gameHandler.GetComponent<AccountContainer>().BedtimeCheck();
    }
    private void FixedUpdate()
    {
        if (myPetMonster.monFormID != 0)
        {
            gameHandler.GetComponent<AccountContainer>().BedtimeCheck();
        }

        if (myPetMonster.monSleeping == true && myPetMonster.monFormID != 0)
        {
            animator.SetBool("Sleeping", true);
            animator.SetBool("Crying", false);
            animator.SetBool("Eating", false);
            animator.SetBool("Idling", false);
        }

        if (myPetMonster.monSleeping != true && myPetMonster.monFormID != 0)
        {
            animator.SetBool("Sleeping", false);

            if (hungerTimer <= DateTime.Now && training == false)
            {
                hungerTimer = DateTime.Now.AddMinutes(30);
                LowerHunger();
            }
            if (pooTimer <= DateTime.Now && training == false)
            {
                pooTimer = DateTime.Now.AddMinutes(15);
                poo.GetComponent<Poo>().Poop();
            }

            if (food.activeInHierarchy == false && drink.activeInHierarchy == false)
            {
                eating = false;
            }
            if (hungry == false && eating == false && training == false)
            {
                if (myPetMonster.monHunger <= 3 && myPetMonster.monFormID > 0)
                {
                    idling = false;
                    hungry = true;
                    if (animator.GetBool("Idling") == true)
                    {
                        animator.SetBool("Idling", false);
                    }
                    if (animator.GetBool("Eating") == true)
                    {
                        animator.SetBool("Eating", false);
                    }
                    if (animator.GetBool("Crying") != true)
                    {
                        animator.SetBool("Crying", true);
                    }

                }
            }
            if (hungry == true && eating == false && training == false)
            {
                if (myPetMonster.monHunger > 3 && myPetMonster.monFormID > 0)
                {
                    idling = true;
                    hungry = false;
                    if (animator.GetBool("Crying") == true)
                    {
                        animator.SetBool("Crying", false);
                    }
                    if (animator.GetBool("Eating") == true)
                    {
                        animator.SetBool("Eating", false);
                    }
                    if (animator.GetBool("Idling") != true)
                    {
                        animator.SetBool("Idling", true);
                    }
                }
                else
                {
                    idling = false;
                    hungry = true;
                    if (animator.GetBool("Idling") == true)
                    {
                        animator.SetBool("Idling", false);
                    }
                    if (animator.GetBool("Eating") == true)
                    {
                        animator.SetBool("Eating", false);
                    }
                    if (animator.GetBool("Crying") != true)
                    {
                        animator.SetBool("Crying", true);
                    }

                }
            }
            if (eating == true)
            {
                if (animator.GetBool("Crying") == true)
                {
                    animator.SetBool("Crying", false);
                }
                if (animator.GetBool("Idling") == true)
                {
                    animator.SetBool("Idling", false);
                }
                if (animator.GetBool("Eating") != true)
                {
                    animator.SetBool("Eating", true);
                }
            }
            else
            {
                animator.SetBool("Eating", false);
                if(hungry != true)
                {
                    animator.SetBool("Idling", true);
                }
            }
            if (idling == true && attacking == false)
            {
                
                if (animator.GetBool("Crying") == true)
                {
                    animator.SetBool("Crying", false);
                }
            }
            if (eating != true && hungry != true && idling != true)
            {
                idling = true;
            }
            if(attacking == true)
            {
                if (animator.GetBool("Crying") == true)
                {
                    animator.SetBool("Crying", false);
                }
                if (animator.GetBool("Idling") == true)
                {
                    animator.SetBool("Idling", false);
                }
                if (animator.GetBool("Eating") == true)
                {
                    animator.SetBool("Eating", false);
                }
                animator.SetTrigger("Attack");
            }
        }

        if (myPetMonster.monHunger <= 0 && hungerCall == false)
        {
            myPetMonster.monHunger = 0;
            hungerCall = true;
            AddCry();
        }

        if(myPetMonster.monsterReborn == true && training == false)
        {
            rebornButton.SetActive(true);
        }
        else
        {
            rebornButton.SetActive(false);
        }

        if(myPetMonster.monEvolutionTime != "" && training == false && myPetMonster.monsterReborn == false)
        {
            evolveTime = DateTime.Now - Convert.ToDateTime(myPetMonster.monEvolutionTime);
            if(evolveTime.Hours >= 6 && myPetMonster.monFormID > 2 || myPetMonster.monFormID == 1 && evolveTime.Minutes >= 10 || myPetMonster.monFormID == 2 && evolveTime.Hours >= 1)
            {
                Evolution();
                myPetMonster.monEvolutionTime = DateTime.Now.ToString();
            }
        }
    }

    #region Functions

    void Evolution()
    {
        switch (myPetMonster.monFormID)
        {
            case 1:
                myPetMonster.monSkillPoints += 6;
                myPetMonster.monFormID = 2;
                break;
            case 2:
                myPetMonster.monSkillPoints += 6;
                if (myPetMonster.monCrys < 3 && myPetMonster.monOverFeeds == 0 && myPetMonster.monWakeUps == 0)
                {
                    myPetMonster.monFormID = 3;
                }
                else
                {
                    myPetMonster.monFormID = 4;
                }
                break;
            case 3:
                myPetMonster.monSkillPoints += 6;
                if (myPetMonster.monCrys < 3 && myPetMonster.monOverFeeds == 0 && myPetMonster.monWakeUps == 0)
                {
                    myPetMonster.monFormID = 5;
                }
                else
                {
                    myPetMonster.monFormID = 7;
                }
                break;
            case 4:
                myPetMonster.monSkillPoints += 6;
                if (myPetMonster.monCrys < 3 && myPetMonster.monOverFeeds == 0 && myPetMonster.monWakeUps == 0)
                {
                    myPetMonster.monFormID = 8;
                }
                else
                {
                    myPetMonster.monFormID = 6;
                }
                break;
            case 5:
                myPetMonster.monSkillPoints += 6;
                if (myPetMonster.monOverFeeds == 0 && myPetMonster.monWakeUps == 0)
                {
                    myPetMonster.monFormID = 9;
                }
                else
                {
                    myPetMonster.monFormID = 13;
                }
                break;
            case 6:
                myPetMonster.monSkillPoints += 6;
                if (myPetMonster.monOverFeeds > 2 && myPetMonster.monWakeUps == 0)
                {
                    myPetMonster.monFormID = 15;
                }
                else
                {
                    myPetMonster.monFormID = 11;
                }
                break;
            case 7:
                myPetMonster.monSkillPoints += 6;
                if (myPetMonster.monCrys < 3 && myPetMonster.monOverFeeds > 2 && myPetMonster.monWakeUps == 0)
                {
                    myPetMonster.monFormID = 14;
                }
                else
                {
                    myPetMonster.monFormID = 16;
                }
                break;
            case 8:
                myPetMonster.monSkillPoints += 6;
                if (myPetMonster.monCrys < 3 && myPetMonster.monOverFeeds == 0 && myPetMonster.monWakeUps > 0)
                {
                    myPetMonster.monFormID = 10;
                }
                else
                {
                    myPetMonster.monFormID = 12;
                }
                break;
            default:
                myPetMonster.monsterReborn = true;
                break;
        }
        myPetMonster.monCrys = 0;
        myPetMonster.monOverFeeds = 0;
        myPetMonster.monWakeUps = 0;
        FindCodex();
        UpdateMonSoul();
    }
    public void MonsterReborn()
    {
        float worldEntryPoint = myPetMonster.monWorld / 2;
        myPetMonster.monsterReborn = false;
        myPetMonster.monRebirths++;
        myPetMonster.monLevel = 0;
        myPetMonster.monCrys = 0;
        myPetMonster.monOverFeeds = 0;
        myPetMonster.monWakeUps = 0;
        myPetMonster.monFormID = 0;
        myPetMonster.monWorld = (int)(worldEntryPoint);
        FindCodex();
        StartCoroutine("Hatch");
    }
    public void AddCry()
    {
        myPetMonster.monCrys++;
    }
    public void AddPvpWin()
    {
        myPetMonster.monPvpWins++;
    }
    public void AddWakeUp()
    {
        myPetMonster.monWakeUps++;
    }
    public void AddPvpLoss()
    {
        myPetMonster.monPvpLoss++;
    }
    public void AddRebirth()
    {
        myPetMonster.monRebirths++;
    }
    public void ChangeLastWakeDate(string newDate)
    {
        if (wokeUpThisSession == false)
        {
            wokeUpThisSession = true;
        }
        myPetMonster.monLastWakeUp = newDate;
    }
    public void LowerHunger()
    {
        if (myPetMonster.monHunger > 0)
        {
            myPetMonster.monHunger--;
        }
    }
    public void LowerEnergy()
    {
        if (myPetMonster.monEnergy > 0)
        {
            myPetMonster.monEnergy--;
        }
    }
    public void LowerHungerTimer()
    {
        hungerTimer = hungerTimer.AddMinutes(-1.00);
    }
    public void LowerPooTimer()
    {
        pooTimer = pooTimer.AddMinutes(-1.00);
    }
    public void GetSoul()
    {
        Account myAccount = gameHandler.GetComponent<AccountContainer>().AccountReference();
        myPetMonster = myAccount.monSoul;
        FindCodex();
        if(myPetMonster.monFormID != 0)
        {
            DateTime logoutTime = Convert.ToDateTime(myAccount.LogoutTime);
            TimeSpan timeGone = logoutTime - DateTime.Now;
            switch (timeGone.Hours)
            {
                case 0:
                    poo.GetComponent<Poo>().Poop();
                    break;
                case 1:
                    myPetMonster.monHunger -= 1;
                    myPetMonster.monEnergy -= 1;
                    HungerEnergyCheck();
                    poo.GetComponent<Poo>().Poop();
                    break;
                case 2:
                    myPetMonster.monHunger -= 2;
                    myPetMonster.monEnergy -= 2;
                    HungerEnergyCheck();
                    poo.GetComponent<Poo>().Poop();
                    break;
                case 3:
                    myPetMonster.monHunger -= 3;
                    myPetMonster.monEnergy -= 3;
                    HungerEnergyCheck();
                    poo.GetComponent<Poo>().Poop();
                    poo.GetComponent<Poo>().Poop();
                    break;
                case 4:
                    myPetMonster.monHunger -= 4;
                    myPetMonster.monEnergy -= 4;
                    HungerEnergyCheck();
                    poo.GetComponent<Poo>().Poop();
                    poo.GetComponent<Poo>().Poop();
                    break;
                case 5:
                    myPetMonster.monHunger -= 5;
                    myPetMonster.monEnergy -= 5;
                    HungerEnergyCheck();
                    poo.GetComponent<Poo>().Poop();
                    poo.GetComponent<Poo>().Poop();
                    AddCry();
                    break;
                case 6:
                    myPetMonster.monHunger -= 6;
                    myPetMonster.monEnergy -= 6;
                    HungerEnergyCheck();
                    poo.GetComponent<Poo>().Poop();
                    poo.GetComponent<Poo>().Poop();
                    AddCry();
                    break;
                default:
                    myPetMonster.monHunger -= 6;
                    myPetMonster.monEnergy -= 6;
                    HungerEnergyCheck();
                    poo.GetComponent<Poo>().Poop();
                    poo.GetComponent<Poo>().Poop();
                    poo.GetComponent<Poo>().Poop();
                    AddCry();
                    AddCry();
                    break;
            }

        }
    }
    void HungerEnergyCheck()
    {
        if(myPetMonster.monHunger < 0)
        {
            myPetMonster.monHunger = 0;
        }
        if (myPetMonster.monEnergy < 0)
        {
            myPetMonster.monEnergy = 0;
        }
    }
    public void FindCodex()
    {
        foreach (AnimatorCodex codex in gameHandler.GetComponent<AnimControlLibrarian>().animatorLibrary)
        {
            if (codex.formNumber == myPetMonster.monFormID)
            {
                gameObject.GetComponent<Animator>().runtimeAnimatorController = codex.formAnimatorControler;
            }
        }
        if (myPetMonster.monFormID != 0)
        {
            if (animator.GetBool("Idling") != true)
            {
                animator.SetBool("Idling", true);
            }
        }
        else
        {
            StartCoroutine("Hatch");
        }
    }
    public void UpdateMonSoul()
    {
        gameHandler.GetComponent<AccountContainer>().PetUpdate(myPetMonster);
    }
    public void ToggleEating()
    {
        eating = false;
    }
    public void AttemptToFeed()
    {
        if (myPetMonster.monFormID != 0)
        {
            if (myPetMonster.monSleeping == true)
            {
                if (wokeUpThisSession == false)
                {
                    wokeUpThisSession = true;
                    gameHandler.GetComponent<AccountContainer>().WakeUp();
                }

                myPetMonster.monSleeping = false;
            }

            if (eating != true && refusing != true)
            {

                if (myPetMonster.monHunger == 6)
                {
                    if (overFed != true)
                    {
                        overEatingCount++;
                        AddExpForNourishment();
                        eating = true;
                        food.SetActive(true);
                        food.GetComponent<ConsumeItem>().StartAnimation();
                        LowerPooTimer();

                        if (overEatingCount >= 3)
                        {
                            myPetMonster.monOverFeeds++;
                            AddExpForNourishment();
                            overFed = true;
                        }
                    }
                    else
                    {
                        StartCoroutine("Refuse");
                    }
                }

                if (myPetMonster.monHunger < 6)
                {
                    if(hungerCall == true)
                    {
                        hungerCall = false;
                    }
                    myPetMonster.monHunger += 1;
                    AddExpForNourishment();
                    eating = true;
                    food.SetActive(true);
                    food.GetComponent<ConsumeItem>().StartAnimation();
                    LowerPooTimer();
                }

                if (myPetMonster.monHunger > 6)
                {
                    myPetMonster.monHunger = 6;
                }
            }
        }
    }
    public void AttemptToHydrate()
    {
        if (myPetMonster.monFormID != 0)
        {
            if (myPetMonster.monSleeping == true)
            {
                if (wokeUpThisSession == false)
                {
                    wokeUpThisSession = true;
                    gameHandler.GetComponent<AccountContainer>().WakeUp();
                }
                myPetMonster.monSleeping = false;
            }
            if (eating != true && refusing != true)
            {
                if (myPetMonster.monEnergy < 6)
                {
                    myPetMonster.monEnergy += 1;
                    AddExpForNourishment();
                    eating = true;
                    drink.SetActive(true);
                    drink.GetComponent<ConsumeItem>().StartAnimation();
                    LowerHungerTimer();
                }
                else
                {
                    StartCoroutine("Refuse");
                }
            }

        }
    }
    void AddExpForNourishment()
    {
        int expToAdd = (int)((myPetMonster.monLevel * 300) * 0.03f);
        AddExp(expToAdd);
    }
    public void TrainingToggle()
    {
        training = !training;
        if(training == true)
        {
            gameObject.GetComponentInChildren<Poo>().HidePoop();
        }
        else
        {
            gameObject.GetComponentInChildren<Poo>().RevealPoop();
        }
    }
    public void AddExp(int xp)
    {
        myPetMonster.monExp += xp;
        if (myPetMonster.monExp >= myPetMonster.monLevel*300)
        {
            LevelUp(myPetMonster.monLevel);
        }
    }
    public void LevelUp(int previousLevel)
    {
        myPetMonster.monLevel++;
        myPetMonster.monSkillPoints++;
        myPetMonster.monExp -= previousLevel*300;
        if(myPetMonster.monExp < 0)
        {
            myPetMonster.monExp = 0;
        }
    }
    public void WorldClimb()
    {
        myPetMonster.monWorld++;
        if(myPetMonster.monWorld > myPetMonster.monHighestWorld)
        {
            myPetMonster.monHighestWorld = myPetMonster.monWorld;
        }
        if(myPetMonster.monHighestWorld > myPetMonster.monWorldRecord)
        {
            myPetMonster.monWorldRecord = myPetMonster.monHighestWorld;
        }
    }
    public void AttackAnim()
    {
        attacking = true;
    }
    public void AttackAnimFalse()
    {
        attacking = false;
        animator.SetBool("Attack", false);
    }

    #endregion

    #region Coroutines

    IEnumerator Hatch() 
    {
        animator.runtimeAnimatorController = EggHatch;
        yield return new WaitForSeconds(10);
        myPetMonster.monLevel = 1;
        myPetMonster.monSkillPoints += 6;
        myPetMonster.monExp = 0;
        myPetMonster.monFormID = 1;
        if(myPetMonster.monPower == 0)
        {
            myPetMonster.monPower = 5;
            myPetMonster.monCriticalEffect = 5;
            myPetMonster.monSpeed = 5;
            myPetMonster.monStamina = 5;
        }
        if(myPetMonster.monBirthDay == "")
        {
            myPetMonster.monBirthDay = DateTime.Now.ToString();
        }
        myPetMonster.monEvolutionTime = DateTime.Now.ToString();
        FindCodex();
        UpdateMonSoul();
    }
    IEnumerator Refuse() 
    {
        refusing = true;
        gameObject.GetComponent<Image>().transform.rotation = new Quaternion(0, 180, 0, 0);
        yield return new WaitForSeconds(1);
        gameObject.GetComponent<Image>().transform.rotation = new Quaternion(0, 0, 0, 0);
        yield return new WaitForSeconds(1);
        gameObject.GetComponent<Image>().transform.rotation = new Quaternion(0, 180, 0, 0);
        yield return new WaitForSeconds(1);
        gameObject.GetComponent<Image>().transform.rotation = new Quaternion(0, 0, 0, 0);
        yield return new WaitForSeconds(1);
        refusing = false;
    }

    #endregion

}