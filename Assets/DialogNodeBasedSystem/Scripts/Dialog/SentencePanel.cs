using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace cherrydev
{
    public class SentencePanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI dialogNameText;
        [SerializeField] private TextMeshProUGUI dialogText;
        [SerializeField] private Image dialogCharacterImage;

        /// <summary>
        /// Setting dialogText max visible characters to zero
        /// </summary>
        public void ResetDialogText()
        {
            dialogText.maxVisibleCharacters = 0;
        }

        /// <summary>
        /// Set dialog text max visible characters to dialog text length
        /// </summary>
        /// <param name="text"></param>
        public void ShowFullDialogText(string text)
        {
            dialogText.maxVisibleCharacters = text.Length;
        }

        /// <summary>
        /// Assigning dialog name text, character image sprite, and dialog text
        /// </summary>
        /// <param name="name"></param>
        public void Setup(string name, string text, Sprite sprite)
        {
            dialogNameText.text = name;
            dialogText.text = text;

            if (sprite == null)
            {
                // Deactivate the character image GameObject if no sprite is provided
                dialogCharacterImage.gameObject.SetActive(false);
            }
            else
            {
                // Activate the character image GameObject and assign the sprite
                dialogCharacterImage.gameObject.SetActive(true);
                dialogCharacterImage.sprite = sprite;
            }
        }

        /// <summary>
        /// Increasing max visible characters
        /// </summary>
        public void IncreaseMaxVisibleCharacters()
        {
            dialogText.maxVisibleCharacters++;
        }
    }
}
