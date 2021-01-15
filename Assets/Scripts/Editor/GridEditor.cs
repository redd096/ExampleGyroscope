using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LabyrinthGrid))]
public class GridEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Space(10);

        if(GUILayout.Button("Regen Grid"))
        {
            ((LabyrinthGrid)target).RegenGrid();

            //set undo
            Undo.RegisterFullObjectHierarchyUndo(target, "Regen World");

            //foreach(Transform child in transform)
            //{
            //    Undo.RecordObject(child, "Regen World");
            //}

            //set scene dirty
            //using UnityEditor.SceneManagement;
            //EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }
    }
}
