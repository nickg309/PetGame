using System.Collections;
//using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Train : MonoBehaviour
{
    public GameObject gameHandler, pet, enemy, petSliders, championEmblem, startSessionPanel, trainPanel, enemyAnim, waveCounter, petFireball, enemyFireball;
    public Slider petHpSlider, petChargeSlider, enemyHpSlider, enemyChargeSlider, expSlider;
    public Text power, critEffect, speed, stam, skillPoints, petLevelText, petHpText, petChargeText, enemyHpText, enemyChargeText, worldText;
    int potPower, potCritEffect, potSpeed, potStam, potSkillPoints, enemyWave, petCharge, enemyCharge, specialBuff;
    MonsterClass myPetStats;
    EnemyClass myEnemy = new EnemyClass();
    CombatPetClass myCombatPet = new CombatPetClass();

    #region Buttons
    public void EnergyCheck()
    {
        if(pet.GetComponent<PetMonster>().myPetMonster.monEnergy < 1)
        {
            return;
        }
        else
        {
            gameHandler.GetComponent<UIcontrols>().OpenWindow(startSessionPanel);
            StartSession();
            UpdateStats();
        }
    }
    public void StartSession()
    {
        pet.GetComponent<PetMonster>().myPetMonster.monWorld = pet.GetComponent<PetMonster>().myPetMonster.monHighestWorld - 3;
        if(pet.GetComponent<PetMonster>().myPetMonster.monWorld < 1)
        {
            pet.GetComponent<PetMonster>().myPetMonster.monWorld = 1;
        }
        pet.GetComponent<PetMonster>().TrainingToggle();
        myPetStats = pet.GetComponent<PetMonster>().myPetMonster;
        pet.GetComponent<Transform>().localPosition += new Vector3(125, 0, 0);
        worldText.text = myPetStats.monWorld.ToString();
        petCharge = 0;
        enemyCharge = 0;
        enemyWave = 1;
        petSliders.SetActive(true);
        Fight();
    }
    public void EndSession()
    {
        if (pet.GetComponent<PetMonster>().attacking == true)
        {
            pet.GetComponent<PetMonster>().AttackAnimFalse();
        }
        if (petFireball.activeInHierarchy == true)
        {
            petFireball.SetActive(false);
        }
        if (enemyFireball.activeInHierarchy == true)
        {
            enemyFireball.SetActive(false);
        }
        pet.GetComponent<Transform>().localPosition += new Vector3(-125, 0, 0);
        pet.GetComponent<PetMonster>().TrainingToggle();
        pet.GetComponent<PetMonster>().LowerEnergy();
        pet.GetComponent<PetMonster>().myPetMonster.monWorld = 1;
        petSliders.SetActive(false);
        enemy.SetActive(false);
        enemyAnim.SetActive(false);
        waveCounter.SetActive(false);
        championEmblem.SetActive(false);
        startSessionPanel.SetActive(true);
        trainPanel.SetActive(false);
    }
    public void UpdateStats()
    {
        myPetStats = pet.GetComponent<PetMonster>().myPetMonster;
        
        potPower = myPetStats.monPower;
        power.text = potPower.ToString();
        potCritEffect = myPetStats.monCriticalEffect;
        critEffect.text = potCritEffect.ToString();
        potSpeed = myPetStats.monSpeed;
        speed.text = potSpeed.ToString();
        potStam = myPetStats.monStamina;
        stam.text = potStam.ToString();
        potSkillPoints = myPetStats.monSkillPoints;
        skillPoints.text = potSkillPoints.ToString();
        petLevelText.text = myPetStats.monLevel.ToString();
        expSlider.maxValue = myPetStats.monLevel * 300;
        expSlider.value = myPetStats.monExp;
    }
    public void ApplyStatChanges()
    {
        pet.GetComponent<PetMonster>().myPetMonster.monPower = potPower;
        pet.GetComponent<PetMonster>().myPetMonster.monCriticalEffect = potCritEffect;
        pet.GetComponent<PetMonster>().myPetMonster.monSpeed = potSpeed;
        pet.GetComponent<PetMonster>().myPetMonster.monStamina = potStam;
        pet.GetComponent<PetMonster>().myPetMonster.monSkillPoints = potSkillPoints;
        UpdateStats();
    }
    public void IncreasePower()
    {
        if(potSkillPoints > 0)
        {
            potSkillPoints--;
            skillPoints.text = potSkillPoints.ToString();
            potPower++;
            power.text = potPower.ToString();
        }
    }
    public void DecreasePower()
    {
        if (potPower > myPetStats.monPower)
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
        if (potCritEffect > myPetStats.monCriticalEffect)
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
        if (potSpeed > myPetStats.monSpeed)
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
        if (potStam > myPetStats.monStamina)
        {
            potSkillPoints++;
            skillPoints.text = potSkillPoints.ToString();
            potStam--;
            stam.text = potStam.ToString();
        }
    }

    #endregion

    void InitializeCombatUi()
    {
        petHpSlider.maxValue = myCombatPet.staminaConversion;
        petHpSlider.value = myCombatPet.currentHp;
        petHpText.text = petHpSlider.value.ToString() + "/" + (myCombatPet.staminaConversion).ToString();
        petChargeSlider.maxValue = 100;
        petChargeSlider.value = petCharge;
        petChargeText.text = petChargeSlider.value.ToString() + "%";

        enemyHpSlider.maxValue = myEnemy.Estamina * 10;
        enemyHpSlider.value = myEnemy.currentHp;
        enemyHpText.text = enemyHpSlider.value.ToString() + "/" + (myEnemy.Estamina * 10).ToString();
        enemyChargeSlider.maxValue = 100;
        enemyChargeSlider.value = 0;
        enemyChargeText.text = enemyChargeSlider.value.ToString() + "%";
    }
    void UpdateCombatUi()
    {
        //call this after every move
        petHpSlider.value = myCombatPet.currentHp;
        petHpText.text = petHpSlider.value.ToString() + "/" + (myCombatPet.staminaConversion).ToString();
        petChargeSlider.value = petCharge;
        petChargeText.text = petChargeSlider.value.ToString() + "%";
        enemyHpSlider.value = myEnemy.currentHp;
        enemyHpText.text = enemyHpSlider.value.ToString() + "/" + (myEnemy.Estamina * 10).ToString();
        enemyChargeSlider.value = enemyCharge;
        enemyChargeText.text = enemyChargeSlider.value.ToString() + "%";
    }
    void Fight()
    {
        myCombatPet.ConvertPetStats(myPetStats);
        myEnemy.SetEWorld(myPetStats.monWorld);
        if (enemyWave < 10)
        {
            myEnemy.RivalStatBoost();
            enemy.SetActive(true);
            enemyAnim.SetActive(true);
            enemyAnim.GetComponent<EnemyAnimations>().SetRivalAnim();
            waveCounter.GetComponent<Text>().text = (enemyWave.ToString() + "/9");
            waveCounter.SetActive(true);
        }
        if (enemyWave == 10)
        {
            championEmblem.SetActive(true);
            enemy.SetActive(true);
            enemyAnim.SetActive(true);
            enemyAnim.GetComponent<EnemyAnimations>().SetBossAnim();
            myEnemy.ChampionStatBoost();
        }
        enemy.SetActive(true);
        InitializeCombatUi();
        StartCoroutine("PauseCallSpeedCheck");
    }
    void SpeedCheck()
    {
        if(petCharge != 100)
        {
            if (specialBuff > 0)
            {
                int myenemySpeed = myEnemy.Espeed;
                int mypetSpeed = myCombatPet.specialAttack.specialSpeed;
                myCombatPet.currentHp += myCombatPet.specialAttack.specialHoT;
                if (mypetSpeed >= myenemySpeed)
                {
                    BuffPetAttackFirst();
                }
                else
                {
                    BuffEnemyAttackFirst();
                }
            }
            else
            {
                int myenemySpeed = myEnemy.Espeed;
                int mypetSpeed = myCombatPet.speedConversion;
                if (mypetSpeed >= myenemySpeed)
                {
                    PetFirst();
                }
                else
                {
                    EnemyFirst();
                }
            }
        }
        else
        {
            petCharge = 0;
            specialBuff = myCombatPet.specialAttack.specialDuration;
            myCombatPet.currentHp += myCombatPet.specialAttack.specialHeal;
            myCombatPet.currentHp += myCombatPet.specialAttack.specialHoT;
            BuffPetAttackFirst();
        }
    }
    void EnemyFirst()
    {
        var enemyAnim = enemy.GetComponent<EnemyAnimations>();
        int enemySwing = Random.Range(0, 101);
        if(enemySwing < myCombatPet.dodgeChance)
        {
            //enemy miss
            Debug.Log("Enemy Miss!");
        }
        if(enemySwing > (100 - myEnemy.critChance))
        {
            //enemy critical hit
            Debug.Log("Enemy Crit!");
            enemyFireball.SetActive(true);
            enemyAnim.EnemyAttackAnim();
            myCombatPet.currentHp -= (int)(myEnemy.Epower + myEnemy.EcritEffect);
            if(myCombatPet.currentHp <= 0)
            {
                StartCoroutine("EndPause");
                return;
            }
        }
        if(enemySwing > myCombatPet.dodgeChance && enemySwing < (100 - myEnemy.critChance))
        {
            //enemy normal hit
            enemyFireball.SetActive(true);
            enemyAnim.EnemyAttackAnim();
            myCombatPet.currentHp -= myEnemy.Epower;
            if (myCombatPet.currentHp <= 0)
            {
                StartCoroutine("EndPause");
                return;
            }
        }
        UpdateCombatUi();
        StartCoroutine("PauseCallPetAttack");
    }
    void PetFirst()
    {
        var petAnim = pet.GetComponent<PetMonster>();
        int mySwing = Random.Range(0, 101);
        if(mySwing < myEnemy.dodgeChance)
        {
            //miss
            Debug.Log("Pet Miss!");
        }
        if(mySwing > (100 - myCombatPet.critChance))
        {
            //critical hit
            Debug.Log("Pet Crit!");
            petFireball.SetActive(true);
            petCharge += myCombatPet.chargeUp * 2;
            if (petCharge > 100)
            {
                petCharge = 100;
            }
            myEnemy.currentHp -= (int)(myCombatPet.powerConversion + myCombatPet.criticalEffectConversion);
            petAnim.AttackAnim();
            if (myEnemy.currentHp <= 0)
            {
                UpdateCombatUi();
                StartCoroutine("Pause");
                return;
            }
        }
        if(mySwing > myEnemy.dodgeChance && mySwing < (100 -  myCombatPet.critChance))
        {
            //normal hit
            petFireball.SetActive(true);
            petCharge += myCombatPet.chargeUp;
            if(petCharge > 100)
            {
                petCharge = 100;
            }
            petAnim.AttackAnim();
            myEnemy.currentHp -= myCombatPet.powerConversion;
            if (myEnemy.currentHp <= 0)
            {
                UpdateCombatUi();
                StartCoroutine("Pause");
                return;
            }
        }
        UpdateCombatUi();
        StartCoroutine("PauseCallEnemyAttack");
    }
    void EnemyAttack()
    { 
        int enemySwing = Random.Range(0, 101);

        if(enemySwing < myCombatPet.dodgeChance)
        {
            //enemy miss
            Debug.Log("Enemy Miss!");
        }
        if(enemySwing > (100 - myEnemy.critChance))
        {
            //enemy critical hit
            Debug.Log("Enemy Crit!");
            enemyFireball.SetActive(true);
            enemyAnim.GetComponent<EnemyAnimations>().EnemyAttackAnim();
            myCombatPet.currentHp -= (int)(myEnemy.Epower + myEnemy.EcritEffect);
            if (myCombatPet.currentHp <= 0)
            {
                StartCoroutine("EndPause");
                return;
            }
        }
        if(enemySwing > myCombatPet.dodgeChance && enemySwing < (100 - myEnemy.critChance))
        {
            //enemy normal hit
            enemyFireball.SetActive(true);
            enemyAnim.GetComponent<EnemyAnimations>().EnemyAttackAnim();
            myCombatPet.currentHp -= myEnemy.Epower;
            if (myCombatPet.currentHp <= 0)
            {
                StartCoroutine("EndPause");
                return;
            }
        }

        UpdateCombatUi();
        StartCoroutine("PauseCallSpeedCheck");
    }
    void PetAttack()
    {
        int mySwing = Random.Range(0, 101);

        if(mySwing < myEnemy.dodgeChance)
        {
            //pet miss
            Debug.Log("Pet Miss!");
        }
        if(mySwing > (100 - myCombatPet.critChance))
        {
            //pet critical hit
            Debug.Log("Pet Crit!");
            petFireball.SetActive(true);
            petCharge += myCombatPet.chargeUp * 2;
            if (petCharge > 100)
            {
                petCharge = 100;
            }
            pet.GetComponent<PetMonster>().AttackAnim();
            myEnemy.currentHp -= (int)(myCombatPet.powerConversion + myCombatPet.criticalEffectConversion);
            if (myEnemy.currentHp <= 0)
            {
                StartCoroutine("Pause");
                return;
            }
        }
        if(mySwing > myEnemy.dodgeChance && mySwing < (100 - myCombatPet.critChance))
        {
            //pet normal hit
            petFireball.SetActive(true);
            petCharge += myCombatPet.chargeUp;
            if (petCharge > 100)
            {
                petCharge = 100;
            }
            pet.GetComponent<PetMonster>().AttackAnim();
            myEnemy.currentHp -= myCombatPet.powerConversion;
            if (myEnemy.currentHp <= 0)
            {
                StartCoroutine("Pause");
                return;
            }
        }

        UpdateCombatUi();
        StartCoroutine("PauseCallSpeedCheck");
    }
    void BuffPetAttack()
    {
        int mySwing = Random.Range(0, 101);

        if (mySwing < myEnemy.dodgeChance)
        {
            //pet miss
            Debug.Log("Pet Miss!");
        }
        if (mySwing > (100 - myCombatPet.specialAttack.specialCriticalChance))
        {
            //pet critical hit
            Debug.Log("Pet Crit!");
            petFireball.SetActive(true);
            petCharge += myCombatPet.chargeUp * 2;
            if (petCharge > 100)
            {
                petCharge = 100;
            }
            pet.GetComponent<PetMonster>().AttackAnim();
            myEnemy.currentHp -= (int)(myCombatPet.specialAttack.specialPower + myCombatPet.specialAttack.specialCriticalEffect);
            if (myEnemy.currentHp <= 0)
            {
                StartCoroutine("Pause");
                return;
            }
        }
        if (mySwing > myEnemy.dodgeChance && mySwing < (100 - myCombatPet.critChance))
        {
            //pet normal hit
            petFireball.SetActive(true);
            petCharge += myCombatPet.chargeUp;
            if (petCharge > 100)
            {
                petCharge = 100;
            }
            pet.GetComponent<PetMonster>().AttackAnim();
            myEnemy.currentHp -= myCombatPet.powerConversion;
            if (myEnemy.currentHp <= 0)
            {
                StartCoroutine("Pause");
                return;
            }
        }
        specialBuff--;
        UpdateCombatUi();
        StartCoroutine("PauseCallSpeedCheck");
    }
    void BuffEnemyAttackFirst()
    {
        int enemySwing = Random.Range(0, 101);

        if (enemySwing < myCombatPet.specialAttack.specialDodge)
        {
            //enemy miss
            Debug.Log("Enemy Miss!");
        }
        if (enemySwing > (100 - myEnemy.critChance))
        {
            //enemy critical hit
            Debug.Log("Enemy Crit!");
            enemyFireball.SetActive(true);
            int critDamage = (int)(myEnemy.Epower + myEnemy.EcritEffect);
            enemyAnim.GetComponent<EnemyAnimations>().EnemyAttackAnim();
            myCombatPet.currentHp -= (int)(critDamage - (critDamage * myCombatPet.specialAttack.specialShield));
            myEnemy.currentHp -= (int)(critDamage * (myCombatPet.specialAttack.specialReflect));
            if (myCombatPet.currentHp <= 0)
            {
                StartCoroutine("EndPause");
                return;
            }
            else if (myEnemy.currentHp <= 0)
            {
                StartCoroutine("Pause");
                return;
            }
        }
        if (enemySwing > myCombatPet.dodgeChance && enemySwing < (100 - myEnemy.critChance))
        {
            //enemy normal hit
            enemyFireball.SetActive(true);
            int damage = (int)(myEnemy.Epower);
            enemyAnim.GetComponent<EnemyAnimations>().EnemyAttackAnim();
            myCombatPet.currentHp -= (int)(damage - (damage * myCombatPet.specialAttack.specialShield));
            myEnemy.currentHp -= (int)(damage * (myCombatPet.specialAttack.specialReflect));
            if (myCombatPet.currentHp <= 0)
            {
                StartCoroutine("EndPause");
                return;
            }
            else if (myEnemy.currentHp <= 0)
            {
                StartCoroutine("Pause");
                return;
            }
        }

        UpdateCombatUi();
        StartCoroutine("PauseCallPetAttack");
    }
    void BuffPetAttackFirst()
    {
        if (myCombatPet.currentHp > myCombatPet.staminaConversion)
        {
            myCombatPet.currentHp = myCombatPet.staminaConversion;
        }
        int mySwing = Random.Range(0, 101);
        if (mySwing < myEnemy.dodgeChance)
        {
            //miss
            Debug.Log("Pet Special Attack Miss!");
        }
        if (mySwing > (100 - myCombatPet.specialAttack.specialCriticalChance))
        {
            //critical hit
            Debug.Log("Pet Special Attack Crit!");
            pet.GetComponent<PetMonster>().AttackAnim();
            myEnemy.currentHp -= (int)(myCombatPet.specialAttack.specialDamage + myCombatPet.specialAttack.specialCriticalEffect);
            if (myEnemy.currentHp <= 0)
            {
                StartCoroutine("Pause");
                return;
            }
        }
        if (mySwing > myEnemy.dodgeChance && mySwing < (100 - myCombatPet.critChance))
        {
            //normal hit
            pet.GetComponent<PetMonster>().AttackAnim();
            myEnemy.currentHp -= myCombatPet.specialAttack.specialDamage;
            if (myEnemy.currentHp <= 0)
            {
                StartCoroutine("Pause");
                return;
            }
        }
        UpdateCombatUi();
        StartCoroutine("PauseCallEnemyAttack");
    }
    void Victory()
    {
        
        enemy.SetActive(false);
        if(enemyWave == 10)
        {
            pet.GetComponent<PetMonster>().AddExp(myPetStats.monWorld * 100);
            pet.GetComponent<PetMonster>().WorldClimb();
            worldText.text = myPetStats.monWorld.ToString();
            championEmblem.SetActive(false);
            enemyAnim.SetActive(true);
            enemyAnim.GetComponent<EnemyAnimations>().SetBossAnim();
            enemyWave = 1;
            UpdateStats();
        }
        if(enemyWave < 10)
        {
            pet.GetComponent<PetMonster>().AddExp(myPetStats.monWorld * 30);
            enemyWave++;
            enemyAnim.SetActive(true);
            enemyAnim.GetComponent<EnemyAnimations>().SetRivalAnim();
            waveCounter.SetActive(false);
            UpdateStats();
        }
        expSlider.maxValue = myPetStats.monLevel * 300;
        expSlider.value = myPetStats.monExp;
        StartCoroutine("PauseCallNextFight");
    }

    IEnumerator EndPause()
    {
        yield return new WaitForSeconds(1);
        EndSession();
    }
    IEnumerator Pause()
    {
        yield return new WaitForSeconds(1);
        if (pet.GetComponent<PetMonster>().attacking == true)
        {
            pet.GetComponent<PetMonster>().AttackAnimFalse();
        }
        Victory();
    }
    IEnumerator PauseCallSpeedCheck()
    {
        yield return new WaitForSeconds(1);
        if (pet.GetComponent<PetMonster>().attacking == true)
        {
            pet.GetComponent<PetMonster>().AttackAnimFalse();
        }
        SpeedCheck();
    }
    IEnumerator PauseCallEnemyAttack()
    {
        yield return new WaitForSeconds(1);
        if (pet.GetComponent<PetMonster>().attacking == true)
        {
            pet.GetComponent<PetMonster>().AttackAnimFalse();
        }
        if (specialBuff > 0)
        {
            BuffEnemyAttackFirst();
        }
        else
        {
            EnemyAttack();
        }
    }
    IEnumerator PauseCallPetAttack()
    {
        yield return new WaitForSeconds(1);
        if(specialBuff > 0)
        {
            BuffPetAttack();
        }
        else
        {        
            PetAttack();
        }
    }
    IEnumerator PauseCallNextFight()
    {
        yield return new WaitForSeconds(1);
        if(pet.GetComponent<PetMonster>().attacking == true)
        {
            pet.GetComponent<PetMonster>().AttackAnimFalse();
        }
        yield return new WaitForSeconds(2);
        Fight();
    }
}
public class EnemyClass
{
    public int Epower, EcritEffect, Espeed, Estamina, Eworld, currentHp;
    public float critChance, dodgeChance;

