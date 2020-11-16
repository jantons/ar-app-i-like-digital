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
        /// <summary>
        /// assis_follow : True = Follow user and maintain safe distance and viceversa
        /// </summary>
        bool assis_follow = false;
        public float SafeZoneLimit { get => safeZone; set => safeZone = value; }
        public float DangerZone { get => dangerZone; set => dangerZone = value; }
        public bool Assis_follow { get => assis_follow; set => assis_follow = value; }

        OSC_StreamingController oscController;
        StateStreamOutlet osc_stateStreamOutlet;

        float limit_X, limit_Z;
        [SerializeField] float safeZone = 2f;
        [SerializeField] float dangerZone = 1f;
        [SerializeField] float startfollowLimit = 4f;
        [SerializeField] float stopfollowLimit = 3f;





        // Start is called before the first frame update
        void Start()
        {
            myAnimator = gameObject.GetComponent<Animator>();

            SessionEvents.instance.onDangerZoneTriggered += invokeOnDanger;
            SessionEvents.instance.onSafeZoneTriggered += invokeOnSafeRegion;
            SessionEvents.instance.onFollowTriggered += invokeOnFollow;
            SessionEvents.instance.onUnFollowTriggered += invokeOnUnFollow;

            oscController = OSC_StreamingController.instance;
            osc_stateStreamOutlet = StateStreamOutlet.instance;

            // Get boundaries
            limit_X = ARManager.Instance.R_width_X;
            limit_Z = ARManager.Instance.R_length_Z;

        }

        // Update is called once per frame
        void Update()
        {
            if (isActive)
            {
                //PositionEstimation();
            }

            if (isActive)
            {
 
                if (!checkBoundary())
                {
                    setModelIdle();
                }
            }
        }
        void setModelIdle()
        {
            // Set idle
                myAnimator.SetBool("isAttack", false);
                myAnimator.SetFloat("BlendX", 0f);
                myAnimator.SetFloat("BlendY", 0f);
                resetPosition();
            RotateToCamera();
                myAnimator.Play("RestState", -1, 0);
            Debug.Log("ResetPosition");
        }

        void resetPosition()
        {
            if (transform.position.x > 0) // Ist or IV qd, 
            {
                if (transform.position.z > 0)
                {
                    //Ist qd
                    transform.position = new Vector3(SafeZoneLimit, transform.position.y, SafeZoneLimit);
                }
                else
                {
                    //IV qd
                    transform.position = new Vector3(SafeZoneLimit, transform.position.y, -SafeZoneLimit);
                }
            }
            else
            {
                if (transform.position.z > 0)
                {
                    //II qd
                    transform.position = new Vector3(-SafeZoneLimit, transform.position.y, SafeZoneLimit);
                }
                else
                {
                    //III qd
                    transform.position = new Vector3(-SafeZoneLimit, transform.position.y, -SafeZoneLimit);
                }
            }
        }
        public void RotateToCamera()
        {
            Vector3 targetPostition = new Vector3(Camera.main.transform.position.x,
                                       this.transform.position.y,
                                       Camera.main.transform.position.z);
            this.transform.LookAt(targetPostition);
        }
        bool checkBoundary()
        {
            if (Mathf.Abs(transform.position.x) < limit_X && Mathf.Abs(transform.position.z) < limit_Z)
                return true;
            else
                return false;
        }
        private void PositionEstimation()
        {
            Vector3 heading = refPlayerTrans.position - transform.position;
            dirNum = AngleDir(transform.forward, heading, transform.up);

            if (isFollow)
            {
                Vector3 toTarget = (refPlayerTrans.position - transform.position).normalized;

                if (Vector3.Dot(toTarget, transform.forward) > 0)
                {
                    // Debug.Log("Target is in front of this game object.");
                }
                else
                {
                    // Debug.Log("Target is not in front of this game object.");
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
        string getCurrentAnimName()
        {
            //Fetch the current Animation clip information for the base layer
            m_CurrentClipInfo = this.myAnimator.GetCurrentAnimatorClipInfo(0);
            if (m_CurrentClipInfo.Length > 0)
                //Access the current length of the clip
                return m_CurrentClipInfo[0].clip.name;
            else
                return "";
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

        public void OnReceivePush1(OscMessage message)
        {
            myAnimator.SetBool("isAuto", true);
            myAnimator.StopPlayback();
            myAnimator.Play("state10", -1, 0);

            Debug.Log("Pressed"+message.GetFloat(0));
        }
        public void OnReceiveStop10(OscMessage message)
        {
            myAnimator.SetBool("isAuto", false);

            myAnimator.StopPlayback();
            myAnimator.Play("state10", -1, 0);

            Debug.Log(message.GetFloat(0));
        }

        public void OnReceiveStop11(OscMessage message)
        {
            myAnimator.SetBool("isAuto", false);

            myAnimator.StopPlayback();
            osc_stateStreamOutlet.streamStateEnd();
            myAnimator.Play("state11", -1, 0);

            Debug.Log(message.GetFloat(0));
        }

        public void OnReceiveStop12(OscMessage message)
        {
            myAnimator.SetBool("isAuto", false);

            myAnimator.StopPlayback();
            osc_stateStreamOutlet.streamStateEnd();
            myAnimator.Play("state12", -1, 0);

            Debug.Log(message.GetFloat(0));
        }

        public void OnReceiveStop13(OscMessage message)
        {
            myAnimator.SetBool("isAuto", false);

            myAnimator.StopPlayback();
            osc_stateStreamOutlet.streamStateEnd();
            myAnimator.Play("state13", -1, 0);

            Debug.Log(message.GetFloat(0));
        }

        public void OnReceiveStop14(OscMessage message)
        {
            myAnimator.SetBool("isAuto", false);

            myAnimator.StopPlayback();
            osc_stateStreamOutlet.streamStateEnd();
            myAnimator.Play("state14", -1, 0);

            Debug.Log(message.GetFloat(0));
        }

    }
}