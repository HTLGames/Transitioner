using System;
using UnityEngine;

namespace HTL.Transitioner.Loading
{
    #region Docs
    /// <summary>
    /// Disposable for showing and hiding a loading screen in an asyncronous workflow.<br/>
    /// You must await <c>ShowScreenAsync()</c> to wait for loading screen to load.
    /// </summary>
    #endregion
    public class LoadingDisposable : IDisposable
    {
        ITransition loadingScreen;

        public LoadingDisposable(ITransition loadingScreen)
        {
            this.loadingScreen = loadingScreen;
        }

        /// <summary>
        /// Plays transition to show loading screen.
        /// </summary>
        public async Awaitable ShowScreenAsync()
        {
            await loadingScreen.PlayAsync(true);
        }

        public void Dispose()
        {
            loadingScreen.Play(false);
        }
    }
}
