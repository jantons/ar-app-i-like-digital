using System.Collections;
using UnityEngine;
namespace ARExample
{
    public class ModelController : MonoBehaviour
    {
        [SerializeField] bool SelfReposition = false;
        [SerializeField] bool assis_follow = false; // assis_follow : True = Follow user and maintain safe distance and viceversa
        [SerializeField] bool attackMode = false;
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
        public float SafeZoneLimit { get => safeZone; set => safeZone = value; }
        public float DangerZone { get => dangerZone; set => dangerZone = value; }
        public bool Assis_follow { get => assis_follow; set => assis_follow = value; }
        public bool SelfReposition1 { get => SelfReposition; set => SelfReposition = value; }
        public bool AttackMode { get => attackMode; set => attackMode = value; }

        OSC_Send osc_OutletStreamingController;
        StateStreamOutlet osc_stateStreamOutlet;

        float limit_X, limit_Z;
        [SerializeField] float safeZone = 2f;
        [SerializeField] float dangerZone = 1f;
        [SerializeField] float startfollowLimit = 4f;
        [SerializeField] float stopfollowLimit = 3f;

        ARManager _arManager;

        // Start is called before the first frame update
        void Awake()
        {
            myAnimator = GetComponent<Animator>();

            SessionEvents.instance.onDangerZoneTriggered += invokeOnDanger;
            SessionEvents.instance.onSafeZoneTriggered += invokeOnSafeRegion;
            SessionEvents.instance.onFollowTriggered += invokeOnFollow;
            SessionEvents.instance.onUnFollowTriggered += invokeOnUnFollow;

            osc_OutletStreamingController = OSC_Send.Instance;
            osc_stateStreamOutlet = StateStreamOutlet.Instance;

            // Get boundaries
            limit_X = ARManager.Instance.R_width_X;
            limit_Z = ARManager.Instance.R_length_Z;

            _arManager = ARManager.Instance;
        }

        // Update is called once per frame
        void Update()
        {
            if (isActive)
            {
                    if (!checkBoundary())
                    {
                        setModelIdle();
                    }
                stram_OSC_Messages();
            }
        }
        void stram_OSC_Messages()
        {
            // Stream Position
            osc_OutletStreamingController.StreamMessage("/locationX/", transform.position.x);
            osc_OutletStreamingController.StreamMessage("/locationZ/", transform.position.z);

            // osc_OutletStreamingController.StreamMessage("/PhonelocationX/", _arManager.Player.transform.position.x);
            // osc_OutletStreamingController.StreamMessage("/PhonelocationZ/", _arManager.Player.transform.position.z);
            osc_OutletStreamingController.StreamMessage("/PhonelocationX/", Camera.main.transform.position.x);
            osc_OutletStreamingController.StreamMessage("/PhonelocationZ/", Camera.main.transform.position.z);
            osc_OutletStreamingController.StreamMessage("/PhoneRotationX/", Camera.main.transform.rotation.x);
            osc_OutletStreamingController.StreamMessage("/PhoneRotationY/", Camera.main.transform.rotation.y);
            osc_OutletStreamingController.StreamMessage("/PhoneRotationZ/", Camera.main.transform.rotation.z);
            osc_OutletStreamingController.StreamMessage("/Distance/", Vector3.Distance(Camera.main.transform.position, transform.position));
        }

        void setModelIdle()
        {
            // Set idle
                myAnimator.SetBool("isAttack", false);
                myAnimator.SetFloat("BlendX", 0f);
                myAnimator.SetFloat("BlendY", 0f);
            if(SelfReposition) // if user has selected to reposition on boundary detection
                resetPosition();
            RotateToCamera();
                myAnimator.Play("RestState", -1, 0);
        }


        #region Position and Direction Estimation
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
        void CheckDirection()
        {
            var targetRelative = transform.InverseTransformPoint(refPlayerTrans.transform.position);

            if (targetRelative.x > 2.5)
            {
                myAnimator.SetFloat("BlendX", .75f);
                Debug.Log(targetRelative.x + "Right");
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
        /// <summary>
        ///  Reset position of model in case if it reaches to boundary
        /// </summary>
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
        #endregion
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
        
        public void SetModelControllerState(bool state,Transform refPTrans)
        {
            isActive = state;
            refPlayerTrans = refPTrans;
        }
        #region Proximity Calculations
        public void SetModelControllerState(bool state)
        {
            isActive = state;
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
        #endregion

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

        #region OSC Animation Controll

        public void OnReceiveAction_Auto()
        {
            setState("state1", true);
        }
        public void OnReceiveState_1()
        {
            setState("state1", false);
        }

        public void OnReceiveState_2()
        {
            setState("state2", false);
        }

        public void OnReceiveState_3()
        {
            setState("state3", false);
        }

        public void OnReceiveState_4()
        {
            setState("state4", false);
        }

        public void OnReceiveState_5()
        {
            setState("state5", false);
        }

        void setState(string myState , bool autoState)
        {
            myAnimator.SetBool("isAuto", autoState);

            myAnimator.StopPlayback();
            osc_stateStreamOutlet.streamStateEnd();
            myAnimator.Play(myState, -1, 0);
        }

        #endregion

    }
}