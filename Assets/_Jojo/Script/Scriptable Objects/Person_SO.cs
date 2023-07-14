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

    [System.Serializable]
    public class PersonResponse
    {
        public string response;
        public bool isBluffing;
    }

    [Space]
    public List<string> questions;
    public List<string> responses;
    public List<string> otherResponses;
    public bool haveMentalHealth;
}
