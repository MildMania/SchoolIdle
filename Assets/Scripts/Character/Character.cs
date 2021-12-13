using System.Collections.Generic;
using UnityEngine;

public class Character : Singleton<Character>
{
    [SerializeField] private CharacterFSM _characterFSM = null;
    public CharacterFSM CharacterFSM => _characterFSM;
}