using UnityEngine;

public class NPC_AnimationEventReceiver : MonoBehaviour
{
    GameObject MainGameObject;
    Animator animator;
    RuntimeAnimatorController runtimeAnimatorController;


    #region NPC State

    [HideInInspector] public bool isTurningRight;
    [HideInInspector] public bool isTurningLeft;
    [HideInInspector] public bool isSitting;
    [HideInInspector] public bool isStanding;

    #endregion


    #region Rotating Properties

    private Quaternion startRotation;
    private Quaternion targetRotation;
    private float turnSpeed;
    bool didSetRotatingProperties = false;

    #endregion


    #region Sitting and Standing Properties

    private float moveTimer;
    private float animationLength;
    private Vector3 startPosition;
    private Vector3 targetPosition;

    public AnimationCurve sittingCurve = new AnimationCurve(
        new Keyframe(0.0f, 0.0f),
        new Keyframe(0.5f, 0.5f),
        new Keyframe(0.8f, 1f),
        new Keyframe(1f, 1f));

    public AnimationCurve standingCurve = new AnimationCurve(
    new Keyframe(0.0f, 0.0f),
    new Keyframe(0.3f, 0.1f),
    new Keyframe(0.5f, 0.4f),
    new Keyframe(0.9f, 1.0f),
    new Keyframe(1f, 1f));

    #endregion


    private void OnEnable()
    {
        MainGameObject = this.transform.parent.gameObject;
        animator = GetComponent<Animator>();
        runtimeAnimatorController = animator.runtimeAnimatorController;

        isTurningRight = false;
        isTurningLeft = false;
        isSitting = false;
        isStanding = false;
    }


    private void Update()
    {
        if (didSetRotatingProperties && (isTurningRight || isTurningLeft))
        {
            MainGameObject.transform.rotation = Quaternion.RotateTowards(
                MainGameObject.transform.rotation,
                targetRotation,
                turnSpeed * Time.deltaTime
                );
        }

        if (didSetRotatingProperties && isSitting)
        {
            moveTimer += Time.deltaTime;
            float t = Mathf.Clamp01(moveTimer / animationLength);
            float curvedT = sittingCurve.Evaluate(t);
            MainGameObject.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, curvedT);

            if (t >= 1f)
            {
                isSitting = false;
            }
        }

        if (didSetRotatingProperties && isStanding)
        {
            moveTimer += Time.deltaTime;
            float t = Mathf.Clamp01(moveTimer / animationLength);
            float curvedT = standingCurve.Evaluate(t);
            MainGameObject.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, curvedT);

            if (t >= 1f)
            {
                isStanding = false;
            }
        }
    }



    public void TurningRightAnimationStarted()
    {
        startRotation = MainGameObject.transform.localRotation;
        targetRotation = startRotation * Quaternion.Euler(0f, 90f, 0f);
        turnSpeed = 90f / GetAnimationClipLength("TurningRight_Edited");
        didSetRotatingProperties = true;
    }
    public void TurningRightAnimationEnded()
    {
        MainGameObject.transform.localRotation = targetRotation;


        Vector3 euler = MainGameObject.transform.localEulerAngles;
        euler.y = Mathf.Repeat(euler.y, 360f);
        euler.y = Mathf.Round(euler.y / 90f) * 90f;
        MainGameObject.transform.localEulerAngles = euler;


        isTurningRight = false;
        didSetRotatingProperties = false;
    }

    public void TurningLeftAnimationStarted()
    {
        startRotation = MainGameObject.transform.localRotation;
        targetRotation = startRotation * Quaternion.Euler(0f, -90f, 0f);
        turnSpeed = 90f / GetAnimationClipLength("TurningLeft_Edited");
        didSetRotatingProperties = true;
    }
    public void TurningLeftAnimationEnded()
    {
        MainGameObject.transform.localRotation = targetRotation;


        Vector3 euler = MainGameObject.transform.localEulerAngles;
        euler.y = Mathf.Repeat(euler.y, 360f);
        euler.y = Mathf.Round(euler.y / 90f) * 90f;
        MainGameObject.transform.localEulerAngles = euler;


        isTurningLeft = false;
        didSetRotatingProperties = false;
    }

    public void StandToSitAnimationStarted()
    {
        moveTimer = 0;
        animationLength = GetAnimationClipLength("StandingToSitting_Edited");
        startPosition = GetComponentInParent<NPCController>().Points[3];
        targetPosition = GetComponentInParent<NPCController>().Points[4];
        didSetRotatingProperties = true;
    }
    public void StandToSitAnimationEnded()
    {
        MainGameObject.transform.localPosition = targetPosition;
        isSitting = false;
        didSetRotatingProperties = false;
    }

    public void SitToStandAnimationStarted()
    {
        moveTimer = 0;
        animationLength = GetAnimationClipLength("SittingToStanding_Edited");
        startPosition = GetComponentInParent<NPCController>().Points[4];
        targetPosition = GetComponentInParent<NPCController>().Points[3];
        didSetRotatingProperties = true;
    }
    public void SitToStandAnimationEnded()
    {
        MainGameObject.transform.localPosition = targetPosition;
        isStanding = false;
        didSetRotatingProperties = false;
    }



    public float GetAnimationClipLength(string clipName)
    {
        foreach (var clip in runtimeAnimatorController.animationClips)
        {
            if (clip.name == clipName)
            {
                return clip.length;
            }
        }

        Debug.LogWarning("Animation clip not found: " + clipName);
        return 0f;
    }



}
