using UnityEngine;
using UnityEngine.Events;

namespace RPG.Dialogue
{
    public class DialogueTrigger : MonoBehaviour
    {
        #region --Fields-- (Inspector)
        [SerializeField] private string _actionString;
        #endregion



        #region --Events-- (UnityEvent)
        [Header("UnityEvent")]
        [SerializeField] private UnityEvent _onTriggerHappen;
        #endregion



        #region --Methods (Custom PUBLIC)
        public void Trigger(string callerActionString)
        {
            if (_actionString == callerActionString)
            {
                _onTriggerHappen?.Invoke();
            }
        }
        #endregion
    }
}