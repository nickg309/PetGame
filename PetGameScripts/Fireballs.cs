//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class Fireballs : MonoBehaviour
{
    public GameObject pet, target;
    int petForm;
    Vector2 start;
    public RuntimeAnimatorController Fireball1, Fireball2, Fireball3, Fireball4, Fireball5, Fireball6, Fireball7;
    Animator animator;
    public float speed;

    private void OnEnable()
    {
        animator = gameObject.GetComponent<Animator>();
        start = gameObject.transform.localPosition;
        petForm = pet.GetComponent<PetMonster>().myPetMonster.monFormID;
        FireballSwitch();
    }
    public void KillMyFireBalls()
    {
        gameObject.transform.localPosition = start;
        gameObject.SetActive(false);
    }
    private void Update()
    {
        if (transform.position.x <= target.transform.position.x)
        {
            gameObject.transform.localPosition = start;
            gameObject.SetActive(false);
        }
        else
        {
            transform.Translate(Vector2.left * speed);
        }
    }

    public void FireballSwitch()
    {
        switch (petForm)
        {
            case 1:
                animator.runtimeAnimatorController = Fireball6;
                break;

            case 2:
                animator.runtimeAnimatorController = Fireball7;

                break;

            case 3:
                animator.runtimeAnimatorController = Fireball1;

                break;

            case 4:
                animator.runtimeAnimatorController = Fireball3;

                break;

            case 5:
                animator.runtimeAnimatorController = Fireball2;

                break;

            case 6:
                animator.runtimeAnimatorController = Fireball4;

                break;

            case 7:
                animator.runtimeAnimatorController = Fireball5;

                break;

            case 8:
                animator.runtimeAnimatorController = Fireball5;

                break;

            case 9:
                animator.runtimeAnimatorController = Fireball3;

                break;

            case 10:
                animator.runtimeAnimatorController = Fireball7;

                break;

            case 11:
                animator.runtimeAnimatorController = Fireball6;

                break;

            case 12:
                animator.runtimeAnimatorController = Fireball4;

                break;

            case 13:
                animator.runtimeAnimatorController = Fireball6;

                break;

            case 14:
                animator.runtimeAnimatorController = Fireball2;

                break;

            case 15:
                animator.runtimeAnimatorController = Fireball1;

                break;

            default:
                animator.runtimeAnimatorController = Fireball1;

                break;
        }

    }
}
