using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        #region --Fields-- (In Class)
        private IAction _currentAction;
        #endregion



        #region --Methods-- (Custom PUBLIC)
        public void StartAction(IAction action) // Using Polymorphism to Pass in with Mover or Fighter classes
        {
            if (_currentAction == action) return;

            if (_currentAction != null)
            {
                _currentAction.Cancel();
            }

            _currentAction = action;
        }

        public void StopCurrentAction()
        {
            StartAction(null);
        }
        #endregion
    }
}