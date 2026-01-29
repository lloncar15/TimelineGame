using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerCharacterProperties", order = 1)]
public class PlayerCharacterProperties : ScriptableObject
{
    public float _moveSpeed, _jumpSpeed;
}
