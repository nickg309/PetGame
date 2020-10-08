//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    public RuntimeAnimatorController rivalForm, bossForm;
    public Animator opponentAnim;
    public void SetRivalAnim()
    {
        gameObject.GetComponent<Animator>().runtimeAnimatorController = rivalForm;
    }
    public void SetBossAnim() 
    {
        gameObject.GetComponent<Animator>().runtimeAnimatorController = bossForm;
    }
    public void EnemyAttackAnim()
    {
        opponentAnim.SetTrigger("Attack");
    }
    public void EnemyStopAttackAnim()
    {
        opponentAnim.SetBool("Attack", false);
    }
}