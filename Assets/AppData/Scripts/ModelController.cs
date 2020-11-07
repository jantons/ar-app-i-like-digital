using System.Collections;
using UnityEngine;
namespace ARExample
{
    public class ModelController : MonoBehaviour
    {
        Animator myAnimator;
        AnimatorClipInfo[] m_CurrentClipInfo;
        
        public float dirNum;
        Transform refPlayerTrans;

        bool isActive = false;

        bool isRightMove = false;
        bool isLeftMove = false;
        bool isFollow = false;
        bool CheckTurn = true;
        bool isTurn = false;
        public bool Turn { get => isTurn; }
        OSC_StreamingController oscController;
        

        // Start is called before the first frame update
        void Start()
        {
            myAnimator = gameObject.GetComponent<Animator>();

            SessionEvents.instance.onDangerZoneTriggered += invokeOnDanger;
            SessionEvents.instance.onSafeZoneTriggered += invokeOnSafeRegion;
            SessionEvents.instance.onFollowTriggered += invokeOnFollow;
            SessionEvents.instance.onUnFollowTriggered += invokeOnUnFollow;
            oscController = OSC_StreamingController.instance;
        }

        // Update is called once per frame
        void Update()
        {
            if (isActive)
            {
                Vector3 heading = refPlayerTrans.position - transform.position;
                dirNum = AngleDir(transform.forward, heading, transform.up);

                if (isFollow)
                {
                    Vector3 toTarget = (refPlayerTrans.position - transform.position).normalized;

                    if (Vector3.Dot(toTarget, transform.forward) > 0)
                    {
                        Debug.Log("Target is in front of this game object.");
                    }
                    else
                    {
                        Debug.Log("Target is not in front of this game object.");
                    }
                    myAnimator.SetFloat("BlendY", 1f);
                }

                if (CheckTurn)
                {
                    CheckTurn = false;
                    CheckDirection();
                    StopCoroutine("resetCheckTurn");
                    StartCoroutine(resetCheckTurn(.25f));
                }
            }
            oscController.SendLocation_XZ(transform.position.x, transform.position.z);
        }

        IEnumerator resetCheckTurn(float delay)
        {
            yield return new WaitForSeconds(delay);
           CheckTurn = true;

        }
        public void SetModelControllerState(bool state,Transform refPTrans)
        {
            isActive = state;
            refPlayerTrans = refPTrans;
        }
        public void SetModelControllerState(bool state)
        {
            isActive = state;
        }

        void CheckDirection()
        {
            var targetRelative = transform.InverseTransformPoint(refPlayerTrans.transform.position);

            if (targetRelative.x > 2.5)
            {
                myAnimator.SetFloat("BlendX", .75f);
                Debug.Log(targetRelative.x+"Right");
            }
            else if (targetRelative.x < -2.5)
            {
                myAnimator.SetFloat("BlendX", -.75f);
                Debug.Log(targetRelative.x + "Left");
            }
            else
            {
                isTurn = false;
                myAnimator.SetFloat("BlendX", 0f);
            }
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

        public void OnReceivePush15(OscMessage message)
        {
            myAnimator.SetBool("isAuto", true);
            myAnimator.StopPlayback();
            myAnimator.Play("state1", -1, 0);
            Debug.Log(message.GetFloat(0));
        }
        public void OnReceiveStop1(OscMessage message)
        {
            myAnimator.SetBool("isAuto", false);
            myAnimator.StopPlayback();
            myAnimator.Play("state1", -1, 0);
            Debug.Log(message.GetFloat(0));
        }

        public void OnReceiveStop2(OscMessage message)
        {
            myAnimator.SetBool("isAuto", false);
            myAnimator.StopPlayback();
            myAnimator.Play("state2", -1, 0);
            Debug.Log(message.GetFloat(0));
        }

        public void OnReceiveStop3(OscMessage message)
        {
            myAnimator.SetBool("isAuto", false);
            myAnimator.StopPlayback();
            myAnimator.Play("state3", -1, 0);
            Debug.Log(message.GetFloat(0));
        }

        public void OnReceiveStop4(OscMessage message)
        {
            myAnimator.SetBool("isAuto", false);
            myAnimator.StopPlayback();
            myAnimator.Play("state4", -1, 0);
            Debug.Log(message.GetFloat(0));
        }

        public void OnReceiveStop5(OscMessage message)
        {
            myAnimator.SetBool("isAuto", false);
            myAnimator.StopPlayback();
            myAnimator.Play("state5", -1, 0);
            Debug.Log(message.GetFloat(0));
        }

        public void OnReceiveStop6(OscMessage message)
        {
            myAnimator.SetBool("isAuto", false);
            myAnimator.StopPlayback();
            myAnimator.Play("state6", -1, 0);
            Debug.Log(message.GetFloat(0));
        }
    }
}