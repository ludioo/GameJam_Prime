using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Person_SO", menuName = "Person_SO")]
public class Person_SO : ScriptableObject
{
    public string personName;
    public Sprite personImage;

    [TextArea]
    public string personDescription;

    [Space]
    public List<string> question;
    public List<string> response;
    public bool haveMentalHealth;
}
