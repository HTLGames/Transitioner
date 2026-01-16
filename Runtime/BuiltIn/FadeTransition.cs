using DG.Tweening;
using UnityEngine;

namespace HTL.Transitioner.BuiltIn
{
    /// <summary>
    /// Sample fade transition that depends on DOTween for UI elements.<br/>
    /// It will modify a CanvasGroup's alpha channel to show or hide the game object and its children.<br/>
    /// <c>interactable</c> and <c>blocksRaycast</c> are also modified to assure there won't be any interaction
    ///  with the UI elements while hidden.
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public class FadeTransition : MonoBehaviour, ITransition
    {
        // Fields
        [Header("Timing")]
        [SerializeField] float duration = 0.5f;
        [SerializeField] float showDelay = 0f;
        [SerializeField] float hideDelay = 0f;

        [Header("Animation")]
        [SerializeField] Ease showEase = Ease.InCubic;
        [SerializeField] Ease hideEase = Ease.OutCubic;

        [Header("Other")]
        [SerializeField] bool resetAnimationOnPlay = true;

        // References
        CanvasGroup canvasGroup;

        bool ITransition.resetAnimationOnPlay => resetAnimationOnPlay;

        [SerializeField]
        async Awaitable ITransition.Hide()
        {
            // Avoid interactions while playing
            canvasGroup.interactable = false;

            // Play animation
            await canvasGroup.DOFade(0, duration)
                .SetEase(hideEase)
                .SetDelay(hideDelay)
                .AsyncWaitForCompletion();

            // Don't block raycasts when hidden
            canvasGroup.blocksRaycasts = true;
        }

        async Awaitable ITransition.Show()
        {
            // Block raycasts while playing
            canvasGroup.blocksRaycasts = true;

            // Play animation
            await canvasGroup.DOFade(1, duration)
                .SetEase(showEase)
                .SetDelay(showDelay)
                .AsyncWaitForCompletion();
            
            // Reactivate interactivity
            canvasGroup.interactable = true;
        }

        void ITransition.SetHidden()
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        void ITransition.SetVisible()
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }
}
