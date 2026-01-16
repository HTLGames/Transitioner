using System.Collections.Generic;
using UnityEngine;

namespace HTL.Transitioner.Menus
{
    public class TransitionerMenuGroup : MonoBehaviour
    {
        private TransitionerMenu currentMenu;
        [SerializeField] private List<TransitionerMenu> menus;

        /// <summary>
        /// Unloads current menu and loads the next one.<br/>
        /// The provided menu <b>can be set to null</b> to hide menu group.
        /// </summary>
        /// <param name="menu">Menu to load.<br/><b>If null:</b> the menu group will be hidden.</param>
        public void LoadMenu(TransitionerMenu menu)
        {
            LoadMenuAsync(menu).GetAwaiter();
        }

        private async Awaitable LoadMenuAsync(TransitionerMenu menu)
        {
            await currentMenu?.UnloadMenuAsync();
            currentMenu = menu;
            await currentMenu?.LoadMenuAsync();
        }

        public void LoadMenu(TransitionerMenu menu, System.Action onComplete)
        {
            LoadMenuAsyncOnComplete(menu, onComplete).GetAwaiter();
        }

        private async Awaitable LoadMenuAsyncOnComplete(TransitionerMenu menu, System.Action onComplete)
        {
            await LoadMenuAsync(menu);
            onComplete();
        }

        internal void AddMenu(TransitionerMenu menu) => menus.Add(menu);
        internal bool HasMenu(TransitionerMenu menu) => menus.Contains(menu);
    }
}
