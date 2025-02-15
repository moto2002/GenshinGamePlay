﻿using System;
using Nino.Serialization;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TaoTie
{
    /// <summary>
    /// SceneGroup调整suite进度,只对非randSuite有效
    /// </summary>
    [LabelText("跳转到其他Suite")]
    [NinoSerialize]
    public partial class ConfigSceneGroupGoToSuiteAction:ConfigSceneGroupAction
    {
        [LabelText("要跳转的组Id")]
        [ValueDropdown("@OdinDropdownHelper.GetSceneGroupSuiteIds()")]
        [NinoMember(10)]
        public int SuiteId;
        protected override void Execute(IEventBase evt, SceneGroup aimSceneGroup, SceneGroup fromSceneGroup)
        {
            if (!aimSceneGroup.Config.RandSuite)
            {
                aimSceneGroup.ChangeSuite(SuiteId);
            }
            else
            {
                Log.Error("SceneGroup调整suite进度,只对非randGroup有效");
            }
        }
    }
}