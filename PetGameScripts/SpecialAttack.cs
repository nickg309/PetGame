/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;*/
[System.Serializable]

public class SpecialAttack
{
    public int specialDamage = 0, specialHeal = 0, specialHoT, specialDodge = 0, specialCriticalEffect = 0, specialCriticalChance = 0, specialPower = 0, specialSpeed = 0, specialDuration = 0;
    public float specialShield = 0, specialReflect = 0;
    public void ChangeSpecialAttack(CombatPetClass combatPetClass, float healingBoost, float dodgeBoost, float critEffectBoost, float critChanceBoost, float powerBoost, float speedBoost, float shieldEffect, float reflectDamage, float healOverTurn)
    {
        specialHoT = (int)(combatPetClass.staminaConversion * healOverTurn);
        specialHeal = (int)(combatPetClass.powerConversion + (combatPetClass.powerConversion * healingBoost));
        specialDodge = (int)(combatPetClass.dodgeChance + (combatPetClass.dodgeChance * dodgeBoost));
        specialCriticalEffect = (int)(combatPetClass.criticalEffectConversion + (combatPetClass.criticalEffectConversion * critEffectBoost));
        specialCriticalChance = (int)(combatPetClass.critChance + (combatPetClass.critChance * critChanceBoost));
        specialPower = (int)(combatPetClass.powerConversion + (combatPetClass.powerConversion * powerBoost));
        specialSpeed = (int)(combatPetClass.speedConversion + (combatPetClass.speedConversion * speedBoost));
        specialDuration = 5;
        specialShield = shieldEffect;
        specialReflect = reflectDamage;
    }
}
