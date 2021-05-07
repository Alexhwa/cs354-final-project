using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CParticleSystem))]
public class CParticleSystemEditor : Editor
{
    SerializedObject so;
    SerializedProperty propEmissionData;
    SerializedProperty propRenderData;
    SerializedProperty propModules;

    private void OnEnable() {
        so = serializedObject;

        propEmissionData = so.FindProperty("emissionData");
        propRenderData = so.FindProperty("renderData");
        propModules = so.FindProperty("modules");

        Debug.Log(propModules.isArray);
    }

    public override void OnInspectorGUI()
    {
        so.Update();

        // base.OnInspectorGUI();

        EditorGUILayout.PropertyField(propEmissionData);
        EditorGUILayout.PropertyField(propRenderData);
        EditorGUILayout.PropertyField(propModules);

        if (EditorGUILayout.DropdownButton(new GUIContent("dropdown"), FocusType.Keyboard)) {
            var rect = GUILayoutUtility.GetLastRect();
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("a thing"), false, () => {  });
            menu.AddItem(new GUIContent("b thing"), false, () => {});
            menu.AddItem(new GUIContent("c thing"), false, () => {});
            menu.DropDown(rect);
        }
    
        so.ApplyModifiedProperties();
    }
}
