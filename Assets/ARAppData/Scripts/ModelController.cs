using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace ARExample
{
    public class ModelController : MonoBehaviour
    {
        Animator myAnimator;
        AnimatorClipInfo[] m_CurrentClipInfo;
        bool isFollow;
        public Transform target;
        public float dirNum;
        Transform refPlayerTrans;
        // Start is called before the first frame update
        void Start()
        {
            myAnimator = gameObject.GetComponent<Animator>();
            refPlayerTrans = gameObject.GetComponent<ProximityController>().GetPlayerTrans;

            SessionEvents.instance.onDangerZoneTriggered += invokeOnDanger;
            SessionEvents.instance.onSafeZoneTriggered += invokeOnSafeRegion;
            SessionEvents.instance.onFollowTriggered += invokeOnFollow;
            SessionEvents.instance.onUnFollowTriggered += invokeOnUnFollow;
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 heading = refPlayerTrans.position - refPlayerTrans.position;
            dirNum = AngleDir(transform.forward, heading, transform.up);

            if (isFollow)
            {
                myAnimator.SetFloat("BlendY", 1f);
            }

            if (dirNum != 0)
            {
                if (dirNum > 0)
                {
                    myAnimator.SetFloat("BlendX", +1);
                }
                else
                {
                    myAnimator.SetFloat("BlendX", -1);
                }
            }
            else
            {
                myAnimator.SetFloat("BlendX", 0);
            }
            
        }

        void invokeOnSafeRegion()
        {
            myAnimator.SetBool("isAttack", false);
        }

        private void invokeOnFollow()
        {
            myAnimator.SetBool("isMove", true);
            isFollow = true;
        }
        private void invokeOnUnFollow()
        {
            myAnimator.SetBool("isMove", false);
            isFollow = false;
            myAnimator.SetFloat("BlendY", 0f);
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

        IEnumerator resetFlag(float delay, string flag)
        {
            yield return new WaitForSeconds(delay);
            myAnimator.SetBool(flag, !myAnimator.GetBool(flag));

        }

        float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
        {
            Vector3 perp = Vector3.Cross(fwd, targetDir);
            float dir = Vector3.Dot(perp, up);

            if (dir > 0f)
            {
                return 1f;
            }
            else if (dir < 0f)
            {
                return -1f;
            }
            else
            {
                return 0f;
            }
        }


}
}