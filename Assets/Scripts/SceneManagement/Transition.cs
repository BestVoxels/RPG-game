using UnityEngine;
using System.Collections;

namespace RPG.SceneManagement
{
    public class Transition : MonoBehaviour
    {
        public enum Types
        {
            Fade,
            CircleWipe
        }



        #region --Fields-- (Inspector)
        [SerializeField] private Animator[] _animators;
        #endregion



        #region --Fields-- (In Class)
        public static Transition Instance { get; private set; }
        #endregion



        #region --Methods-- (Built In)
        private void Awake() => Instance = this;
        #endregion



        #region --Methods-- (Custom PUBLIC)
        public IEnumerator StartTransition(Types types, float transitioningSpeed)
        {
            int index = 0;
            switch (types)
            {
                case Types.Fade:
                    index = 0;
                    break;

                case Types.CircleWipe:
                    index = 1;
                    break;
            }

            _animators[index].speed = transitioningSpeed;
            _animators[index].Play("Transition Start", -1, 0f);

            // While Animation Clip is still the OLD one, wait for it to get update
            while (!_animators[index].GetCurrentAnimatorStateInfo(0).IsName("Transition Start"))
            {
                yield return null;
            }

            // Wait for New Animation Clip to play until it ends
            while (_animators[index].GetCurrentAnimatorStateInfo(0).IsName("Transition Start") &&
                _animators[index].GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            {
                yield return null;
            }
            print("STARTED");
            yield break;
        }

        public IEnumerator EndTransition(Types types, float transitioningSpeed)
        {
            int index = 0;
            switch (types)
            {
                case Types.Fade:
                    index = 0;
                    break;

                case Types.CircleWipe:
                    index = 1;
                    break;
            }

            _animators[index].speed = transitioningSpeed;
            _animators[index].Play("Transition End", -1, 0f);

            // While Animation Clip is still the OLD one, wait for it to get update
            while (!_animators[index].GetCurrentAnimatorStateInfo(0).IsName("Transition End"))
            {
                yield return null;
            }

            // Wait for New Animation Clip to play until it ends
            while (_animators[index].GetCurrentAnimatorStateInfo(0).IsName("Transition End") &&
                _animators[index].GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            {
                yield return null;
            }
            print("ENDED");
            yield break;
        }
        #endregion
    }
}