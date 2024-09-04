using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyBehaviour : MonoBehaviour
{
    public int size;

}

[CustomEditor(typeof(EnemyBehaviour)), CanEditMultipleObjects]
public class EnemyBehaviourEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Select all enemies"))
        {
            var allEnemyBehaviour = GameObject.FindObjectsOfType
           <EnemyBehaviour>();
            var allEnemyGameObjects = allEnemyBehaviour
            .Select(enemy => enemy.gameObject)
            .ToArray();
            Selection.objects = allEnemyGameObjects;
        }

        if(GUILayout.Button("Clear Selection"))
        {
            Selection.objects = null;
        }
        EditorGUILayout.EndHorizontal();
        if (GUILayout.Button("Disable/Enable all enemy", GUILayout.Height(100)))
        {
            foreach (var enemy in GameObject.FindObjectsOfType<EnemyBehaviour>(true))
            {
                Undo.RecordObject(enemy.gameObject, "Disable/Enable enemy");
                enemy.gameObject.SetActive(!enemy.gameObject.activeSelf);
            }
        }
    }
}