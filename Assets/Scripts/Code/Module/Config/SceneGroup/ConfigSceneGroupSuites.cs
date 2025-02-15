﻿using System;
using System.Collections.Generic;
using System.Linq;
using LitJson.Extensions;
using Nino.Serialization;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TaoTie
{
    /// <summary>
    /// 小组配置
    /// </summary>
    [NinoSerialize]
    public partial class ConfigSceneGroupSuites
    {
#if UNITY_EDITOR
        [NinoMember(0)][SerializeField] [LabelText("策划备注")][PropertyOrder(int.MinValue+1)]
        public string Remarks;
        [JsonIgnore]
        public bool RandSuite => OdinDropdownHelper.sceneGroup.RandSuite;
#endif
        [NinoMember(1)][PropertyOrder(int.MinValue)]
        public int LocalId;
        [NinoMember(2)]
        [ValueDropdown("@"+nameof(OdinDropdownHelper)+"."+nameof(OdinDropdownHelper.GetSceneGroupActorIds)+"()",AppendNextDrawer = true)]
        public int[] Actors;
        [NinoMember(3)]
        [ValueDropdown("@"+nameof(OdinDropdownHelper)+"."+nameof(OdinDropdownHelper.GetSceneGroupZoneIds)+"()",AppendNextDrawer = true)]
        public int[] Zones;
        [NinoMember(4)]
        [ValueDropdown("@"+nameof(OdinDropdownHelper)+"."+nameof(OdinDropdownHelper.GetSceneGroupTriggerIds)+"()",AppendNextDrawer = true)]
        public int[] Triggers;
#if UNITY_EDITOR
        [ShowIf(nameof(RandSuite))]
#endif
        [NinoMember(5)]
        public int RandWeight;
        
        
    }
}