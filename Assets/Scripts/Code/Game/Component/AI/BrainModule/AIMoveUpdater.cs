﻿namespace TaoTie
{
    /// <summary>
    /// 移动
    /// </summary>
    public class AIMoveUpdater : BrainModuleBase
    {
        private FsmComponent fsm => knowledge.AiOwnerEntity.GetComponent<FsmComponent>();
        
        protected override void UpdateMainThreadInternal()
        {
            base.UpdateMainThreadInternal();
            knowledge.MoveKnowledge.CanMove = fsm.DefaultFsm.currentState.CanMove;
            knowledge.MoveKnowledge.CanTurn = fsm.DefaultFsm.currentState.CanTurn;
        }
    }
}