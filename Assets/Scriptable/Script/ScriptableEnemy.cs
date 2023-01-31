using UnityEngine;

[CreateAssetMenu(fileName = "Scriptables Enemy", menuName = "Scriptable Enemy Objects/New Scriptables")]
public class ScriptableEnemy : ScriptableObject
{
    public EnemySO[] enemies;
}
