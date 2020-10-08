//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CombatPetClass
{
    public int powerConversion, criticalEffectConversion, staminaConversion, speedConversion, chargeUp, chargeBalance, currentHp, specialDuration;
    public float critChance, dodgeChance;
    public bool opponent;
    public SpecialAttack specialAttack = new SpecialAttack();
    public void ConvertPetStats(MonsterClass baseStats)
    {
        int form = baseStats.monFormID;

        switch (form)
        {
            case 1:
                powerConversion = baseStats.monPower;
                criticalEffectConversion = baseStats.monCriticalEffect;
                speedConversion = baseStats.monSpeed;
                dodgeChance = 1f;
                critChance = 1f;
                chargeUp = 0;
                staminaConversion = baseStats.monStamina * 3;
                specialAttack.ChangeSpecialAttack(this, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f);
                break;
            case 2:
                powerConversion = baseStats.monPower;
                criticalEffectConversion = baseStats.monCriticalEffect;
                speedConversion = baseStats.monSpeed;
                dodgeChance = 2f;
                critChance = 2f;
                chargeUp = 10;
                staminaConversion = baseStats.monStamina * 6;
                specialAttack.ChangeSpecialAttack(this, 0.0f, 0.0f, 0.0f, 0.0f, 0.05f, 0.0f, 0.0f, 0.0f, 0.0f);
                break;
            case 3:
                powerConversion = baseStats.monPower + (int)(baseStats.monPower * 0.05f);
                criticalEffectConversion = baseStats.monCriticalEffect;
                speedConversion = baseStats.monSpeed;
                dodgeChance = 2f;
                critChance = 3f;
                chargeUp = 15;
                staminaConversion = baseStats.monStamina * 8;
                specialAttack.ChangeSpecialAttack(this, 0.0f, 0.0f, 0.05f, 0.1f, 0.05f, 0.0f, 0.0f, 0.0f, 0.0f);
                break;
            case 4:
                powerConversion = baseStats.monPower;
                criticalEffectConversion = baseStats.monCriticalEffect;
                speedConversion = baseStats.monSpeed + (int)(baseStats.monSpeed * 0.05f);
                dodgeChance = 4f;
                critChance = 2f;
                chargeUp = 12;
                staminaConversion = baseStats.monStamina * 10;
                specialAttack.ChangeSpecialAttack(this, 0.1f, 0.05f, 0.0f, 0.0f, 0.0f, 0.5f, 0.0f, 0.0f, 0.0f);
                break;
            case 5:
                powerConversion = baseStats.monPower + (int)(baseStats.monPower * 0.05f);
                criticalEffectConversion = baseStats.monCriticalEffect + (int)(baseStats.monCriticalEffect * 0.05f);
                speedConversion = baseStats.monSpeed;
                dodgeChance = 7f;
                critChance = 7f;
                chargeUp = 18;
                staminaConversion = baseStats.monStamina * 12;
                specialAttack.ChangeSpecialAttack(this, 0.0f, 0.0f, 0.05f, 0.1f, 0.15f, 0.0f, 0.0f, 0.0f, 0.0f);
                break;
            case 6:
                powerConversion = baseStats.monPower;
                criticalEffectConversion = baseStats.monCriticalEffect + (int)(baseStats.monCriticalEffect * 0.05f);
                speedConversion = baseStats.monSpeed + (int)(baseStats.monSpeed * 0.05f);
                dodgeChance = 7f;
                critChance = 4f;
                chargeUp = 12;
                staminaConversion = baseStats.monStamina * 15;
                specialAttack.ChangeSpecialAttack(this, 0.15f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.05f, 0.05f, 0.05f);
                break;
            case 7:
                powerConversion = baseStats.monPower;
                criticalEffectConversion = baseStats.monCriticalEffect + (int)(baseStats.monCriticalEffect * 0.05f);
                speedConversion = baseStats.monSpeed + (int)(baseStats.monSpeed * 0.05f);
                dodgeChance = 9f;
                critChance = 6f;
                chargeUp = 12;
                staminaConversion = baseStats.monStamina * 12;
                specialAttack.ChangeSpecialAttack(this, 0.0f, 0.1f, 0.0f, 0.0f, 0.0f, 0.1f, 0.0f, 0.0f, 0.1f);
                break;
            case 8:
                powerConversion = baseStats.monPower + (int)(baseStats.monPower * 0.05f);
                criticalEffectConversion = baseStats.monCriticalEffect;
                speedConversion = baseStats.monSpeed + (int)(baseStats.monSpeed * 0.05f);
                dodgeChance = 6f;
                critChance = 9f;
                chargeUp = 15;
                staminaConversion = baseStats.monStamina * 15;
                specialAttack.ChangeSpecialAttack(this, 0.2f, 0.0f, 0.05f, 0.1f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f);
                break;
            case 9:
                powerConversion = baseStats.monPower + (int)(baseStats.monPower * 0.1f);
                criticalEffectConversion = baseStats.monCriticalEffect + (int)(baseStats.monCriticalEffect * 0.05f);
                speedConversion = baseStats.monSpeed + (int)(baseStats.monSpeed * 0.05f);
                dodgeChance = 10f;
                critChance = 7f;
                chargeUp = 21;
                staminaConversion = baseStats.monStamina * 18;
                specialAttack.ChangeSpecialAttack(this, 0.25f, 0.0f, 0.0f, 0.15f, 0.25f, 0.0f, 0.0f, 0.15f, 0.2f);
                break;
            case 10:
                powerConversion = baseStats.monPower + (int)(baseStats.monPower * 0.05f);
                criticalEffectConversion = baseStats.monCriticalEffect + (int)(baseStats.monCriticalEffect * 0.05f);
                speedConversion = baseStats.monSpeed + (int)(baseStats.monSpeed * 0.1f);
                dodgeChance = 10f;
                critChance = 7f;
                chargeUp = 18;
                staminaConversion = baseStats.monStamina * 21;
                specialAttack.ChangeSpecialAttack(this, 0.0f, 0.2f, 0.2f, 0.2f, 0.0f, 0.2f, 0.0f, 0.0f, 0.2f);
                break;
            case 11:
                powerConversion = baseStats.monPower + (int)(baseStats.monPower * 0.1f);
                criticalEffectConversion = baseStats.monCriticalEffect + (int)(baseStats.monCriticalEffect * 0.05f);
                speedConversion = baseStats.monSpeed + (int)(baseStats.monSpeed * 0.05f);
                dodgeChance = 7f;
                critChance = 10f;
                chargeUp = 21;
                staminaConversion = baseStats.monStamina * 18;
                specialAttack.ChangeSpecialAttack(this, 0.0f, 0.0f, 0.0f, 0.0f, 0.25f, 0.0f, 0.25f, 0.25f, 0.25f);
                break;
            case 12:
                powerConversion = baseStats.monPower + (int)(baseStats.monPower * 0.08f);
                criticalEffectConversion = baseStats.monCriticalEffect + (int)(baseStats.monCriticalEffect * 0.07f);
                speedConversion = baseStats.monSpeed + (int)(baseStats.monSpeed * 0.05f);
                dodgeChance = 7f;
                critChance = 10f;
                chargeUp = 18;
                staminaConversion = baseStats.monStamina * 21;
                specialAttack.ChangeSpecialAttack(this, 0.0f, 0.15f, 0.15f, 0.2f, 0.2f, 0.3f, 0.0f, 0.0f, 0.0f);
                break;
            case 13:
                powerConversion = baseStats.monPower + (int)(baseStats.monPower * 0.05f);
                criticalEffectConversion = baseStats.monCriticalEffect + (int)(baseStats.monCriticalEffect * 0.1f);
                speedConversion = baseStats.monSpeed + (int)(baseStats.monSpeed * 0.05f);
                dodgeChance = 12f;
                critChance = 5f;
                chargeUp = 15;
                staminaConversion = baseStats.monStamina * 24;
                specialAttack.ChangeSpecialAttack(this, 0.3f, 0.0f, 0.1f, 0.0f, 0.2f, 0.0f, 0.2f, 0.0f, 0.2f);
                break;
            case 14:
                powerConversion = baseStats.monPower + (int)(baseStats.monPower * 0.05f);
                criticalEffectConversion = baseStats.monCriticalEffect + (int)(baseStats.monCriticalEffect * 0.1f);
                speedConversion = baseStats.monSpeed + (int)(baseStats.monSpeed * 0.05f);
                dodgeChance = 12f;
                critChance = 5f;
                chargeUp = 15;
                staminaConversion = baseStats.monStamina * 24;
                specialAttack.ChangeSpecialAttack(this, 0.3f, 0.0f, 0.1f, 0.0f, 0.2f, 0.0f, 0.2f, 0.0f, 0.2f);
                break;
            case 15:
                powerConversion = baseStats.monPower + (int)(baseStats.monPower * 0.06f);
                criticalEffectConversion = baseStats.monCriticalEffect + (int)(baseStats.monCriticalEffect * 0.06f);
                speedConversion = baseStats.monSpeed + (int)(baseStats.monSpeed * 0.06f);
                dodgeChance = 6f;
                critChance = 4f;
                chargeUp = 18;
                staminaConversion = baseStats.monStamina * 18;
                specialAttack.ChangeSpecialAttack(this, 0.25f, 0.25f, 0.0f, 0.25f, 0.0f, 0.25f, 0.0f, 0.0f, 0.0f);
                break;
            default:
                Debug.Log("Form ID not within range");
                break;
        }
        currentHp = staminaConversion;
    }
}