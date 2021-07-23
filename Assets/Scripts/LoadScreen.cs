using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LoadScreen : MonoBehaviour
{
    public Image LoadBG;
    public Image Logo;
    public GameObject ScreenLoad;

    // Start is called before the first frame update
    void Start()
    {

        var Seq = DOTween.Sequence();

        Seq.Append(Logo.DOFade(1, 3));

        Seq.AppendInterval(2);

        Seq.Append(Logo.DOFade(0, 1.5f));

        Seq.Join(LoadBG.DOFade(0, 2.5f).SetEase(Ease.InExpo)).OnComplete(() =>
        {

            ScreenLoad.SetActive(false);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
