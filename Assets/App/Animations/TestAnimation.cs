using Unity.VisualScripting;
using UnityEngine;

public class TestAnimation : MonoBehaviour
{
    Animator animator;

    #region Animation Names

    int isStandingIdle_Hash;
    int isWalking_Hash;
    int isTurningRight_Hash;
    int isTurningLeft_Hash;
    int isSittingToStanding_Hash;
    int isStandingToSitting_Hash;
    int isSittingIdle_Hash;

    #endregion


    private void OnEnable()
    {
        animator = GetComponentInChildren<Animator>();

        TurnAnimationNamesIntoHash();

        //SetAnimation(isStandingIdle_Hash);
        StartNPCLoop();
    }

    private async void StartNPCLoop()
    {
        for (int i = 0; i < 10; i++)
        {
            SetAnimation(isStandingIdle_Hash);
            await Awaitable.WaitForSecondsAsync(2f);

            SetAnimation(isWalking_Hash);
            await Awaitable.WaitForSecondsAsync(2f);

            SetAnimation(isTurningRight_Hash);
            await Awaitable.WaitForSecondsAsync(2f);

            SetAnimation(isWalking_Hash);
            await Awaitable.WaitForSecondsAsync(2f);

            SetAnimation(isTurningLeft_Hash);
            await Awaitable.WaitForSecondsAsync(2f);

            SetAnimation(isWalking_Hash);
            await Awaitable.WaitForSecondsAsync(2f);

            SetAnimation(isTurningRight_Hash);
            await Awaitable.WaitForSecondsAsync(2f);

            SetAnimation(isStandingToSitting_Hash);
            await Awaitable.WaitForSecondsAsync(2f);

            SetAnimation(isSittingIdle_Hash);
            await Awaitable.WaitForSecondsAsync(2f);

            SetAnimation(isSittingToStanding_Hash);
            await Awaitable.WaitForSecondsAsync(2f);
        }

    }


    private void TurnAnimationNamesIntoHash()
    {
        isStandingIdle_Hash = Animator.StringToHash("isStandingIdle");
        isWalking_Hash = Animator.StringToHash("isWalking");
        isTurningRight_Hash = Animator.StringToHash("isTurningRight");
        isTurningLeft_Hash = Animator.StringToHash("isTurningLeft");
        isSittingToStanding_Hash = Animator.StringToHash("isSittingToStanding");
        isStandingToSitting_Hash = Animator.StringToHash("isStandingToSitting");
        isSittingIdle_Hash = Animator.StringToHash("isSittingIdle");
    }

    private void SetAnimation(int animation_Hash)
    {
        animator.SetBool(isStandingIdle_Hash, false);
        animator.SetBool(isWalking_Hash, false);
        animator.SetBool(isTurningRight_Hash, false);
        animator.SetBool(isTurningLeft_Hash, false);
        animator.SetBool(isSittingToStanding_Hash, false);
        animator.SetBool(isStandingToSitting_Hash, false);
        animator.SetBool(isSittingIdle_Hash, false);

        animator.SetBool(animation_Hash, true);
    }
}
