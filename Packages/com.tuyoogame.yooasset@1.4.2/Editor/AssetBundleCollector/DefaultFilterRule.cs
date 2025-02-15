﻿using UnityEngine;
using UnityEditor;
using System.IO;

namespace YooAsset.Editor
{
	[DisplayName("收集所有资源")]
	public class CollectAll : IFilterRule
	{
		public bool IsCollectAsset(FilterRuleData data)
		{
			return true;
		}
	}

	[DisplayName("收集场景")]
	public class CollectScene : IFilterRule
	{
		public bool IsCollectAsset(FilterRuleData data)
		{
			return Path.GetExtension(data.AssetPath) == ".unity";
		}
	}

	[DisplayName("收集预制体")]
	public class CollectPrefab : IFilterRule
	{
		public bool IsCollectAsset(FilterRuleData data)
		{
			return Path.GetExtension(data.AssetPath) == ".prefab";
		}
	}

	[DisplayName("收集精灵类型的纹理")]
	public class CollectSprite : IFilterRule
	{
		public bool IsCollectAsset(FilterRuleData data)
		{
			var mainAssetType = AssetDatabase.GetMainAssetTypeAtPath(data.AssetPath);
			if (mainAssetType == typeof(Texture2D))
			{
				var texImporter = AssetImporter.GetAtPath(data.AssetPath) as TextureImporter;
				if (texImporter != null && texImporter.textureType == TextureImporterType.Sprite)
					return true;
				else
					return false;
			}
			else
			{
				return false;
			}
		}
	}

	[DisplayName("收集着色器变种集合")]
	public class CollectShaderVariants : IFilterRule
	{
		public bool IsCollectAsset(FilterRuleData data)
		{
			return Path.GetExtension(data.AssetPath) == ".shadervariants";
		}
	}
	
	[DisplayName("收集Unit")]
	public class CollectUnit : IFilterRule
	{
		public bool IsCollectAsset(FilterRuleData data)
		{
			if (data.AssetPath.Contains("/Edit/")) return false;
			var ext = Path.GetExtension(data.AssetPath);
			return ext == ".prefab" || ext == ".bytes"|| ext == ".json" || ext == ".controller";
		}
	}
}