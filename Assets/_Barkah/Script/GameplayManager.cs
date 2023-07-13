using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [Header("NPC")]
    public List<GameObject> convoNPC = new List<GameObject>();
    
    [Header("Panel")]
    public GameObject panelNotes;
    public GameObject photoPreview;

    [Header("Others")]
    public Animator animator_photoPreview;
    public Animation animation_photoPreview;
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

    public void OpenNotes(bool openThis)
    {
        if (openThis)
        {
            panelNotes.transform.localPosition = new Vector3(1280.3f, 0, 0);
            panelNotes.SetActive(true);
            LeanTween.moveLocalX(panelNotes, 0, 0.25f);
        }
        else
        {
            LeanTween.moveLocalX(panelNotes, 1280.3f, 0.25f).setOnComplete(() =>
                panelNotes.SetActive(false));
        }
    }

    public IEnumerator IE_PreviewPicture(bool openThis)
    {
        if(openThis)
        {
            photoPreview.SetActive(true);
        }
        else
        {
            animator_photoPreview.SetTrigger("Close");
            yield return new WaitForSeconds(animation_photoPreview.clip.length);
            photoPreview.SetActive(false);
        }
    }

    public void PreviewPicture(bool openThis)
    {
        StartCoroutine(IE_PreviewPicture(openThis));
    }
}
