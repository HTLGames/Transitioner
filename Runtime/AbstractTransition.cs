using UnityEngine;

namespace HTL.Transitioner
{
    public abstract class AbstractTransition : MonoBehaviour, ITransition
    {
        [SerializeField] private bool resetAnimationOnPlay = true;

        bool ITransition.resetAnimationOnPlay => resetAnimationOnPlay;

        #region Docs
        /// <summary>
        /// Inmediately shows or hides the object.
        /// </summary>
        /// <param name="visible">True to show, false to hide.</param>
        #endregion
        public void SetVisibility(bool visible)
        {
            if (visible)
            {
                SetVisible();
            }
            else
            {
                SetHidden();
            }
        }

        #region Docs
        /// <summary>
        /// Triggers a transition which may show or hide the object.
        /// </summary>
        /// <param name="show">True to show the object, false to hide.</param>
        #endregion
        void ITransition.Play(bool show)
        {
            PlayAsync(show).GetAwaiter();
        }

        #region Docs
        /// <summary>
        /// Triggers a transition which may show or hide the object.
        /// </summary>
        /// <param name="show">True to show the object, false to hide.</param>
        /// <param name="onComplete">Action to perform on transition end.</param>
        #endregion
        public void Play(bool show, System.Action onComplete)
        {
            PlayAsyncOnComplete(show, onComplete).GetAwaiter();
        }

        #region Docs
        /// <summary>
        /// Triggers a transition which may show or hide the object.
        /// </summary>
        /// <param name="show">True to show the object, false to hide.</param>
        #endregion
        public async Awaitable PlayAsync(bool show)
        {
            if (resetAnimationOnPlay) SetVisibility(!show);

            if (show)
            {
                await Show();
            }
            else
            {
                await Hide();
            }
        }

        private async Awaitable PlayAsyncOnComplete(bool fadeIn, System.Action onComplete)
        {
            await PlayAsync(fadeIn);
            onComplete?.Invoke();
        }

        /// <summary>
        /// Should play a transition to hide the object.
        /// </summary>
        /// <returns></returns>
        protected abstract Awaitable Hide();

        /// <summary>
        /// Should inmediately hide the object.
        /// </summary>
        protected abstract void SetHidden();

        /// <summary>
        /// Should inmediately show the object.
        /// </summary>
        protected abstract void SetVisible();

        /// <summary>
        /// Should play a transition to show the object.
        /// </summary>
        /// <returns></returns>
        protected abstract Awaitable Show();

        async Awaitable ITransition.Show() => await Show();
        async Awaitable ITransition.Hide() => await Hide();
        void ITransition.SetVisible() => SetVisible();
        void ITransition.SetHidden() => SetHidden();
    }
}
