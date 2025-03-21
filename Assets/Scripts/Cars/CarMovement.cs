using System;
using UnityEngine;
using Utils;

namespace Cars
{
    public class CarMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 8f;
        [SerializeField] private float turnRadius = 5f;
        [SerializeField] private float angularSpeed = 90f;

        [SerializeField] private Transform facilityTargetPosition;
        [SerializeField] private Transform roadTargetPosition;

        public CarState currentState = CarState.OnRoad;
        public bool isNeedRepair = true;
        public bool isNeedFuel = true;
        private Timer _waitTimer;

        
        private Vector3 pivotPoint;
        private int turnDirection;
        private float currentAngle;
        private float targetAngle;

        private void Update()
        {
            if (_waitTimer is { isActive: true })
            {
                _waitTimer.Update();
                return;
            }

            switch (currentState)
            {
                case CarState.OnRoad:
                    MoveStraight();
                    break;
                case CarState.Turning:
                    CurveTurn(() => currentState = CarState.InsideFacility);
                    break;
                case CarState.InsideFacility:
                    MoveTowards(facilityTargetPosition.position,
                        () => currentState = CarState.Idle);
                    break;
                case CarState.Idle:
                    //WaitForRepair(() => currentState = CarState.ReversingOut);
                    break;
                case CarState.ReversingOut:
                    MoveTowards(roadTargetPosition.position,
                        () =>
                        {
                            SetupTurn(roadTargetPosition, true); 
                            currentState = CarState.TurningBackToRoad;
                        });
                    break;
                case CarState.TurningBackToRoad:
                    CurveTurn(() => currentState = CarState.OnRoad);
                    break;
            }
        }

        private void MoveStraight()
        {
            transform.Translate(Vector3.forward * (speed * Time.deltaTime));
        }

        private void MoveTowards(Vector3 target, Action onArrive)
        {
            Vector3 dir = (target - transform.position).normalized;
            transform.Translate(dir * (speed * Time.deltaTime), Space.World);

            if (Vector3.Distance(transform.position, target) < 0.1f)
            {
                onArrive?.Invoke();
            }
        }
        
        private void CurveTurn(Action onComplete)
        {
            float step = angularSpeed * Time.deltaTime * turnDirection;
            transform.RotateAround(pivotPoint, Vector3.up, step);
            currentAngle += Mathf.Abs(step);

            if (currentAngle >= targetAngle)
            {
                currentAngle = 0f;
                onComplete?.Invoke();
            }
        }

        private void SetupTurn(Transform targetPosition, bool isReturning = false)
        {
            Vector3 toTarget = targetPosition.position - transform.position;

            Vector3 referenceForward = isReturning ? -transform.forward : transform.forward;
            Vector3 referenceRight = isReturning ? -transform.right : transform.right;

            float side = Vector3.Dot(referenceRight, toTarget);

            turnDirection = side > 0 ? 1 : -1;

            pivotPoint = transform.position + (referenceRight * (turnRadius * turnDirection));
            targetAngle = 90f;
        }

        public void TurnToFacility(Transform targetPosition, Transform roadReturnPosition)
        {
            facilityTargetPosition = targetPosition;
            roadTargetPosition = roadReturnPosition;
            SetupTurn(facilityTargetPosition);
            currentState = CarState.Turning;
        }
        
        // private void WaitForRepair(Action onComplete)
        // {
        //     _waitTimer = new Timer(5f, onComplete);
        //     _waitTimer.isActive = true;
        // }
    }
}