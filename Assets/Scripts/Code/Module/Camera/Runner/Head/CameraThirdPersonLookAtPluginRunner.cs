﻿using UnityEngine;

namespace TaoTie
{
    public sealed class CameraThirdPersonLookAtPluginRunner: CameraHeadPluginRunner<ConfigCameraThirdPersonLookAtPlugin>
    {
        private ConfigActorCommon _actorCommon;
        private float nearFocusOffset;
        protected override void InitInternal()
        {
            LoadCommonConfig();
            Calculating();
        }

        protected override void UpdateInternal()
        {
            Calculating();
        }

        protected override void DisposeInternal()
        {
            _actorCommon = null;
        }
        
        public override void OnSetTarget()
        {
            base.OnSetTarget();
            LoadCommonConfig();
            Calculating();
        }
        
        private void LoadCommonConfig()
        {
            _actorCommon = null;
            if (state.follow != null)
            {
                var ec = state.follow.GetComponent<EntityComponent>();
                if (ec != null)
                {
                    var entityId = ec.Id;
                    if (SceneManager.Instance.CurrentScene is BaseMapScene map)
                    {
                        var unit = map.GetManager<EntityManager>().Get<Actor>(entityId);
                        _actorCommon = unit.configActor.Common;
                    }
                }
            }
        }

        private void Calculating()
        {
            Vector3 dir;
            
            if (_actorCommon != null)
            {
                data.LookAt += data.TargetUp * _actorCommon.Height / 2;
            }
            dir = data.LookAt - data.Position;
            if (dir == Vector3.zero)
            {
                data.Orientation = Quaternion.LookRotation(data.TargetForward, data.TargetUp);
            }
            else
            {
                data.Orientation = Quaternion.LookRotation(dir);
                if (config.NearFocusEnable)
                {
                    var distance = dir.magnitude;
                    if (distance >= config.NearFocusMaxDistance)
                    {
                        return;
                    }

                    distance = Mathf.Clamp(distance, config.NearFocusMinDistance, config.NearFocusMaxDistance);
                    var progress = 1 - (distance - config.NearFocusMinDistance) /
                                  (config.NearFocusMaxDistance - config.NearFocusMinDistance);
                    var offset =  progress * _actorCommon.NearFocusOffsetHeight;
                    nearFocusOffset = Mathf.Lerp(nearFocusOffset, offset, 0.5f);
                    var offsetV3 = data.TargetUp * nearFocusOffset;
                    data.Position += offsetV3;
                    data.LookAt += offsetV3;

                }
            }
        }
    }
}