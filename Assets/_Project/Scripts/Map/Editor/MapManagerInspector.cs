using UnityEngine;
using UnityEditor;

namespace Map
{
    [CustomEditor(typeof(MapManager))]

    public class MapManagerInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var myScript = (MapManagerInspector)target;

            GUILayout.Space(10);

            //if (GUILayout.Button("Generate"))
                //myScript.GenerateNewMap();
        }
    }
}
