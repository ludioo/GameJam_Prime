using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public List<GameObject> convoNPC = new List<GameObject>();
    public void ChooseNPC(int index)
    {
        for (int i = 0; i < convoNPC.Count; i++)
        {
            convoNPC[i].SetActive(false);
            if(index == i)
            {
                convoNPC[i].SetActive(true);
            }
        }
    }
}
