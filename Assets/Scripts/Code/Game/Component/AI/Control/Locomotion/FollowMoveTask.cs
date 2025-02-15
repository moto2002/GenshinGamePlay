﻿using UnityEngine;

namespace TaoTie
{
    public class FollowMoveTask: LocoBaseTask
    {
        private Unit anchor;
        private NumericComponent numc => anchor.GetComponent<NumericComponent>();
        private bool useMeleeSlot; 
        private float stopDistance; 
        private float targetAngle; 
        public override void UpdateLoco(AILocomotionHandler handler, AITransform currentTransform, ref LocoTaskState state)
        {
            Vector3 anchorPos = anchor.Position;

            float distance = Vector3.Distance(currentTransform.pos, anchorPos);
            if (distance > stopDistance)
            {
                var desiredDirection = (anchorPos - currentTransform.pos);
                desiredDirection.y = 0;
                desiredDirection = desiredDirection.normalized;
                // handler.aiKnowledge.desiredForward = desiredDirection;

                handler.UpdateMotionFlag(speedLevel);
                var moveDis = numc.GetAsFloat(NumericType.Speed) * GameTimerManager.Instance.GetDeltaTime() / 1000f;
                if (moveDis > distance)
                {
                    aiKnowledge.AiOwnerEntity.Position = anchorPos;
                }
                else
                {
                    aiKnowledge.AiOwnerEntity.Position += desiredDirection * moveDis;
                }

                // aiKnowledge.AiOwnerEntity.IsTurn = anchorPos.x < aiKnowledge.AiOwnerEntity.Position.x;
            }
            else
            {
                handler.UpdateMotionFlag(AIMoveSpeedLevel.Idle);
            }
        }

        public void Init(AIKnowledge knowledge, AILocomotionHandler.ParamFollowMove param)
        {
            base.Init(knowledge);
            anchor = param.anchor;
            useMeleeSlot = param.useMeleeSlot;
            stopDistance = param.stopDistance;
            targetAngle = param.targetAngle;
            speedLevel = param.speedLevel;
        }
        public override void OnCloseTask(AILocomotionHandler handler)
        {
            handler.UpdateMotionFlag(AIMoveSpeedLevel.Idle);
        }
        public override void Deallocate()
        {
            
        }
    }
}