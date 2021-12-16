using UnityEngine;

namespace RPG.Core
{
    public class FollowCamera : MonoBehaviour
    {
        #region --Fields-- (Inspector)
        [Header("Follow Camera Properties")]
        [SerializeField] private Vector3 _offSet; // 0, 12, -11
        [SerializeField] private float _smoothSpeed = 0.2f;
        #endregion



        #region --Fields-- (In Class)
        private Vector3 _smoothVelocityRef = Vector3.zero; // Don't have to use just for Reference

        private Transform _target;
        #endregion



        #region --Methods-- (Built In)
        private void Start()
        {
            _target = GameObject.FindWithTag("Player").transform;
        }

        private void LateUpdate()
        {
            Vector3 targetPostion = _target.position + _offSet;

            transform.position = Vector3.SmoothDamp(transform.position, targetPostion, ref _smoothVelocityRef, _smoothSpeed);
        }
        #endregion
    }
}