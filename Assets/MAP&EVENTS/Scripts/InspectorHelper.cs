using UnityEditor;

/// <summary>
/// Displays basic info for building the GameFlow/Events System



[CustomEditor(typeof(EnemySpawner))]
public class InspectorHelper : Editor
{
    public override void OnInspectorGUI()
    {
        // Dibuja el recuadro informativo arriba del todo
        EditorGUILayout.HelpBox(
            "Este script genera enemigos en los Spawn Points asignados.\n\n" +
            "*No es necessario añadir los 3 tipos, se puede asignar a este "+ 
            "1 y complementarlo con otros spawners adicionales.*\n\n" +
            "• Bin: Objeto padre donde se guardarán las instancias.\n" +
            "• Weight: Probabilidad relativa de aparición de cada enemigo.", 
            MessageType.Info
        );

        // Dibuja el resto de variables normales de tu script
        DrawDefaultInspector();
    }
}