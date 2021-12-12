using UnityEngine;
using RPG.Movement;
using RPG.Combat;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        #region --Fields-- (Inspector)
        #endregion



        #region --Fields-- (In Class)
        private Camera _camera;
        private Mover _mover;
        private Fighter _fighter;
        #endregion



        #region --Methods-- (Built In)
        private void Start()
        {
            _camera = Camera.main;
            _mover = GetComponent<Mover>();
            _fighter = GetComponent<Fighter>();
        }

        private void Update()
        {
            InteractWithMovement();
            InteractWithCombat();
        }
        #endregion



        #region --Methods-- (Custom PRIVATE)
        private void InteractWithMovement()
        {
            if (Input.GetMouseButton(0))
            {
                MoveToCursor();
            }
        }

        private void MoveToCursor()
        {
            // Draw the ray GET FIRST HIT
            if (Physics.Raycast(GetMouseRay(), out RaycastHit hitInfo))
            {
                _mover.MoveTo(hitInfo.point);
            }
        }

        private void InteractWithCombat()
        {
            if (Input.GetMouseButtonDown(0))
            {
                AttackTarget();
            }
        }

        private void AttackTarget()
        {
            // Draw the ray GET ALL, WON'T GET BLOCK
            RaycastHit[] hitsInfo = Physics.RaycastAll(GetMouseRay());

            foreach (RaycastHit each in hitsInfo)
            {
                CombatTarget combatTarget = each.transform.GetComponent<CombatTarget>();
                if (combatTarget == null) continue;

                _fighter.Attack();
            }
        }

        private Ray GetMouseRay() => _camera.ScreenPointToRay(Input.mousePosition); // get ray direction from camera to a screen point
        #endregion
    }
}