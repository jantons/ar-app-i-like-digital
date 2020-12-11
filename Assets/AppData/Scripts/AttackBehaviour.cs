using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARExample;

public class AttackBehaviour : MonoBehaviour
{
    Animator myAnimator;
    AnimatorClipInfo[] m_CurrentClipInfo;
    bool isFollow = false;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        SessionEvents.instance.onDangerZoneTriggered += invokeOnDanger;
        SessionEvents.instance.onSafeZoneTriggered += invokeOnSafeRegion;
        SessionEvents.instance.onFollowTriggered += invokeOnFollow;
        SessionEvents.instance.onUnFollowTriggered += invokeOnUnFollow;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void invokeOnSafeRegion()
    {
        myAnimator.SetBool("isAttack", false);
    }

    private void invokeOnFollow()
    {
        if (!isFollow)
        {
            isFollow = true;
            myAnimator.SetFloat("BlendY", 1f);
            myAnimator.SetBool("isRest", false);

        }
    }
    private void invokeOnUnFollow()
    {
        if (isFollow)
        {
            isFollow = false;
            myAnimator.SetFloat("BlendY", 0f);
        }
    }
    void invokeOnDanger()
    {
        if (!myAnimator.GetBool("isAttack") && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack_StandAngry_01_Low")) //Not attacking
        {
            myAnimator.SetBool("isAttack", true); // Set Attacking
            myAnimator.Play("Attack_StandAngry_01_Low", -1, 0);

            StartCoroutine(resetFlag(getRandomDelay(getCurrentAnimLength()), "isAttack"));
        }
    }
    IEnumerator resetFlag(float delay, string flag)
    {
        yield return new WaitForSeconds(delay);
        myAnimator.SetBool(flag, !myAnimator.GetBool(flag));

    }
    float getRandomDelay(float bufferDelay)
    {
        return bufferDelay + UnityEngine.Random.Range(1f, 5f); // This will add random respond time.
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
}
