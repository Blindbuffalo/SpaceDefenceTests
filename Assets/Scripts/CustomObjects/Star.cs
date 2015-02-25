using UnityEngine;
using System.Collections;

public class Star  {
    public string Name { get; set; }
    public float Size { get; set; }
    
    public Star Create(string name, float size)
    {
        Name = name;
        Size = size;
        return this;
    }
}
