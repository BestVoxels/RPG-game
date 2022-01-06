using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        #region --Fields-- (In Class)
        private bool _isTriggered = false;
        #endregion



        #region --Methods-- (Build In)
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !_isTriggered)
            {
                GetComponent<PlayableDirector>().Play();
                _isTriggered = true;
            }
        }
        #endregion
    }
}