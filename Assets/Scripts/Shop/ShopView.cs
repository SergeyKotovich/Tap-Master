using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Shop
{
    public class ShopView : MonoBehaviour
    {
        [SerializeField] private Color _initialColor;
        [SerializeField] private Button _skinsButton;
        [SerializeField] private Button _boostersButton;

        private Button _selectedButton;
        private void Awake()
        {
            _skinsButton.onClick.AddListener(() => OnButtonClick(_skinsButton));
            _boostersButton.onClick.AddListener(() => OnButtonClick(_boostersButton));
        }
        
        private void OnButtonClick(Button clickedButton)
        {
            if (_selectedButton != null)
            {
                DeselectButton(_selectedButton);
            }
            _selectedButton = clickedButton;
            SelectButton(_selectedButton);
            
        }
        
        private void SelectButton(Button button)
        {
            var images = button.gameObject.GetComponentsInChildren<Image>();
            for (int i = 0; i < images.Length; i++)
            {
                if (images[i].gameObject.CompareTag("Icon"))
                {
                    images[i].color = Color.white;
                }
            }
        }

        private void DeselectButton(Button button)
        {
            var images = button.gameObject.GetComponentsInChildren<Image>();
            for (int i = 0; i < images.Length; i++)
            {
                if (images[i].gameObject.CompareTag("Icon"))
                {
                    images[i].color = _initialColor;
                }
            }
        }

    }
}