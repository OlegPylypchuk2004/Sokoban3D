using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Data/Levels")]
public class LevelData : ScriptableObject
{
    [field: SerializeField] public int Number { get; private set; }
    [field: SerializeField] public Field FieldPrefab { get; private set; }
}