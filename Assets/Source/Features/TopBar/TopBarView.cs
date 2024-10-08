using TMPro;
using UnityEngine;

namespace Source.Features.TopBar
{
    public class TopBarView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _levelTxt;
        [SerializeField] private TMP_Text _livesTxt;

        public void SetLevel(int level)
        {
            _levelTxt.SetText($"Lvl: {level}");
        }
        
        public void SetLives(int lives)
        {
            _livesTxt.SetText($"Lives: {lives}");
        }
    }
}