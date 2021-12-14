using UnityEngine;
using RPG.Core;
using RPG.Movement;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        #region --Fields-- (Inspector)
        [SerializeField] private float _weaponRange = 2f;
        #endregion



        #region --Fields-- (In Class)
        private ActionScheduler _actionScheduler;

        private Transform _target;
        private Mover _mover;
        #endregion



        #region --Methods-- (Built In)
        private void Start()
        {
            _actionScheduler = GetComponent<ActionScheduler>();

            _mover = GetComponent<Mover>();
        }

        private void Update()
        {
            if (_target == null) return;

            if (!IsInStopRange())
            {
                _mover.MoveTo(_target.position);
            }
            else
            {
                _mover.CancelMoveTo();
            }
        }
        #endregion



        #region --Methods-- (Custom PUBLIC)
        public void Attack(CombatTarget target)
        {
            _actionScheduler.StartAction(this);

            _target = target.transform;
        }
        #endregion



        #region --Methods-- (Custom PRIVATE)
        private void CancelAttack()
        {
            _target = null;
        }

        private bool IsInStopRange() => Vector3.Distance(transform.position, _target.position) < _weaponRange;
        #endregion



        #region --Methods-- (Interface)
        void IAction.Cancel()
        {
            CancelAttack();
        }
        #endregion
    }
}