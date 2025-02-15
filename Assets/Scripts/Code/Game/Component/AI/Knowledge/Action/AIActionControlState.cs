﻿using System;

namespace TaoTie
{
    public class AIActionControlState: IDisposable
    {
        private bool isPrepared;
        public AISkillInfo Skill;
        public SkillStatus Status;
        private float QuerySkillDiscardTick;
        public string CurrentStateID;

        public static AIActionControlState Create()
        {
            return ObjectPool.Instance.Fetch<AIActionControlState>();
        }
        
        public void Dispose()
        {
            isPrepared = false;
            Skill = null;
            Status = default;
            QuerySkillDiscardTick = 0;
            CurrentStateID = null;
        }

        public void Reset()
        {
            Skill = null;
            CurrentStateID = null;
            Status = SkillStatus.Inactive;
        }
    }
}