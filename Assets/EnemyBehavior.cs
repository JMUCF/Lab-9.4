using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] protected float shapeSize;

    private void Awake()
    {
        shapeSize = 1f;
    }

    private void OnValidate()
    {
        transform.localScale = new Vector3(shapeSize, shapeSize, shapeSize);
    }
}

[CustomEditor(typeof(EnemyBehaviour)), CanEditMultipleObjects]
public class EnemyBehaviourEditor : Editor
{
    private bool cubesEnabled = true;
    private bool spheresEnabled = true;
    private SerializedProperty shapeSizeProperty;

    private void OnEnable()
    {
        shapeSizeProperty = serializedObject.FindProperty("shapeSize");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (shapeSizeProperty.floatValue < 0)
            EditorGUILayout.HelpBox("Size cannot be less than 0!", MessageType.Warning);

        else if(shapeSizeProperty.floatValue > 2)
            EditorGUILayout.HelpBox("Size cannot be greater than 2!", MessageType.Warning);

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Select all Cubes"))
        {
            var allEnemyBehaviour = GameObject.FindGameObjectsWithTag("Cube");
            var allEnemyGameObjects = allEnemyBehaviour
            .Select(enemy => enemy.gameObject)
            .ToArray();
            Selection.objects = allEnemyGameObjects;
        }
        if (GUILayout.Button("Select all Spheres"))
        {
            var allEnemyBehaviour = GameObject.FindGameObjectsWithTag("Sphere");
            var allEnemyGameObjects = allEnemyBehaviour
            .Select(enemy => enemy.gameObject)
            .ToArray();
            Selection.objects = allEnemyGameObjects;
        }

        if (GUILayout.Button("Clear Selection"))
        {
            Selection.objects = null;
        }
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.BeginHorizontal();

        if(cubesEnabled)
            GUI.backgroundColor = UnityEngine.Color.green;
        else if(!cubesEnabled)
            GUI.backgroundColor = UnityEngine.Color.red;
            
        if (GUILayout.Button("Disable/Enable Cubes", GUILayout.Height(50)))
        {
            foreach (var enemy in GameObject.FindObjectsOfType<EnemyBehaviour>(true))
            {
                if (enemy.CompareTag("Cube"))
                {
                    Undo.RecordObject(enemy.gameObject, "Disable/Enable Cubes");
                    enemy.gameObject.SetActive(!enemy.gameObject.activeSelf);
                }
            }
            cubesEnabled = !cubesEnabled;
        }

        if(spheresEnabled)
            GUI.backgroundColor = UnityEngine.Color.green;
        else if(!spheresEnabled)
            GUI.backgroundColor = UnityEngine.Color.red;

        if (GUILayout.Button("Disable/Enable Spheres", GUILayout.Height(50)))
        {
            foreach (var enemy in GameObject.FindObjectsOfType<EnemyBehaviour>(true))
            {
                if (enemy.CompareTag("Sphere"))
                {
                    Undo.RecordObject(enemy.gameObject, "Disable/Enable Spheres");
                    enemy.gameObject.SetActive(!enemy.gameObject.activeSelf);
                }
            }
            spheresEnabled = !spheresEnabled;
        }
        EditorGUILayout.EndHorizontal();
    }
}