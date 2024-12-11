using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class FPPMvmtGameMovement : MonoBehaviour
{

    public GameObject targetCamera;
    public Rigidbody phys;
    public CharacterController ccontr;
    public Animator retryAnim;
    public Animator levelTransitionAnimator;
    public TextMeshProUGUI levelTransitionText;
    public GameObject dofPostProcess;
    public GameObject interactPrompt;

    public TimeManager timeManager;
    public PlayerUI ui;
    public bool timeStopped = false;
    public bool stageStopped = false;
    
    public float movementSpeed = 10f;
    private Vector2 gamepadSensitivity = new Vector2(200f, 150f);

    public Clickable currentActiveClickable = null;

    [Header("Debugs")]

    public float rotX = 0;
    public float rotY = 0;

    public Vector3 Velocity = new Vector3(0,0,0);
    public Vector3 walkVelocity = new Vector3(0,0,0);
    public Vector3 actWalkVelocity = new Vector3(0,0,0);
    public Lockable<bool> canWallJump = new Lockable<bool>(false);
    public bool attachedToWall = false;
    public bool grounded = true;
    public MovableOverTime groundedTo = null;

    public const float altGravityVal = -5.81f;
    public const float gravityVal = -16.81f;

    private LevelProperties thisLevelProperties;

    private Vector3[] rts = new Vector3[] {
        new Vector3(-1,0,0),
        new Vector3(-1,0,1),
        new Vector3(0,0,1),
        new Vector3(1,0,1),
        new Vector3(1,0,0),
        new Vector3(1,0,-1),
        new Vector3(0,0,-1),
        new Vector3(-1,0,-1),
    };

    public void SetPosition(Vector3 pos)
    {
        GetComponent<CharacterController>().enabled = false;
        transform.position = pos;
        GetComponent<CharacterController>().enabled = true;
    }

    KeyValuePair<bool, RaycastHit> RTAndDebug(Vector3 from, Vector3 dir, float distance)
    {
        bool ret = Physics.Raycast(from, dir, out RaycastHit hit, distance, LayerMask.GetMask("Default"));
        Debug.DrawRay(from, dir*distance, Color.red);
        return new KeyValuePair<bool, RaycastHit>(ret, hit);
    }

    private void Awake()
    {
        levelTransitionAnimator.gameObject.SetActive(true);
        if (CrossScene.levelName != null)
        {
            levelTransitionText.text = CrossScene.levelName;
            levelTransitionAnimator.Play("animLevelTransitionReverse", -1, 0);
        }
    }

    void Start()
    {

        var levelProps = GameObject.FindObjectsOfType<LevelProperties>();
        if (levelProps.Any()) {
            thisLevelProperties = levelProps.First();
            if (thisLevelProperties.bgm != null) {
                AudioSource auds = transform.AddComponent<AudioSource>();
                auds.clip = thisLevelProperties.bgm;
                auds.loop = true;
                auds.Play();
            }
        }

        if (targetCamera == null)
        {
            targetCamera = GetComponentInChildren<Camera>().gameObject;
        }   
        /*if (phys == null)
        {
            phys = GetComponent<Rigidbody>();
            //phys.mass = 12;
        }*/
        if (ccontr == null)
        {
            ccontr = GetComponent<CharacterController>();
        } 
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR_WIN
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
        }
#endif

        if (!timeStopped) {
            var groundedToV = RTAndDebug(transform.position, -transform.up, 1.3f);
            grounded = groundedToV.Key;
            if (grounded) {
                groundedToV.Value.transform.TryGetComponent<MovableOverTime>(out groundedTo);
            }
        }
        if (grounded && groundedTo != null) {
            ccontr.Move(groundedTo.deltaMovement);
        }

        attachedToWall = false;
        //wall detection
        foreach (Vector3 r in rts)
        {
            if (RTAndDebug(transform.position, r, 0.8f).Key)
            {
                attachedToWall = true;
                break;
            }
        }

        rotX += Input.GetAxis("Mouse X") + Mathf.Pow(Input.GetAxis("Gamepad RSX"),5) * gamepadSensitivity.x * Time.deltaTime;
        rotY = Mathf.Clamp(rotY + -(Input.GetAxis("Mouse Y") + Mathf.Pow(Input.GetAxis("Gamepad RSY"),3) * gamepadSensitivity.y * Time.deltaTime), -90, 90);
        Util.ModifyTransformX(targetCamera.transform, rotY);
        Util.ModifyTransformY(this.transform, rotX);

        walkVelocity = new Vector3(0, 0, 0);
        float realMovementSpeed = movementSpeed * (attachedToWall ? 0.1f : 1);
        //ccontr.Move(targetCamera.transform.forward * (Input.GetKey(FwdKey) ? realMovementSpeed : 0));
        /*ccontr.Move(-targetCamera.transform.right * (Input.GetKey(LeftKey) ? realMovementSpeed : 0) * 0.6f);
        ccontr.Move(targetCamera.transform.right * (Input.GetKey(RightKey) ? realMovementSpeed : 0) * 0.6f);
        ccontr.Move(-targetCamera.transform.forward * (Input.GetKey(BckwdKey) ? realMovementSpeed : 0));*/
        //print(Input.GetAxis("Vertical"));
        walkVelocity += (Input.GetAxis("Vertical") * transform.forward + Input.GetAxis("Horizontal") * transform.right) * movementSpeed;

        timeStopped = stageStopped = Input.GetButton("StopTime");
        if (!stageStopped)
        {
            stageStopped = Input.GetButton("StopStage");
        }

        dofPostProcess.SetActive((thisLevelProperties == null || !thisLevelProperties.dofDisabled) && (timeStopped || stageStopped));
        //ccontr.Move(-transform.up * Time.deltaTime * 8);

        if (grounded && Velocity.y < 0)
        {
            Velocity.y = 0;
        }

        if (!timeStopped)
        {
            if (!attachedToWall || grounded)
            {

                canWallJump.Unlock();
            }
            canWallJump.Value = attachedToWall && !grounded;

            if (Input.GetButtonDown("Jump") && (grounded || canWallJump.Value))
            {
                Velocity.y = 0;
                Velocity.y += Mathf.Sqrt(2 * -3.0f * gravityVal);
                Velocity.x = walkVelocity.x;
                Velocity.z = walkVelocity.z;
                canWallJump.Value = false;
                canWallJump.Lock();
            }


            Velocity.y += (attachedToWall && canWallJump.Value ? (Velocity.y > 0 ? altGravityVal * 4 : altGravityVal/1.5f) : gravityVal) * Time.deltaTime;

            bool bangingOnCeil = RTAndDebug(transform.position, transform.up, 1.3f).Key;
            if (bangingOnCeil && Velocity.y > 0){
                Velocity.y *= -0.5f;
            }

            //ccontr.Move(Vector3.up * Velocity.y * Time.deltaTime);
            ccontr.Move(Vector3.up * Velocity.y * Time.deltaTime);
            ccontr.Move(Vector3.Scale(new Vector3(1, 0, 1), Velocity) * Time.deltaTime * 1.7f);
            Velocity -= Vector3.Scale(new Vector3(1, 0, 1), Velocity) * Time.deltaTime * 1.7f;

            Vector3 actualTargetWalkVelocity = walkVelocity * (!grounded ? (attachedToWall && canWallJump.Value ? 0.02f : 0.1f) : 1);

            Vector3 diff = (actualTargetWalkVelocity - actWalkVelocity) * Time.deltaTime * 6;
            actWalkVelocity += diff;

            ccontr.Move(actWalkVelocity * Time.deltaTime);
        }

        var clickableRT = RTAndDebug(targetCamera.transform.position, targetCamera.transform.forward, 200);
        if (clickableRT.Key && clickableRT.Value.transform.TryGetComponent<Clickable>(out Clickable foundClickable))
        {
            currentActiveClickable = foundClickable;
        } 
        else {
            currentActiveClickable = null;
        }

        interactPrompt.SetActive(currentActiveClickable != null);

        if (Input.GetButtonDown("Interact")) {
            if (currentActiveClickable != null) {
                currentActiveClickable.Click();
            }
        }

        if (timeStopped && !Input.GetKey(KeyCode.LeftAlt))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void PlayRetryAnimation()
    {
        retryAnim.gameObject.SetActive(true);
        retryAnim.Play("animPlayerRetry", -1, 0);
    }
}
