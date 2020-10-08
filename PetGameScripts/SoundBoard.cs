using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBoard : MonoBehaviour
{

    public AudioSource buttonSoundBoard, battleSoundBoard, eatDrinkSoundBoard, distressSoundBoard;
    public AudioClip buttonBeep, hit, miss, eatDrink, distressCall;


    public void ButtonBeep()
    {
        buttonSoundBoard.clip = buttonBeep;
        buttonSoundBoard.Play();
    }

    public void attackHit()
    {
        battleSoundBoard.clip = hit;
        battleSoundBoard.Play();
    }

    public void attackMiss()
    {
        battleSoundBoard.clip = miss;
        battleSoundBoard.Play();
    }

    public void EatDrink()
    {
        eatDrinkSoundBoard.clip = eatDrink;
        eatDrinkSoundBoard.Play();
    }

    public void DistressCall()
    {
        distressSoundBoard.clip = distressCall;
        distressSoundBoard.Play();
    }

}