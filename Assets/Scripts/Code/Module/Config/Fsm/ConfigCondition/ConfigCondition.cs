﻿using System;
using Nino.Serialization;

namespace TaoTie
{
    [NinoSerialize]
    public abstract partial class ConfigCondition
    {


        public abstract ConfigCondition Copy();
        public abstract bool IsMatch(Fsm fsm);
        public virtual void OnTransitionApply(Fsm fsm) { }
    }
}