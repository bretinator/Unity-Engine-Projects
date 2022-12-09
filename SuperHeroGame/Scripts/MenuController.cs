using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public CharacterSelection characterSelection;
    public AudioSource audioSource;
    public AudioClip bigBrainTheme;
    public AudioClip pirateTheme;
    public AudioClip glacierTheme;
    public TextMeshProUGUI characterName;
    private List<GameObject> characters = new List<GameObject>();
    private List<string> characterNames = new List<string>();
    private List<AudioClip> characterThemes = new List<AudioClip>();
    int selectedIndex = 0;

    private void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("Persist").GetComponent<AudioSource>();
        GameObject charactersParent = GameObject.FindGameObjectWithTag("Characters");
        characters.Add(charactersParent.transform.GetChild(0).gameObject);
        characters.Add(charactersParent.transform.GetChild(1).gameObject);
        characters.Add(charactersParent.transform.GetChild(2).gameObject);
        characterNames.Add("Big Brain");
        characterNames.Add("Pirate");
        characterNames.Add("Glacier");
        characterThemes.Add(bigBrainTheme);
        characterThemes.Add(pirateTheme);
        characterThemes.Add(glacierTheme);
        audioSource.clip = bigBrainTheme;
        audioSource.Play();
        characterSelection.selectedCharacter = characters[0];
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void QuitProgram()
    {
        Application.Quit();
    }

    public void NextCharacter()
    {
        characters[selectedIndex].SetActive(false);
        audioSource.Pause();
        Vector3 currentRot = characters[selectedIndex].transform.eulerAngles;
        if (selectedIndex < 2)
        {
            selectedIndex += 1;
        }
        else if (selectedIndex == 2)
        {
            selectedIndex = 0;
        }
        characters[selectedIndex].SetActive(true);
        characterName.text = characterNames[selectedIndex];
        characters[selectedIndex].transform.eulerAngles = currentRot;
        audioSource.clip = characterThemes[selectedIndex];
        audioSource.Play();
        characterSelection.selectedCharacter = characters[selectedIndex];
    }

    public void PreviousCharacter()
    {
        characters[selectedIndex].SetActive(false);
        audioSource.Pause();
        Vector3 currentRot = characters[selectedIndex].transform.eulerAngles;
        if (selectedIndex > 0)
        {
            selectedIndex -= 1;
        }
        else if (selectedIndex == 0)
        {
            selectedIndex = 2;
        }
        characters[selectedIndex].SetActive(true);
        characterName.text = characterNames[selectedIndex];
        characters[selectedIndex].transform.eulerAngles = currentRot;
        audioSource.clip = characterThemes[selectedIndex];
        audioSource.Play();
        characterSelection.selectedCharacter = characters[selectedIndex];
    }
}
