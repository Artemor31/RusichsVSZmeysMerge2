﻿using System;
using NaughtyAttributes.Core.DrawerAttributes;
using NaughtyAttributes.Editor.Utility;
using UnityEditor;
using UnityEngine;

namespace NaughtyAttributes.Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(EnumFlagsAttribute))]
    public class EnumFlagsPropertyDrawer : PropertyDrawerBase
    {
        protected override float GetPropertyHeight_Internal(SerializedProperty property, GUIContent label)
        {
            Enum targetEnum = PropertyUtility.GetTargetObjectOfProperty(property) as Enum;

            return (targetEnum != null)
                ? GetPropertyHeight(property)
                : GetPropertyHeight(property) + GetHelpBoxHeight();
        }

        protected override void OnGUI_Internal(Rect rect, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(rect, label, property);

            Enum targetEnum = PropertyUtility.GetTargetObjectOfProperty(property) as Enum;
            if (targetEnum != null)
            {
                Enum enumNew = EditorGUI.EnumFlagsField(rect, label.text, targetEnum);
                property.intValue = (int)Convert.ChangeType(enumNew, targetEnum.GetType());
            }
            else
            {
                string message = attribute.GetType().Name + " can be used only on enums";
                DrawDefaultPropertyAndHelpBox(rect, property, message, MessageType.Warning);
            }

            EditorGUI.EndProperty();
        }
    }
}