using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterType
{
    Fifo,
    Lifo
}
public class CharacterBase : MonoBehaviour
{
    public string characterName;
    public CharacterType characterType;

}
