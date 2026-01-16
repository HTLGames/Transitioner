using UnityEngine;

namespace HTL.Transitioner
{
    /// <summary>
    /// The ITransition interface provides methods to handle transitions.<br/>
    /// Transitions are handled asyncronously.
    /// </summary>
    public interface ITransition
    {
        /// <summary>
        /// Will reset the visibility of the object before playing the animation it if true.
        /// </summary>
        protected bool resetAnimationOnPlay { get; }

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

        #region Docs
        /// <summary>
        /// Triggers a transition which may show or hide the object.
        /// </summary>
        /// <param name="show">True to show the object, false to hide.</param>
        /// <param name="onComplete">Action to perform on transition end.</param>
        #endregion
        public void Play(bool fadeIn, System.Action onComplete)
        {
            PlayAsyncOnComplete(fadeIn, onComplete).GetAwaiter();
        }

        private async Awaitable PlayAsyncOnComplete(bool fadeIn, System.Action onComplete)
        {
            await PlayAsync(fadeIn);
            onComplete?.Invoke();
        }

        /// <summary>
        /// Should play a transition to show the object.
        /// </summary>
        /// <returns></returns>
        protected Awaitable Show();

        /// <summary>
        /// Should play a transition to hide the object.
        /// </summary>
        /// <returns></returns>
        protected Awaitable Hide();

        /// <summary>
        /// Should inmediately show the object.
        /// </summary>
        protected void SetVisible();

        /// <summary>
        /// Should inmediately hide the object.
        /// </summary>
        protected void SetHidden();
    }
}
