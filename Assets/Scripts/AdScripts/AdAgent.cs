using UnityEngine;
using UnityEngine.UI;

public class AdAgent : MonoBehaviour
{
    [SerializeField]private Text _message;
    [SerializeField]private Button _requestButton;
    private PangleAdController _pangleAdController;
    private PangleAdController PangleAdController => _pangleAdController ??= new PangleAdController();

    private void Start()
    {
        _message.text = "初始化中...";
        PangleAdController.Init((success, message) =>
        {
            _message.text = message;
        });
        _requestButton.onClick.AddListener(() =>
        {
            _message.text = "加载中...";
            PangleAdController.RequestDirectRewardedAd((success, message) =>
            {
                _message.text = message;
                if (success) return;
                _message.text = message;
            });
        });
    }
}