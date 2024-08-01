using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


    public class CharacterSelect : MonoBehaviour
    {
        [SerializeField] private GameObject characterSelectDisplay = default;
        [SerializeField] private Transform characterPreviewParent = default;
        [SerializeField] private TMP_Text characterNameText = default;
        [SerializeField] private float turnSpeed = 90f;
        [SerializeField] private Character[] characters = default;
        [SerializeField] private Transform spawnLocationCat;
        [SerializeField] private Transform spawnLocationKeeper;
        private Transform spawnLocation;
        

        private int currentCharacterIndex = 0;
        private List<GameObject> characterInstances = new List<GameObject>();

        private static bool[] characterSelected;
        public  void Start()
        {
            if (characterPreviewParent.childCount == 0)
            {
                foreach (var character in characters)
                {
                    GameObject characterInstance =
                        Instantiate(character.CharacterPreviewPrefab, characterPreviewParent);

                    characterInstance.SetActive(false);
                    characterInstances.Add(characterInstance);
                }
            }
            if (characterSelected == null)
            {
                characterSelected = new bool[characters.Length];
            }

            characterInstances[currentCharacterIndex].SetActive(true);
            characterNameText.text = characters[currentCharacterIndex].CharacterName;

            characterSelectDisplay.SetActive(true);
        }
        
        private void Update()
        {
            characterPreviewParent.RotateAround(
                characterPreviewParent.position,
                characterPreviewParent.up,
                turnSpeed * Time.deltaTime);
        }

        public void Select()
        {
            if (currentCharacterIndex == 0)
            {
                spawnLocation = spawnLocationCat;
            }
            else if (currentCharacterIndex == 1)
            {
                spawnLocation = spawnLocationKeeper;
            }
            GameObject characterInstance = Instantiate(characters[currentCharacterIndex].GameplayCharacterPrefab, spawnLocation.position , transform.rotation);
            characterSelectDisplay.SetActive(false);
        }

        public void Right()
        {
            characterInstances[currentCharacterIndex].SetActive(false);

            currentCharacterIndex = (currentCharacterIndex + 1) % characterInstances.Count;

            characterInstances[currentCharacterIndex].SetActive(true);
            characterNameText.text = characters[currentCharacterIndex].CharacterName;
        }

        public void Left()
        {
            characterInstances[currentCharacterIndex].SetActive(false);

            currentCharacterIndex--;
            if (currentCharacterIndex < 0)
            {
                currentCharacterIndex += characterInstances.Count;
            }

            characterInstances[currentCharacterIndex].SetActive(true);
            characterNameText.text = characters[currentCharacterIndex].CharacterName;
        }
    }

