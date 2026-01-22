using UnityEngine;

namespace HTL.Transitioner.Menus
{
    [RequireComponent(typeof(AbstractTransition))]
    public class TransitionerMenu : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private TransitionerMenuGroup menuGroup;

        AbstractTransition transition;

        void Awake()
        {
            transition = GetComponent<AbstractTransition>();
        }

        /// <summary>
        /// Hides currently loaded menu from the menu group and loads this one.
        /// </summary>
        public void LoadMenu()
        {
            menuGroup.LoadMenu(this);
        }

        internal async Awaitable LoadMenuAsync()
        {
            await transition.PlayAsync(true);
        }

        internal async Awaitable UnloadMenuAsync()
        {
            await transition.PlayAsync(false);
        }

        internal void SetVisible() => transition.SetVisibility(true);

#if UNITY_EDITOR
        void OnValidate()
        {
            /*  TODO: menuGroup should be ReadOnly from the inspector and should be set by the MenuGroup itself 
                this code would have to set menu group to null if its not in the menu group. 
                There should be an Editor tool for this. */
            if (menuGroup != null && !menuGroup.HasMenu(this))
            {
                menuGroup.AddMenu(this);
            }
        }
#endif
    }
}