    public void SetEWorld(int world)
    {
        Eworld = world;
    }
    public void ChampionStatBoost()
    {
        int random = Random.Range(1, 7);
        switch (random)
        {
            case 1:
                Epower = Eworld + (int)(Eworld * 0.9f);
                EcritEffect = Eworld + (int)(Eworld * 0.9f);
                Espeed = Eworld;
                Estamina = Eworld + (int)(Eworld * 1.2f);
                break;
            case 2:
                Epower = Eworld + (int)(Eworld * 0.9f);
                EcritEffect = Eworld;
                Espeed = Eworld;
                Estamina = Eworld + (int)(Eworld * 1.2f);
                break;
            case 3:
                Epower = Eworld + (int)(Eworld * 0.9f);
                EcritEffect = Eworld;
                Espeed = Eworld + (int)(Eworld * 0.9f);
                Estamina = Eworld + (int)(Eworld * 1.2f);
                break;
            case 4:
                Epower = Eworld;
                EcritEffect = Eworld + (int)(Eworld * 0.9f);
                Espeed = Eworld;
                Estamina = Eworld + (int)(Eworld * 1.2f);
                break;
            case 5:
                Epower = Eworld;
                EcritEffect = Eworld + (int)(Eworld * 0.9f);
                Espeed = Eworld + (int)(Eworld * 0.9f);
                Estamina = Eworld + (int)(Eworld * 1.2f);
                break;
            case 6:
                Epower = Eworld;
                EcritEffect = Eworld;
                Espeed = Eworld + (int)(Eworld * 0.9f);
                Estamina = Eworld + (int)(Eworld * 1.2f);
                break;
            default:
                Debug.Log("ChampionStatBoostFunction was given a value out of its range.");
                break;
        }
        currentHp = Estamina * 10;
        critChance = 0.1f;
        dodgeChance = 0.2f;
    }
    public void RivalStatBoost()
    {
        int random = Random.Range(1, 6);
        switch (random)
        {
                case 1:
                    Epower = Eworld + (int)(Eworld * 0.3f);
                    EcritEffect = Eworld;
                    Espeed = Eworld;
                    Estamina = Eworld;
                    break;
                case 2:
                    Epower = Eworld;
                    EcritEffect = Eworld + (int)(Eworld* 0.3f);
                    Espeed = Eworld;
                    Estamina = Eworld;
                    break;
                case 3:
                    Epower = Eworld;
                    EcritEffect = Eworld;
                    Espeed = Eworld;
                    Estamina = Eworld;
                    break;
                case 4:
                    Epower = Eworld;
                    EcritEffect = Eworld;
                    Espeed = Eworld + (int)(Eworld * 0.3f);
                    Estamina = Eworld;
                    break;
                case 5:
                    Epower = Eworld;
                    EcritEffect = Eworld;
                    Espeed = Eworld;
                    Estamina = Eworld + (int)(Eworld * 0.3f);
                    break;
                default:
                    Debug.Log("RivalStatBoostFunction was given a value out of its range.");
                    break;

        }
        currentHp = Estamina * 10;
        critChance = 0.05f;
        dodgeChance = 0.02f;
    }
}