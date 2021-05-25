using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using TMPro;

public class IuManager : MonoBehaviour
{
    [SerializeField] private Ease _ease;
    [SerializeField] private GameManager _gameManager;
    #region UI
    [SerializeField] private Button[] _itemButtons;
    [SerializeField] private Image _findImage;
    [SerializeField] private TextMeshProUGUI _find;
    [SerializeField] private GameObject _panelGameOver;
    #endregion
    public event Action<string> SendCheck;
    private int _findIndex;

    private void Awake()
    {
        _gameManager.ChangeMode += SetItem;
    }
    private void Start()
    {
        _panelGameOver.SetActive(false);
        _find.DOFade(1, 5f);
    }
    private void SetItem(Item item)
    {
        OffButtons();
        for (int i = 0; i < item.Sprite.Length; i++)
        {
            _itemButtons[i].transform.DOScale(1f, 1f)
                   .SetEase(_ease);
            _itemButtons[i].gameObject.SetActive(true);
            _itemButtons[i].transform.GetChild(0).GetComponent<Image>().sprite = item.Sprite[i];
        }
        _findIndex = UnityEngine.Random.Range(0, item.Sprite.Length);
        _findImage.sprite = item.Sprite[_findIndex];
        _findImage.transform.DOScale(1f, 1f)
                   .SetEase(_ease);
    }

    public void Check(int index)
    {
        if (index == _findIndex)
        {
            _itemButtons[index].GetComponent<ParticleSystem>().Play();
            var seq = DOTween.Sequence();
            seq.Append(_itemButtons[index].transform.DOPunchScale(new Vector2(0.5f, 0.5f), 0.7f));
            seq.OnComplete(Complete);
        }
        else
        {
            _itemButtons[index].transform.DOShakePosition(2f , 10f);
        }
    }
    public void GameOver()
    {
        _panelGameOver.SetActive(true);
        var seq = DOTween.Sequence();
        seq.Append(_panelGameOver.GetComponent<Image>().DOFade(0.85f, 1f));
        seq.OnComplete(OnButton);
    }
    private void Complete()
    {
        SendCheck?.Invoke("win");
    }
    private void OnButton()
    {
        _panelGameOver.transform.GetChild(0).gameObject.SetActive(true);
    }
    private void OffButtons()
    {
        for (int i = 0; i < _itemButtons.Length; i++)
            _itemButtons[i].transform.localScale = new Vector3(0.1f, 0.1f, 1f);
    }
}
