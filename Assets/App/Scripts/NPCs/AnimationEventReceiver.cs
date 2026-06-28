using UnityEngine;

public class AnimationEventReceiver : MonoBehaviour
{

    /*
    RuntimeAnimatorController runtimeAnimatorController;

    [HideInInspector] public bool isTurningRight;
    [HideInInspector] public bool isTurningLeft;
    [HideInInspector] public bool isSitting;
    [HideInInspector] public bool isStanding;


    //Rotating Properties
    private Quaternion startRotation;
    private Quaternion targetRotation;
    private float turnSpeed;


    //Sitting and Standing Properties
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

    private void Start()
    {
        runtimeAnimatorController = GetComponent<Animator>().runtimeAnimatorController;

        isTurningRight = false;
        isTurningLeft = false;
        isSitting = false;
        isStanding = false;
    }

    private void Update()
    {
        if (isTurningRight || isTurningLeft)
        {
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                turnSpeed * Time.deltaTime
            );
        }

        if (isSitting)
        {
            moveTimer += Time.deltaTime;
            float t = Mathf.Clamp01(moveTimer / animationLength);
            float curvedT = sittingCurve.Evaluate(t);
            transform.localPosition = Vector3.Lerp(startPosition, targetPosition, curvedT);

            if (t >= 1f)
            {
                isSitting = false;
            }
        }

        if (isStanding)
        {
            moveTimer += Time.deltaTime;
            float t = Mathf.Clamp01(moveTimer / animationLength);
            float curvedT = standingCurve.Evaluate(t);
            transform.localPosition = Vector3.Lerp(startPosition, targetPosition, curvedT);


            if (t >= 1f)
            {
                isStanding = false;
            }
        }
    }

    public void RightTurn90AnimationStarted()
    {
        startRotation = transform.localRotation;
        targetRotation = startRotation * Quaternion.Euler(0f, 90f, 0f);
        turnSpeed = 90f / GetAnimationClipLength("Right Turn 90 Writable");
    }
    public void RightTurn90AnimationEnded()
    {
        transform.localRotation = targetRotation;

        Vector3 euler = transform.localEulerAngles;
        euler.y = Mathf.Repeat(euler.y, 360f);
        euler.y = Mathf.Round(euler.y / 90f) * 90f;
        transform.localEulerAngles = euler;

        isTurningRight = false;
    }

    public void LeftTurn90AnimationStarted()
    {
        startRotation = transform.localRotation;
        targetRotation = startRotation * Quaternion.Euler(0f, -90f, 0f);
        turnSpeed = 90f / GetAnimationClipLength("Left Turn 90 Writable");
    }
    public void LeftTurn90AnimationEnded()
    {
        transform.localRotation = targetRotation;

        Vector3 euler = transform.localEulerAngles;
        euler.y = Mathf.Repeat(euler.y, 360f);
        euler.y = Mathf.Round(euler.y / 90f) * 90f;
        transform.localEulerAngles = euler;

        isTurningLeft = false;
    }

    public void StandToSitAnimationStarted()
    {
        moveTimer = 0;
        animationLength = GetAnimationClipLength("Stand To Sit Writable");
        startPosition = GetComponent<NPCController>().myPoints[3];
        targetPosition = GetComponent<NPCController>().myPoints[4];
        isSitting = true;
    }
    public void StandToSitAnimationEnded()
    {
        transform.localPosition = targetPosition;
        isSitting = false;
    }

    public void SitToStandAnimationStarted()
    {
        moveTimer = 0;
        animationLength = GetAnimationClipLength("Sit To Stand Writable");
        startPosition = GetComponent<NPCController>().myPoints[4];
        targetPosition = GetComponent<NPCController>().myPoints[3];
        isStanding = true;
    }
    public void SitToStandAnimationEnded()
    {
        transform.localPosition = targetPosition;
        isStanding = false;
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
    */
}
