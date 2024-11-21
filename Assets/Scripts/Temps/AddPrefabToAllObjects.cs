using UnityEditor;
using UnityEngine;

public class AddPrefabToSelectedObjects : EditorWindow
{
    private GameObject prefabToAdd;
    private GameObject[] selectedObjects;

    [MenuItem("Custom Tools/Add Prefab To Selected Objects")]
    public static void ShowWindow()
    {
        GetWindow<AddPrefabToSelectedObjects>("Add Prefab To Selected Objects");
    }

    void OnGUI()
    {
        prefabToAdd =
            EditorGUILayout.ObjectField("Prefab to Add", prefabToAdd, typeof(GameObject), false) as GameObject;

        // Отображение списка выбранных объектов
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Selected Objects", EditorStyles.boldLabel);
        EditorGUI.indentLevel++;
        for (int i = 0; i < selectedObjects.Length; i++)
        {
            EditorGUILayout.ObjectField(selectedObjects[i], typeof(GameObject), true);
        }

        EditorGUI.indentLevel--;

        if (GUILayout.Button("Add Prefab To Selected Objects"))
        {
            AddPrefab();
        }
    }

    void OnSelectionChange()
    {
        // Обновление выбранных объектов при изменении выбора в редакторе
        selectedObjects = Selection.gameObjects;
        Repaint();
    }

    void AddPrefab()
    {
        if (prefabToAdd == null)
        {
            Debug.LogError("Prefab is not assigned.");
            return;
        }

        // Добавление префаба к каждому выбранному объекту
        foreach (GameObject obj in selectedObjects)
        {
            if (obj != null && obj != prefabToAdd)
            {
                GameObject newObject = PrefabUtility.InstantiatePrefab(prefabToAdd) as GameObject;

               // Quaternion parentRotation = obj.transform.rotation;

                newObject.transform.parent = obj.transform;
                newObject.transform.localPosition = new Vector3(0f, -0.52f, 0f); // Префаб появляется в той же позиции, что и объект
                newObject.transform.localRotation = new Quaternion(180,0,0,0);
                //newObject.transform.localRotation = Quaternion.identity;
                //newObject.transform.localScale = Vector3.one;

            }
        }
    }
}