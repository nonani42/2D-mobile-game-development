﻿using UnityEditor;
using UnityEditor.UI;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Tween.Editor
{
    [CustomEditor(typeof(CustomButtonByInheritance))]
    internal class CustomButtonEditor : ButtonEditor
    {
        private SerializedProperty m_InteractableProperty;

        protected override void OnEnable()
        {
            m_InteractableProperty = serializedObject.FindProperty("m_Interactable");
        }

        // Новый способ редактирования представления инспектора
        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();

            var animationType = new PropertyField(serializedObject.FindProperty(CustomButtonByInheritance.AnimationTypeName));
            var curveEase = new PropertyField(serializedObject.FindProperty(CustomButtonByInheritance.CurveEaseName));
            var duration = new PropertyField(serializedObject.FindProperty(CustomButtonByInheritance.DurationName));
            var jumpStrength = new PropertyField(serializedObject.FindProperty(CustomButtonByInheritance.JumpStrength));
            var jumpCount = new PropertyField(serializedObject.FindProperty(CustomButtonByInheritance.JumpCount));
            var isIndependentUpdate = new PropertyField(serializedObject.FindProperty(CustomButtonByInheritance.IsIndependentUpdate));


            var tweenLabel = new Label("Settings Tween");
            var intractableLabel = new Label("Interactable");

            root.Add(tweenLabel);
            root.Add(animationType);
            root.Add(curveEase);
            root.Add(duration);
            root.Add(jumpStrength);
            root.Add(jumpCount);
            root.Add(isIndependentUpdate);

            root.Add(intractableLabel);
            root.Add(new IMGUIContainer(OnInspectorGUI));

            return root;
        }

        // Старый способ представления инскпектора
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(m_InteractableProperty);

            EditorGUI.BeginChangeCheck();
            EditorGUI.EndChangeCheck();

            serializedObject.ApplyModifiedProperties();
        }
    }
}
