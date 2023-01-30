#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Enemy))]
[CanEditMultipleObjects]
public class EnemiesEditor : Editor
{
    private void OnSceneGUI() 
    {
        // Enemy e = target as Enemy;
        // if (e.spawnLists != null)
        // {
        //     CustomSpawnPoint(e);
        // }
    }

    private void CustomSpawnPoint(Enemy e)
    {
        Vector3[] listPoint = e.spawnLists;

        // for each line segment we need two indices into the points array:
        // the index to the start and the end point
        int[] segmentIndices = new int[listPoint.Length*2];

        // create the points and line segments indices
        int prevIndex = listPoint.Length - 1;
        int pointIndex = 0;
        int segmentIndex = 0;

        for (int i = 0; i < e.spawnLists.Length; i++)
        {
            Vector3 pos = listPoint[i];

            // the index to the start of the line segment
            segmentIndices[segmentIndex] = pointIndex;
            segmentIndex++;

            pointIndex++;
            prevIndex = i;

            // draw a list of indexed dooted line segments
            // draw arrow dir

            // Handles.color = Color.blue;
            Handles.DrawDottedLines(listPoint, segmentIndices, 3);
            int nextIndexPoint = i >= listPoint.Length - 1 ? 0 : i + 1;
            float distanceToNextPoint = Vector3.Distance(pos, listPoint[nextIndexPoint]);
            // Handles.color = Color.yellow;
            for (int j = 0; j <= distanceToNextPoint/4; j += 2)
            {
                Vector3 dir = (listPoint[nextIndexPoint] - pos).normalized;
                if (dir != Vector3.zero)
                {
                    Vector3 posOfArrow = pos + dir*j;
                    Handles.ArrowHandleCap(i, posOfArrow, Quaternion.LookRotation(dir), 2f, EventType.Repaint);
                }
            }

            // draw a button point
            Handles.Label(pos, $"Point {i+1}", "TextField");
            // begin check change on editor
            EditorGUI.BeginChangeCheck();
            Vector3 newPos = Handles.PositionHandle(pos, Quaternion.identity);

            if (EditorGUI.EndChangeCheck())
            {
                // update position point
                Undo.RecordObject(e, "Update Spawn Point");
                listPoint[i] = newPos;
                EditorUtility.SetDirty(e);
            }
        }
    }
}
#endif
