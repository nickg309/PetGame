using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arena : MonoBehaviour
{
    public bool fighting;
    public static MonsterClass statsOpponent = new MonsterClass();
    static MonsterClass statsPet = new MonsterClass();
    static CombatPetClass combatPet = new CombatPetClass();
    static CombatPetClass combatOpponent = new CombatPetClass();
    CombatPetClass attackerCurrent, defenderCurrent;
    public Text textHpPet, textChargePet, textHpOpponent, textChargeOpponent, textNameOpponent, textLevelOpponent, textWorld, resultText;
    public GameObject lvlText, waveText, winlossWindow, searchClose, comlogClose, dogTag, failedSearch, pet, opponent, slidersPet, animObjectOpponent, fireballPet, fireballOpponent;
    public Slider sliderPower, sliderCrit, sliderSpeed, sliderStam, sliderHpPet, sliderChargePet, sliderHPOpponent, sliderChargeOpponent;
    public Image portraitOpponent;
    public List<Sprite> spriteList;
    AnimatorCodex animCodexOpponent;
    public RuntimeAnimatorController animsOpponent;

    #region GetOpponent
    public void NotFound()
    {
        if (dogTag.activeInHierarchy == true)
        {
            dogTag.SetActive(false);
        }
        failedSearch.SetActive(true);
    }
    public void CopyOpponent(MonsterClass targetOpponent, string nameOfOpponent)
    {
        int statTotal = new int();
        if (failedSearch.activeInHierarchy == true)
        {
            failedSearch.SetActive(false);
        }

        statsOpponent = targetOpponent;
        textNameOpponent.text = nameOfOpponent;
        textLevelOpponent.text = statsOpponent.monLevel.ToString();
        statTotal = statsOpponent.monPower + statsOpponent.monCriticalEffect + statsOpponent.monSpeed + statsOpponent.monStamina;
        sliderPower.value = ((float)statsOpponent.monPower / statTotal);
        sliderCrit.value = ((float)statsOpponent.monCriticalEffect / statTotal);
        sliderSpeed.value = ((float)statsOpponent.monSpeed / statTotal);
        sliderStam.value = ((float)statsOpponent.monStamina / statTotal);
        PortraitSwitch(statsOpponent.monFormID);

        if (dogTag.activeInHierarchy != true)
        {
            dogTag.SetActive(true);
        }
    }
    void PortraitSwitch(int id)
    {
        portraitOpponent.sprite = spriteList[id];
    }

    #endregion GetOpponent

    #region Fight
    public void StartSession()
    {
        searchClose.SetActive(false);
        winlossWindow.SetActive(false);
        slidersPet.SetActive(true);
        pet.GetComponent<Transform>().localPosition += new Vector3(125, 0, 0);
        textWorld.text = "PvP";
        combatPet.chargeBalance = 0;
        combatOpponent.chargeBalance = 0;
        statsPet = pet.GetComponent<PetMonster>().myPetMonster;
        pet.GetComponent<PetMonster>().TrainingToggle();
        Fight();
    }
    void Fight()
    {
        fighting = true;
        combatPet.ConvertPetStats(statsPet);
        combatPet.opponent = false;
        combatOpponent.ConvertPetStats(statsOpponent);
        combatOpponent.opponent = true;
        animObjectOpponent.SetActive(true);
        animCodexOpponent = gameObject.GetComponent<AnimControlLibrarian>().animatorLibrary[statsOpponent.monFormID - 1];
        animsOpponent = animCodexOpponent.formAnimatorControler;
        animObjectOpponent.GetComponent<Animator>().runtimeAnimatorController = animsOpponent;
        opponent.SetActive(true);
        InitializeCombatUi();
        StartCoroutine("PauseCallSpeedCheck");
    }
    void InitializeCombatUi()
    {
        sliderHpPet.maxValue = combatPet.staminaConversion;
        sliderHpPet.value = combatPet.currentHp;
        textHpPet.text = sliderHpPet.value.ToString() + "/" + (combatPet.staminaConversion).ToString();
        sliderChargePet.maxValue = 100;
        sliderChargePet.value = 0;
        textChargePet.text = sliderChargePet.value.ToString() + "%";

        sliderHPOpponent.maxValue = combatOpponent.staminaConversion;
        sliderHPOpponent.value = combatOpponent.currentHp;
        textHpOpponent.text = sliderHPOpponent.value.ToString() + "/" + (combatOpponent.staminaConversion).ToString();
        sliderChargeOpponent.maxValue = 100;
        sliderChargeOpponent.value = 0;
        textChargeOpponent.text = sliderChargeOpponent.value.ToString() + "%";

        waveText.SetActive(false);
        lvlText.SetActive(false);
    }
    void UpdateCombatUi()
    {
        //call this after every move
        sliderHpPet.value = combatPet.currentHp;
        textHpPet.text = sliderHpPet.value.ToString() + "/" + (combatPet.staminaConversion).ToString();
        sliderChargePet.value = combatPet.chargeBalance;
        textChargePet.text = sliderChargePet.value.ToString() + "%";
        sliderHPOpponent.value = combatOpponent.currentHp;
        textHpOpponent.text = sliderHPOpponent.value.ToString() + "/" + (combatOpponent.staminaConversion).ToString();
        sliderChargeOpponent.value = combatOpponent.chargeBalance;
        textChargeOpponent.text = sliderChargeOpponent.value.ToString() + "%";
    }
    public void UpdateStats()
    {
        statsPet = pet.GetComponent<PetMonster>().myPetMonster;
    }
    public void EndSession()
    {
        pet.GetComponent<Transform>().localPosition += new Vector3(-125, 0, 0);
        pet.GetComponent<PetMonster>().LowerEnergy();
        waveText.SetActive(true);
        lvlText.SetActive(true);
        slidersPet.SetActive(false);
        opponent.SetActive(false);
        animObjectOpponent.SetActive(false);
        pet.GetComponent<PetMonster>().TrainingToggle();
        searchClose.SetActive(true);
    }
    void SpeedSwitch()
    {
        int checkSpeedOpponent, checkSpeedPet;
        bool petBalence = combatPet.chargeBalance >= 100, opponentBalence = combatOpponent.chargeBalance >= 100;
        switch (petBalence)
        {
            case false when opponentBalence == false:
                checkSpeedOpponent = combatOpponent.speedConversion;
                checkSpeedPet = combatPet.speedConversion;
                break;

            case true when opponentBalence == true:
                combatPet.chargeBalance = 0;
                combatOpponent.chargeBalance = 0;
                combatPet.specialDuration = combatPet.specialAttack.specialDuration;
                combatPet.currentHp += combatPet.specialAttack.specialHeal;
                combatPet.currentHp += combatPet.specialAttack.specialHoT;
                combatOpponent.specialDuration = combatOpponent.specialAttack.specialDuration;
                combatOpponent.currentHp += combatPet.specialAttack.specialHeal;
                combatOpponent.currentHp += combatPet.specialAttack.specialHoT;
                checkSpeedOpponent = combatOpponent.specialAttack.specialSpeed;
                checkSpeedPet = combatPet.specialAttack.specialSpeed;
                break;

            case true when opponentBalence == false:
                combatPet.chargeBalance = 0;
                combatPet.specialDuration = combatPet.specialAttack.specialDuration;
                combatPet.currentHp += combatPet.specialAttack.specialHeal;
                combatPet.currentHp += combatPet.specialAttack.specialHoT;
                checkSpeedOpponent = combatOpponent.speedConversion;
                checkSpeedPet = combatPet.specialAttack.specialSpeed;
                break;

            case false when opponentBalence == true:
                combatOpponent.chargeBalance = 0;
                combatOpponent.specialDuration = combatOpponent.specialAttack.specialDuration;
                combatOpponent.currentHp += combatPet.specialAttack.specialHeal;
                combatOpponent.currentHp += combatPet.specialAttack.specialHoT;
                checkSpeedOpponent = combatOpponent.specialAttack.specialSpeed;
                checkSpeedPet = combatPet.speedConversion;
                break;

            default:
                checkSpeedOpponent = combatOpponent.speedConversion;
                checkSpeedPet = combatPet.speedConversion;
                Debug.Log("Speed Check Switch Has Defaulted!");
                break;
        }
        if (checkSpeedPet >= checkSpeedOpponent)
        {
            attackerCurrent = combatPet;
            defenderCurrent = combatOpponent;
            CombatSwitch(combatPet, combatOpponent);
        }
        else
        {
            attackerCurrent = combatOpponent;
            defenderCurrent = combatPet;
            CombatSwitch(combatOpponent, combatPet);
        }
    }
    void CombatSwitch(CombatPetClass attacker, CombatPetClass defender)
    {
        bool attackerSpecialActive = attacker.specialDuration > 0, defenderSpecialActive = defender.specialDuration > 0;
        switch (attackerSpecialActive)
        {
            case false when defenderSpecialActive == false:
                BasicAttacks(attacker, defender);
                break;

            case true when defenderSpecialActive == true:
                SpecialAttacks(attacker, defender);
                break;

            case true when defenderSpecialActive == false:
                AggressorSpecialAttack(attacker, defender);
                break;

            case false when defenderSpecialActive == true:
                DefenderSpecialAttack(attacker, defender);
                break;

            default:
                Debug.Log("Combat Switch Has Defaulted!");
                BasicAttacks(attacker, defender);
                break;
        }
    }
    void BasicAttacks(CombatPetClass attacker, CombatPetClass defender)
    {
        //Agressor's attack.
        int checkSwing = Random.Range(0, 101);
        if (checkSwing < defender.dodgeChance)
        {
            //attack miss
            Debug.Log("Miss!");
        }
        if (checkSwing > (100 - attacker.critChance))
        {
            //critical hit
            Debug.Log("Crit!");
            attacker.chargeBalance += attacker.chargeUp * 2;
            if (attacker.chargeBalance > 100)
            {
                attacker.chargeBalance = 100;
            }
            if (attacker.opponent != true)
            {
                fireballPet.SetActive(true);
                pet.GetComponent<PetMonster>().AttackAnim();
            }
            else
            {
                fireballOpponent.SetActive(true);
                opponent.GetComponentInChildren<EnemyAnimations>().EnemyAttackAnim();
            }
            defender.currentHp -= (int)(attacker.powerConversion + attacker.criticalEffectConversion);
            if (defender.currentHp <= 0)
            {
                StartCoroutine("PauseEnd");
                return;
            }
        }
        if (checkSwing > defender.dodgeChance && checkSwing < (100 - attacker.critChance))
        {
            //normal hit
            //fireballPet.SetActive(true);
            attacker.chargeBalance += attacker.chargeUp;
            if (attacker.chargeBalance > 100)
            {
                attacker.chargeBalance = 100;
            }
            if (attacker.opponent != true)
            {
                fireballPet.SetActive(true);
                pet.GetComponent<PetMonster>().AttackAnim();
            }
            else
            {
                fireballOpponent.SetActive(true);
                opponent.GetComponentInChildren<EnemyAnimations>().EnemyAttackAnim();
            }
            defender.currentHp -= attacker.powerConversion;
            if (defender.currentHp <= 0)
            {
                StartCoroutine("PauseEnd");
                return;
            }
        }
        UpdateCombatUi();
        StartCoroutine("PauseTurnBasic");
    }
    void BasicAttacks2(CombatPetClass attacker, CombatPetClass defender)
    {
        //Defender's attack.
        int checkSwing = Random.Range(0, 101);
        if (checkSwing < attacker.dodgeChance)
        {
            //attack miss
            Debug.Log("Miss!");
        }
        if (checkSwing > (100 - defender.critChance))
        {
            //critical hit
            Debug.Log("Crit!");
            defender.chargeBalance += defender.chargeUp * 2;
            if (defender.chargeBalance > 100)
            {
                defender.chargeBalance = 100;
            }
            if (defender.opponent != true)
            {
                fireballPet.SetActive(true);
                pet.GetComponent<PetMonster>().AttackAnim();
            }
            else
            {
                fireballOpponent.SetActive(true);
                opponent.GetComponentInChildren<EnemyAnimations>().EnemyAttackAnim();
            }
            attacker.currentHp -= (int)(defender.powerConversion + defender.criticalEffectConversion);
            if (attacker.currentHp <= 0)
            {
                StartCoroutine("PauseEnd");
                return;
            }
        }
        if (checkSwing > attacker.dodgeChance && checkSwing < (100 - defender.critChance))
        {
            //normal hit
            fireballPet.SetActive(true);
            defender.chargeBalance += defender.chargeUp;
            if (defender.chargeBalance > 100)
            {
                defender.chargeBalance = 100;
            }
            if (defender.opponent != true)
            {
                fireballPet.SetActive(true);
                pet.GetComponent<PetMonster>().AttackAnim();
            }
            else
            {
                fireballOpponent.SetActive(true);
                opponent.GetComponentInChildren<EnemyAnimations>().EnemyAttackAnim();
            }
            attacker.currentHp -= defender.powerConversion;
            if (attacker.currentHp <= 0)
            {
                StartCoroutine("PauseEnd");
                return;
            }
        }
        UpdateCombatUi();
        StartCoroutine("PauseCallSpeedCheck");
    }
    void SpecialAttacks(CombatPetClass attacker, CombatPetClass defender)
    {
        //Agressor's attack.
        int checkSwing = Random.Range(0, 101);
        if (checkSwing < defender.specialAttack.specialDodge)
        {
            //attack miss
            Debug.Log("Miss!");
        }
        if (checkSwing > (100 - attacker.specialAttack.specialCriticalChance))
        {
            //critical hit
            Debug.Log("Crit!");
            attacker.chargeBalance += attacker.chargeUp * 2;
            if (attacker.chargeBalance > 100)
            {
                attacker.chargeBalance = 100;
            }
            if (attacker.opponent != true)
            {
                fireballPet.SetActive(true);
                pet.GetComponent<PetMonster>().AttackAnim();
            }
            else
            {
                fireballOpponent.SetActive(true);
                opponent.GetComponent<EnemyAnimations>().EnemyAttackAnim();
            }
            defender.currentHp -= (int)((attacker.specialAttack.specialPower + attacker.specialAttack.specialCriticalEffect) - defender.specialAttack.specialShield);
            if (defender.currentHp <= 0)
            {
                StartCoroutine("PauseEnd");
                return;
            }
        }
        if (checkSwing > defender.specialAttack.specialDodge && checkSwing < (100 - attacker.specialAttack.specialCriticalChance))
        {
            //normal hit
            fireballPet.SetActive(true);
            attacker.chargeBalance += attacker.chargeUp;
            if (attacker.chargeBalance > 100)
            {
                attacker.chargeBalance = 100;
            }
            if (attacker.opponent != true)
            {
                fireballPet.SetActive(true);
                pet.GetComponent<PetMonster>().AttackAnim();
            }
            else
            {
                fireballOpponent.SetActive(true);
                opponent.GetComponent<EnemyAnimations>().EnemyAttackAnim();
            }
            defender.currentHp -= (int)(attacker.specialAttack.specialPower - defender.specialAttack.specialShield);
            if (defender.currentHp <= 0)
            {
                StartCoroutine("PauseEnd");
                return;
            }
        }
        UpdateCombatUi();
        StartCoroutine("PauseTurnSpecial");
    }
    void SpecialAttacks2(CombatPetClass attacker, CombatPetClass defender)
    {
        //Defender's attack.
        int checkSwing = Random.Range(0, 101);
        if (checkSwing < attacker.specialAttack.specialDodge)
        {
            //attack miss
            Debug.Log("Miss!");
        }
        if (checkSwing > (100 - defender.specialAttack.specialCriticalChance))
        {
            //critical hit
            Debug.Log("Crit!");
            defender.chargeBalance += defender.chargeUp * 2;
            if (defender.chargeBalance > 100)
            {
                defender.chargeBalance = 100;
            }
            if (defender.opponent != true)
            {
                fireballPet.SetActive(true);
                pet.GetComponent<PetMonster>().AttackAnim();
            }
            else
            {
                fireballOpponent.SetActive(true);
                opponent.GetComponent<EnemyAnimations>().EnemyAttackAnim();
            }
            attacker.currentHp -= (int)((defender.specialAttack.specialPower + defender.specialAttack.specialCriticalEffect) - attacker.specialAttack.specialShield);
            if (attacker.currentHp <= 0)
            {
                StartCoroutine("PauseEnd");
                return;
            }
        }
        if (checkSwing > attacker.specialAttack.specialDodge && checkSwing < (100 - defender.specialAttack.specialCriticalChance))
        {
            //normal hit
            fireballPet.SetActive(true);
            defender.chargeBalance += defender.chargeUp;
            if (defender.chargeBalance > 100)
            {
                defender.chargeBalance = 100;
            }
            if (defender.opponent != true)
            {
                fireballPet.SetActive(true);
                pet.GetComponent<PetMonster>().AttackAnim();
            }
            else
            {
                fireballOpponent.SetActive(true);
                opponent.GetComponent<EnemyAnimations>().EnemyAttackAnim();
            }
            attacker.currentHp -= (int)(defender.specialAttack.specialPower - attacker.specialAttack.specialShield);
            if (attacker.currentHp <= 0)
            {
                StartCoroutine("PauseEnd");
                return;
            }
        }
        UpdateCombatUi();
        StartCoroutine("PauseCallSpeedCheck");
    }
    void AggressorSpecialAttack(CombatPetClass attacker, CombatPetClass defender)
    {
        //Agressor's attack.
        int checkSwing = Random.Range(0, 101);
        if (checkSwing < defender.dodgeChance)
        {
            //attack miss
            Debug.Log("Miss!");
        }
        if (checkSwing > (100 - attacker.specialAttack.specialCriticalChance))
        {
            //critical hit
            Debug.Log("Crit!");
            attacker.chargeBalance += attacker.chargeUp * 2;
            if (attacker.chargeBalance > 100)
            {
                attacker.chargeBalance = 100;
            }
            if (attacker.opponent != true)
            {
                fireballPet.SetActive(true);
                pet.GetComponent<PetMonster>().AttackAnim();
            }
            else
            {
                fireballOpponent.SetActive(true);
                opponent.GetComponentInChildren<EnemyAnimations>().EnemyAttackAnim();
            }
            defender.currentHp -= (int)(attacker.specialAttack.specialPower + attacker.specialAttack.specialCriticalEffect);
            if (defender.currentHp <= 0)
            {
                StartCoroutine("PauseEnd");
                return;
            }
        }
        if (checkSwing > defender.dodgeChance && checkSwing < (100 - attacker.specialAttack.specialCriticalChance))
        {
            //normal hit
            fireballPet.SetActive(true);
            attacker.chargeBalance += attacker.chargeUp;
            if (attacker.chargeBalance > 100)
            {
                attacker.chargeBalance = 100;
            }
            if (attacker.opponent != true)
            {
                fireballPet.SetActive(true);
                pet.GetComponent<PetMonster>().AttackAnim();
            }
            else
            {
                fireballOpponent.SetActive(true);
                opponent.GetComponentInChildren<EnemyAnimations>().EnemyAttackAnim();
            }
            defender.currentHp -= attacker.specialAttack.specialPower;
            if (defender.currentHp <= 0)
            {
                StartCoroutine("PauseEnd");
                return;
            }
        }
        UpdateCombatUi();
        StartCoroutine("PauseTurnAggressor");
    }
    void AggressorSpecialAttack2(CombatPetClass attacker, CombatPetClass defender)
    {
        //Defender's attack.
        int checkSwing = Random.Range(0, 101);
        if (checkSwing < attacker.specialAttack.specialDodge)
        {
            //attack miss
            Debug.Log("Miss!");
        }
        if (checkSwing > (100 - defender.critChance))
        {
            //critical hit
            Debug.Log("Crit!");
            defender.chargeBalance += defender.chargeUp * 2;
            if (defender.chargeBalance > 100)
            {
                defender.chargeBalance = 100;
            }
            if (defender.opponent != true)
            {
                fireballPet.SetActive(true);
                pet.GetComponent<PetMonster>().AttackAnim();
            }
            else
            {
                fireballOpponent.SetActive(true);
                opponent.GetComponentInChildren<EnemyAnimations>().EnemyAttackAnim();
            }
            attacker.currentHp -= (int)((defender.powerConversion + defender.criticalEffectConversion) - attacker.specialAttack.specialShield);
            if (attacker.currentHp <= 0)
            {
                StartCoroutine("PauseEnd");
                return;
            }
        }
        if (checkSwing > attacker.specialAttack.specialDodge && checkSwing < (100 - defender.critChance))
        {
            //normal hit
            fireballPet.SetActive(true);
            defender.chargeBalance += defender.chargeUp;
            if (defender.chargeBalance > 100)
            {
                defender.chargeBalance = 100;
            }
            if (defender.opponent != true)
            {
                fireballPet.SetActive(true);
                pet.GetComponent<PetMonster>().AttackAnim();
            }
            else
            {
                fireballOpponent.SetActive(true);
                opponent.GetComponentInChildren<EnemyAnimations>().EnemyAttackAnim();
            }
            attacker.currentHp -= (int)(defender.powerConversion - attacker.specialAttack.specialShield);
            if (attacker.currentHp <= 0)
            {
                StartCoroutine("PauseEnd");
                return;
            }
        }
        UpdateCombatUi();
        StartCoroutine("PauseCallSpeedCheck");
    }
    void DefenderSpecialAttack(CombatPetClass attacker, CombatPetClass defender)
    {
        //Agressor's attack.
        int checkSwing = Random.Range(0, 101);
        if (checkSwing < defender.specialAttack.specialDodge)
        {
            //attack miss
            Debug.Log("Miss!");
        }
        if (checkSwing > (100 - attacker.critChance))
        {
            //critical hit
            Debug.Log("Crit!");
            attacker.chargeBalance += attacker.chargeUp * 2;
            if (attacker.chargeBalance > 100)
            {
                attacker.chargeBalance = 100;
            }
            if (attacker.opponent != true)
            {
                fireballPet.SetActive(true);
                pet.GetComponent<PetMonster>().AttackAnim();
            }
            else
            {
                fireballOpponent.SetActive(true);
                opponent.GetComponentInChildren<EnemyAnimations>().EnemyAttackAnim();
            }
            defender.currentHp -= (int)((attacker.powerConversion + attacker.criticalEffectConversion) - defender.specialAttack.specialShield);
            if (defender.currentHp <= 0)
            {
                StartCoroutine("Paused");
                return;
            }
        }
        if (checkSwing > defender.specialAttack.specialDodge && checkSwing < (100 - attacker.critChance))
        {
            //normal hit
            fireballPet.SetActive(true);
            attacker.chargeBalance += attacker.chargeUp;
            if (attacker.chargeBalance > 100)
            {
                attacker.chargeBalance = 100;
            }
            if (attacker.opponent != true)
            {
                fireballPet.SetActive(true);
                pet.GetComponent<PetMonster>().AttackAnim();
            }
            else
            {
                fireballOpponent.SetActive(true);
                opponent.GetComponentInChildren<EnemyAnimations>().EnemyAttackAnim();
            }
            defender.currentHp -= (int)(attacker.powerConversion - defender.specialAttack.specialShield);
            if (defender.currentHp <= 0)
            {
                StartCoroutine("PauseEnd");
                return;
            }
        }
        UpdateCombatUi();
        StartCoroutine("PauseTurnDefender");
    }
    void DefenderSpecialAttack2(CombatPetClass attacker, CombatPetClass defender)
    {
        //Defender's attack.
        int checkSwing = Random.Range(0, 101);
        if (checkSwing < attacker.dodgeChance)
        {
            //attack miss
            Debug.Log("Miss!");
        }
        if (checkSwing > (100 - defender.specialAttack.specialCriticalChance))
        {
            //critical hit
            Debug.Log("Crit!");
            defender.chargeBalance += defender.chargeUp * 2;
            if (defender.chargeBalance > 100)
            {
                defender.chargeBalance = 100;
            }
            if (defender.opponent != true)
            {
                fireballPet.SetActive(true);
                pet.GetComponent<PetMonster>().AttackAnim();
            }
            else
            {
                fireballOpponent.SetActive(true);
                opponent.GetComponentInChildren<EnemyAnimations>().EnemyAttackAnim();
            }
            attacker.currentHp -= (int)(defender.specialAttack.specialPower + defender.specialAttack.specialCriticalEffect);
            if (attacker.currentHp <= 0)
            {
                StartCoroutine("PauseEnd");
                return;
            }
        }
        if (checkSwing > attacker.dodgeChance && checkSwing < (100 - defender.specialAttack.specialCriticalChance))
        {
            //normal hit
            fireballPet.SetActive(true);
            defender.chargeBalance += defender.chargeUp;
            if (defender.chargeBalance > 100)
            {
                defender.chargeBalance = 100;
            }
            if (defender.opponent != true)
            {
                fireballPet.SetActive(true);
                pet.GetComponent<PetMonster>().AttackAnim();
            }
            else
            {
                fireballOpponent.SetActive(true);
                opponent.GetComponentInChildren<EnemyAnimations>().EnemyAttackAnim();
            }
            attacker.currentHp -= defender.specialAttack.specialPower;
            if (attacker.currentHp <= 0)
            {
                StartCoroutine("PauseEnd");
                return;
            }
        }
        UpdateCombatUi();
        StartCoroutine("PauseCallSpeedCheck");
    }
    public void StopFight()
    {
        UpdateCombatUi();
        comlogClose.SetActive(true);
        if (combatOpponent.currentHp <= 0)
        {
            resultText.text = "WIN!";
            pet.GetComponent<PetMonster>().AddPvpWin();
        }
        else
        {
            resultText.text = "LOSS...";
            pet.GetComponent<PetMonster>().AddPvpLoss();
        }
        fighting = false;
        winlossWindow.SetActive(true);
    }
    public void ComLogCloseToggle()
    {
        comlogClose.SetActive(false);
    }

    #endregion Fight

    #region Coroutines
    IEnumerator PauseCallSpeedCheck()
    {
        yield return new WaitForSeconds(1);
        if (pet.GetComponent<PetMonster>().attacking == true)
        {
            pet.GetComponent<PetMonster>().AttackAnimFalse();
        }
        opponent.GetComponent<EnemyAnimations>().EnemyStopAttackAnim();
        SpeedSwitch();
    }
    IEnumerator PauseTurnBasic()
    {
        yield return new WaitForSeconds(1);
        if (pet.GetComponent<PetMonster>().attacking == true)
        {
            pet.GetComponent<PetMonster>().AttackAnimFalse();
        }
        opponent.GetComponentInChildren<EnemyAnimations>().EnemyStopAttackAnim();
        BasicAttacks2(attackerCurrent, defenderCurrent);
    }
    IEnumerator PauseTurnSpecial()
    {
        yield return new WaitForSeconds(1);
        if (pet.GetComponent<PetMonster>().attacking == true)
        {
            pet.GetComponent<PetMonster>().AttackAnimFalse();
        }
        opponent.GetComponentInChildren<EnemyAnimations>().EnemyStopAttackAnim();
        SpecialAttacks2(attackerCurrent, defenderCurrent);
    }
    IEnumerator PauseTurnAggressor()
    {
        yield return new WaitForSeconds(1);
        if (pet.GetComponent<PetMonster>().attacking == true)
        {
            pet.GetComponent<PetMonster>().AttackAnimFalse();
        }
        opponent.GetComponentInChildren<EnemyAnimations>().EnemyStopAttackAnim();
        AggressorSpecialAttack2(attackerCurrent, defenderCurrent);
    }
    IEnumerator PauseTurnDefender()
    {
        yield return new WaitForSeconds(1);
        if (pet.GetComponent<PetMonster>().attacking == true)
        {
            pet.GetComponent<PetMonster>().AttackAnimFalse();
        }
        opponent.GetComponentInChildren<EnemyAnimations>().EnemyStopAttackAnim();
        DefenderSpecialAttack2(attackerCurrent, defenderCurrent);
    }
    IEnumerator PauseEnd()
    {
        yield return new WaitForSeconds(1);
        if (pet.GetComponent<PetMonster>().attacking == true)
        {
            pet.GetComponent<PetMonster>().AttackAnimFalse();
        }
        opponent.GetComponentInChildren<EnemyAnimations>().EnemyStopAttackAnim();
        StopFight();
    }

    #endregion Coroutines

}