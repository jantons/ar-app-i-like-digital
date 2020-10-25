using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ModelController : MonoBehaviour
{ 
    Animator myAnimator;
    AnimatorClipInfo[] m_CurrentClipInfo;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = gameObject.GetComponent<Animator>();
  
        SessionEvents.instance.onDangerZoneTriggered += invokeOnDanger;
        SessionEvents.instance.onSafeZoneTriggered += invokeOnSafeRegion;
    }

    // Update is called once per frame
    void Update()
    {
        myAnimator.SetFloat("BlendY", Input.GetAxisRaw("Vertical"));
        myAnimator.SetFloat("BlendX", Input.GetAxisRaw("Horizontal"));
    }
    void invokeOnDanger()
    { 
        if (!myAnimator.GetBool("isAttack") && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack_StandAngry_01_Low")) //Not attacking
        {
            Debug.Log("In Danger");
            myAnimator.SetBool("isAttack", true); // Set Attacking
            myAnimator.Play("Attack_StandAngry_01_Low", -1,0);
            
            StartCoroutine( resetFlag(getRandomDelay(getCurrentAnimLength()), "isAttack"));
        }
    }
    float getRandomDelay(float bufferDelay)
    {
        return bufferDelay+ UnityEngine.Random.Range(1f, 5f); // This will add random respond time.
    }
    float getCurrentAnimLength()
    {
        //Fetch the current Animation clip information for the base layer
        m_CurrentClipInfo = this.myAnimator.GetCurrentAnimatorClipInfo(0);
        if (m_CurrentClipInfo.Length > 0)
            //Access the current length of the clip
            return m_CurrentClipInfo[0].clip.length;
        else
            return 0f;
    }
    IEnumerator resetFlag(float delay, string flag)
    {
        yield return new WaitForSeconds(delay);
        myAnimator.SetBool(flag, !myAnimator.GetBool(flag));

    }
    void invokeOnSafeRegion()
    {
        myAnimator.SetBool("isAttack", false);
    }


}
