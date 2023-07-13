using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance { get; set; }
    public enum State
    {
        Idle,
        Convo,
    }

    public State NowState;

    [Header("NPC")]
    public List<GameObject> spawnNPC = new List<GameObject>();
    public List<GameObject> prefabsNPC = new List<GameObject>();
    public List<GameObject> onGameplayNPC = new List<GameObject>();

    [Header("Panel")]
    public GameObject panelNotes;
    public GameObject photoPreview;
    public GameObject[] notesNPC;

    [Header("Buttons")]
    public GameObject[] allButtons;

    [Header("Animator")]
    public Animator animator_photoPreview;
    public AnimationClip animation_photoPreview;
    public Animator animator_black;
    public AnimationClip animation_black;

    [Header("Spotlights")]
    public List<GameObject> spotlights = new List<GameObject>();

    [Header("Score System")]
    public float scoreDialogue;
    public float scoreDecision;
    public float totalScore;
    public int dialogueChosen = 0;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        RandomPickAndSpawn();
    }
    public void RandomPickAndSpawn()
    {
        // Clear the onGameplayNPC list before picking new GameObjects
        onGameplayNPC.Clear();

        // Check if the number of available prefabs is less than 3
        if (prefabsNPC.Count < 3)
        {
            Debug.LogWarning("There are not enough prefabs to pick from.");
            return;
        }

        // Create a list to store the indices of selected prefabs
        List<int> selectedIndices = new List<int>();

        // Create a list to store the indices of selected spawnNPCs
        List<int> selectedSpawnIndices = new List<int>();

        // Randomly pick three GameObjects from prefabsNPC
        for (int i = 0; i < spawnNPC.Count; i++)
        {
            int randomIndex = Random.Range(0, prefabsNPC.Count);

            // Check if the index has already been selected
            while (selectedIndices.Contains(randomIndex))
            {
                randomIndex = Random.Range(0, prefabsNPC.Count);
            }

            selectedIndices.Add(randomIndex);
            GameObject selectedPrefab = prefabsNPC[randomIndex];
            GameObject randomSpawnNPC = spawnNPC[i];

            // Instantiate the selected GameObject
            GameObject instantiatedNPC = Instantiate(selectedPrefab);

            // Set the instantiated NPC as a child of the randomSpawnNPC GameObject
            instantiatedNPC.transform.parent = randomSpawnNPC.transform;
            instantiatedNPC.transform.localPosition = Vector3.zero;
            instantiatedNPC.transform.localScale = Vector3.one;

            // Add the instantiated NPC to the onGameplayNPC list
            onGameplayNPC.Add(instantiatedNPC);
        }
    }



    public void ChooseNPC(int index)
    {
        for (int i = 0; i < onGameplayNPC.Count; i++)
        {
            onGameplayNPC[i].SetActive(false);
            if (index == i)
            {
                onGameplayNPC[i].SetActive(true);
            }
        }
    }

    public void OpenNotes(bool openThis)
    {
        foreach (GameObject item in notesNPC)
        {
            item.SetActive(false);
        }
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
        if (openThis)
        {
            photoPreview.SetActive(true);
        }
        else
        {
            animator_photoPreview.SetTrigger("Close");
            yield return new WaitForSeconds(animation_photoPreview.length + 0.1f);
            photoPreview.SetActive(false);
        }
    }

    public void PreviewPicture(bool openThis)
    {
        StartCoroutine(IE_PreviewPicture(openThis));
    }

    public IEnumerator IE_ChooseSuspect(int index)
    {
        switch (NowState)
        {
            case State.Idle:
                animator_black.SetTrigger("On");
                yield return new WaitForSeconds(animation_black.length);
                foreach (GameObject item in allButtons)
                    item.SetActive(false);

                for (int i = 0; i < onGameplayNPC.Count; i++)
                {
                    if (index != i)
                        onGameplayNPC[i].GetComponent<Button>().interactable = false;
                }

                spotlights[index].SetActive(true);
                SFXManager.Instance.PlaySpotlight();
                NowState = State.Convo;
                break;

            case State.Convo:
                spotlights[index].SetActive(false);
                SFXManager.Instance.PlaySpotlight();
                animator_black.SetTrigger("Off");
                yield return new WaitForSeconds(animation_black.length);

                foreach (GameObject item in onGameplayNPC)
                    item.GetComponent<Button>().interactable = true;

                foreach (GameObject item in allButtons)
                    item.SetActive(true);

                NowState = State.Idle;
                break;
        }
    }

    public void ChooseSuspect(int index)
    {
        StartCoroutine(IE_ChooseSuspect(index));
    }

    public void NPCNotes(int index)
    {
        foreach (GameObject item in notesNPC)
        {
            item.SetActive(false);
        }
        notesNPC[index].SetActive(true);
    }

    #region ScoreSystem
    [ContextMenu("Calculate Score")]
    private void CalculateScores()
    {
        scoreDialogue = (30 - dialogueChosen) / 30f * 50f;
        totalScore = Mathf.RoundToInt(scoreDialogue + scoreDecision);
        Debug.Log("Total score = " + totalScore);
    }

    [ContextMenu("Increase Dialogue")]
    public void IncreaseDialogueChosen()
    {
        dialogueChosen++;
        CalculateScores();
    }

    public void DecreaseDialogueChosen()
    {
        dialogueChosen--;
        CalculateScores();
    }
    #endregion
}
