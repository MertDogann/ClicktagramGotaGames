using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Women Human" , menuName = "Scriptable")  ]
public class PlayerData : ScriptableObject
{
    public List<Color> colors = new List<Color>();
    public Color defultColor;
    
    
}
