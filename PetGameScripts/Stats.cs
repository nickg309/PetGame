//using System.Collections;
//using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class Stats : MonoBehaviour
{
    public Slider hunger, energy, exp;
    public Text level, formName, age, rebirths, world, wins, power, critEffect, speed, stam, skillPoints, score;
    public GameObject pet, star1, star2, star3, star4, star5;
    int potPower, potCritEffect, potSpeed, potStam, potSkillPoints, rank, TS;
    MonsterClass petUpdate = new MonsterClass();
    public void PopulateStats() 
    {
        if (pet.GetComponent<PetMonster>().myPetMonster.monFormID != 0)
        {
            FindAge();
            petUpdate = pet.GetComponent<PetMonster>().myPetMonster;

            potPower = petUpdate.monPower;
            potCritEffect = petUpdate.monCriticalEffect;
            potSpeed = petUpdate.monSpeed;
            potStam = petUpdate.monStamina;
            potSkillPoints = petUpdate.monSkillPoints;

            level.text = petUpdate.monLevel.ToString();
            formName.text = petUpdate.monFormName;
            age.text = petUpdate.monAge.ToString();
            rebirths.text = petUpdate.monRebirths.ToString();
            world.text = petUpdate.monWorldRecord.ToString();
            WinsPercent();
            skillPoints.text = petUpdate.monSkillPoints.ToString();
            power.text = potPower.ToString();
            critEffect.text = potCritEffect.ToString();
            speed.text = potSpeed.ToString();
            stam.text = potStam.ToString();
            hunger.maxValue = 6;
            hunger.value = petUpdate.monHunger;
            energy.maxValue = 6;
            energy.value = petUpdate.monEnergy;
            exp.maxValue = petUpdate.monLevel * 300;
            exp.value = petUpdate.monExp;

            StarCheck();
            TS = (petUpdate.monPower + petUpdate.monCriticalEffect + petUpdate.monSpeed + petUpdate.monStamina) * rank;
            score.text = TS.ToString();
        }
    }
    void StarCheck()
    {
        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);
        star4.SetActive(false);
        star5.SetActive(false);
        int id = pet.GetComponent<PetMonster>().myPetMonster.monFormID;
        switch (id)
        {
            case 1:
                star1.SetActive(true);
                rank = 1;
                break;
            case 2:
                star1.SetActive(true);
                star2.SetActive(true);
                rank = 2;

                break;
            case 3:
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
                rank = 3;

                break;
            case 4:
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
                rank = 3;

                break;
            case 5:
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
                star4.SetActive(true);
                rank = 4;

                break;
            case 6:
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
                star4.SetActive(true);
                rank = 4;

                break;
            case 7:
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
                star4.SetActive(true);
                rank = 4;

                break;
            case 8:
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
                star4.SetActive(true);
                rank = 4;

                break;
            case 9:
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
                star4.SetActive(true);
                star5.SetActive(true);
                rank = 5;

                break;
            case 10:
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
                star4.SetActive(true);
                star5.SetActive(true);
                rank = 5;

                break;
            case 11:
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
                star4.SetActive(true);
                star5.SetActive(true);
                rank = 5;

                break;
            case 12:
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
                star4.SetActive(true);
                star5.SetActive(true);
                rank = 5;

                break;
            case 13:
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
                star4.SetActive(true);
                star5.SetActive(true);
                rank = 5;

                break;
            case 14:
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
                star4.SetActive(true);
                star5.SetActive(true);
                rank = 5;

                break;
            case 15:
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
                star4.SetActive(true);
                star5.SetActive(true);
                rank = 5;

                break;
            default:
                star1.SetActive(true);
                rank = 1;
                break;
        }
    }
    void WinsPercent()
    {
        int myWins = pet.GetComponent<PetMonster>().myPetMonster.monPvpWins;
        int myLosses = pet.GetComponent<PetMonster>().myPetMonster.monPvpLoss;

        if (myWins != 0 && myLosses != 0)
        {
            wins.text = (myWins / myLosses).ToString("0.00");
        }
        else
        {
            if(myWins == 0)
            {
            wins.text = "0";
            }
            if(myLosses == 0)
            {
                wins.text = "100";

            }
        }
    }
    public void ApplyStatChanges()
    {
        pet.GetComponent<PetMonster>().myPetMonster.monPower = potPower;
        pet.GetComponent<PetMonster>().myPetMonster.monCriticalEffect = potCritEffect;
        pet.GetComponent<PetMonster>().myPetMonster.monSpeed = potSpeed;
        pet.GetComponent<PetMonster>().myPetMonster.monStamina = potStam;
        pet.GetComponent<PetMonster>().myPetMonster.monSkillPoints = potSkillPoints;
        PopulateStats();
    }
    public void IncreasePower()
    {
        if (potSkillPoints > 0)
        {
            potSkillPoints--;
            skillPoints.text = potSkillPoints.ToString();
            potPower++;
            power.text = potPower.ToString();
        }
    }
    public void DecreasePower()
    {
        if (potPower > petUpdate.monPower)
        {
            potSkillPoints++;
            skillPoints.text = potSkillPoints.ToString();
            potPower--;
            power.text = potPower.ToString();
        }
    }
    public void IncreaseCritEffect()
    {
        if (potSkillPoints > 0)
        {
            potSkillPoints--;
            skillPoints.text = potSkillPoints.ToString();
            potCritEffect++;
            critEffect.text = potCritEffect.ToString();
        }
    }
    public void DecreaseCritEffect()
    {
        if (potCritEffect > petUpdate.monCriticalEffect)
        {
            potSkillPoints++;
            skillPoints.text = potSkillPoints.ToString();
            potCritEffect--;
            critEffect.text = potCritEffect.ToString();
        }
    }
    public void IncreaseSpeed()
    {
        if (potSkillPoints > 0)
        {
            potSkillPoints--;
            skillPoints.text = potSkillPoints.ToString();
            potSpeed++;
            speed.text = potSpeed.ToString();
        }
    }
    public void DecreaseSpeed()
    {
        if (potSpeed > petUpdate.monSpeed)
        {
            potSkillPoints++;
            skillPoints.text = potSkillPoints.ToString();
            potSpeed--;
            speed.text = potSpeed.ToString();
        }
    }
    public void IncreaseStamina()
    {
        if (potSkillPoints > 0)
        {
            potSkillPoints--;
            skillPoints.text = potSkillPoints.ToString();
            potStam++;
            stam.text = potStam.ToString();
        }
    }
    public void DecreaseStamina()
    {
        if (potStam > petUpdate.monStamina)
        {
            potSkillPoints++;
            skillPoints.text = potSkillPoints.ToString();
            potStam--;
            stam.text = potStam.ToString();
        }
    }
    public void FindAge()
    {
        DateTime bDay = Convert.ToDateTime(pet.GetComponent<PetMonster>().myPetMonster.monBirthDay);
        TimeSpan totalDays = DateTime.Now - bDay;
        pet.GetComponent<PetMonster>().myPetMonster.monAge = (int)totalDays.Days;
    }
    }