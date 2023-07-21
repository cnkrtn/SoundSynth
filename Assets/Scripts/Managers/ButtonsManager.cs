using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class ButtonsManager : MonoBehaviour
    {
        [SerializeField] private GameObject selectButton;
        [SerializeField] private GameObject[] selectedBackgrounds;
        
        public void SelectAvatar(GameObject selectedBackground)
        {
            selectButton.SetActive(true);
            foreach (var background in selectedBackgrounds)
            {
                background.SetActive(false);
            }
            selectedBackground.SetActive(true);
        }

        public void Devam()
        {
            selectButton.SetActive(false);
            foreach (var background in selectedBackgrounds)
            {
                background.transform.parent.gameObject.SetActive(false);
            }
        }
    }
}
