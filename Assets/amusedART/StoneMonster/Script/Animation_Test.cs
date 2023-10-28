using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Test : MonoBehaviour {

	public const string IDLE	= "Anim_Idle";
	public const string RUN		= "Anim_Run";
	public const string ATTACK	= "Anim_Attack";
	public const string DAMAGE	= "Anim_Damage";
	public const string DEATH	= "Anim_Death";
	public bool attackMode = false;
	public float health = 100f;
	public bool isDead = false;
	public bool isAttack = false;
	public float AttackTime = 2f;



	public GameObject player;

	Animation anim;

	void Start () {

		anim = GetComponent<Animation>();
		StartPatern();
	}

    private void Update()
    {
		this.transform.LookAt(player.transform.position);
		
		if(isDead)
        {
			StopAllCoroutines();
			DeathAni();
			Destroy(this.gameObject);
		}
		
	}


	IEnumerator AttackPattern()
    {
		
				IdleAni();
				attackMode = false;
				yield return new WaitForSeconds(AttackTime);
		        StartCoroutine(AttackPattern2());
		
		

	}


	IEnumerator AttackPattern2()
    {

		
				AttackAni();
				attackMode = true;
				yield return new WaitForSeconds(0);
				StartCoroutine(AttackPattern());
		
	
		


	}

    public void IdleAni (){
		
		anim.CrossFade (IDLE);
		
	}

	public void RunAni (){
		anim.CrossFade (RUN);
	}

	public void AttackAni (){
	
		anim.CrossFade (ATTACK);
		

	}

	public void DamageAni (){
		anim.CrossFade (DAMAGE);
	}

	public void DeathAni (){
		anim.CrossFade (DEATH);
	}

	public void StartPatern()
    {
		StartCoroutine(AttackPattern());
	}

}
