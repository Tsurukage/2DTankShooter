using UnityEngine;
using UnityEngine.UI;

public class Top_UI_Manager : MonoBehaviour
{
    [SerializeField] private Transform _tankCountUi;
    [SerializeField] private Transform _shootCountUi;
    [SerializeField] private Transform _badgeCountUi;
    [SerializeField] private Transform _animalCountUi;
    
    public void SetTankCount(int tank)
    {
        if (_tankCountUi != null)
        {
            var text = _tankCountUi.GetComponentInChildren<Text>();
            text.text = tank.ToString();
        }
    }
    public void SetShootCount(int shoot)
    {
        if (_shootCountUi != null)
        {
            var text = _shootCountUi.GetComponentInChildren<Text>();
            text.text = shoot.ToString();
        }
    }
    public void SetBadgeCount(int badge)
    {
        if (_badgeCountUi != null)
        {
            var text = _badgeCountUi.GetComponentInChildren<Text>();
            text.text = badge.ToString();
        }
    }
    public void SetAnimalCount(int animal)
    {
        if(_animalCountUi != null)
        {
            var text = _animalCountUi.GetComponentInChildren<Text>();
            text.text = animal.ToString();
        }
    }
}
