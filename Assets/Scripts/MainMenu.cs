using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    public Text ScrollCount;
    public Text BookCount;
    public Image ProgressBar1;
    public Image ProgressBar2;
    public GameObject ProgressBarContainer1;
    public GameObject ProgressBarContainer2;
    public GameObject Massage1;
    public GameObject Massage2;
    public RectTransform FabricPos1;
    public RectTransform FabricPos2;
    public RectTransform ItemFly1;
    public RectTransform ItemFly2;
    public RectTransform EndPos1;
    public RectTransform EndPos2;
    public Camera MainCamera;
    public Text AddTextScroll;
    public Text SubTextScroll;
    public Text AddTextBook;
    public bool ClickButton1;
    public bool ClickButton2;

    private int _scrolls;
    private int _books;
    private int _addScroll;
    private int _addBook;
    private Vector3 _camPosition1;
    private Vector3 _camPosition2;
    private Vector3 _camPosition3;
    private Vector3 _camPosition4;

    public void AddScroll()
    {

         _scrolls++;
    }

    public void AddBooks()
    {

        //_scrolls = _scrolls - ;
        //_books = _books + 3;
    }

    public void WarningMassage1()
    {

        Massage1.gameObject.SetActive(true);
        StartCoroutine(MassageAlarm1());

    }

    public void WarningMassage2()
    {

        Massage2.gameObject.SetActive(true);
        StartCoroutine(MassageAlarm2());
      
    }
       
    IEnumerator MassageAlarm1()
    {

        yield return new WaitForSeconds(1f);
        Massage1.gameObject.SetActive(false);
    }

    IEnumerator MassageAlarm2() 
    {

        yield return new WaitForSeconds(1f);
        Massage2.gameObject.SetActive(false);
    }

 
    IEnumerator CounterScroll()
    {
        _addScroll = -10;

        //Debug.Log(_addScroll);

        while (_addScroll < 0) 
        {

            _scrolls--;
            yield return new WaitForSeconds(0.03f);
            _addScroll++;
        }
        
    }

    IEnumerator CounterBook()
    {
        _addBook = 3;

        //Debug.Log(_addBook);

        while (_addBook > 0)
        {
        
            _books ++;
            yield return new WaitForSeconds(0.03f);
            _addBook--;
        }

    }

    public void PlusScroll() 
    {
        
        var Seq = DOTween.Sequence();
        
        Seq.Append(AddTextScroll.rectTransform.DOLocalMoveY(-50, 0.5f , false));
        Seq.Join(AddTextScroll.DOFade(1, 0.3f).SetEase(Ease.Linear));
        Seq.Insert(0.3f, AddTextScroll.DOFade(0, 0.2f).SetEase(Ease.Linear));
        Seq.Insert(0.5f, AddTextScroll.rectTransform.DOLocalMoveY(-150, 0, false));

    }

    public void MinusScroll()
    {

        var Seq = DOTween.Sequence();

        Seq.Append(SubTextScroll.rectTransform.DOLocalMoveY(-150, 0.5f, false));
        Seq.Join(SubTextScroll.DOFade(1, 0.3f).SetEase(Ease.Linear));
        Seq.Insert(0.3f, SubTextScroll.DOFade(0, 0.2f).SetEase(Ease.Linear));
        Seq.Insert(0.5f, SubTextScroll.rectTransform.DOLocalMoveY(-50, 0, false));

    }

    public void PlusBooks()
    {
        
        var Seq = DOTween.Sequence();

        Seq.Append(AddTextBook.rectTransform.DOLocalMoveY(-50, 0.5f, false));
        Seq.Join(AddTextBook.DOFade(1, 0.3f).SetEase(Ease.Linear));
        Seq.Insert(0.3f, AddTextBook.DOFade(0, 0.2f).SetEase(Ease.Linear));
        Seq.Insert(0.5f, AddTextBook.rectTransform.DOLocalMoveY(-150, 0, false));

    }

    public void ScrollFlying()
    {
        _camPosition3 = EndPos1.anchoredPosition3D;

        //Debug.Log(EndPos1.anchoredPosition3D);

        ItemFly1.position = _camPosition1;
        ItemFly1.gameObject.SetActive(true);
        ItemFly1.DOAnchorPos3D(_camPosition3, 1, false).OnComplete(() =>
        {
            PlusScroll();
            AddScroll();
            ClickButton1 = true;
            ItemFly1.gameObject.SetActive(false);
        });
    }

    public void BookFlying()
    {
        _camPosition4 = EndPos2.anchoredPosition3D;

        //Debug.Log(EndPos2.anchoredPosition3D);

        ItemFly2.position = _camPosition2;
        ItemFly2.gameObject.SetActive(true);
        ItemFly2.DOAnchorPos3D(_camPosition4, 1, false).OnComplete(() =>
        {
            MinusScroll();
            PlusBooks();
            StartCoroutine(CounterScroll());
            StartCoroutine(CounterBook());
            CounterScroll();
            CounterBook();
            ClickButton2 = true;
            ItemFly2.gameObject.SetActive(false);
        });
    }

    public void StartProduction1()
    {

        if (ClickButton1 == true)
        {
            ClickButton1 = false;
            ProgressBarContainer1.gameObject.SetActive(true);
            ProgressBar1.DOFillAmount(1, 1).SetEase(Ease.Linear).OnComplete(() =>
            {

                ProgressBar1.DOFillAmount(0, 0).SetEase(Ease.Linear);
                ProgressBarContainer1.gameObject.SetActive(false);
                ScrollFlying();
            });
        }
        else 
        {

            WarningMassage1();
        }
    }

    public void StartProduction2()
    {
        if (ClickButton2 == true && _scrolls >= 10)
        {

            ClickButton2 = false;
            ProgressBarContainer2.gameObject.SetActive(true);
            ProgressBar2.DOFillAmount(1, 1).SetEase(Ease.Linear).OnComplete(() =>
            {

                ProgressBar2.DOFillAmount(0, 0).SetEase(Ease.Linear);
                ProgressBarContainer2.gameObject.SetActive(false);
                BookFlying();
            });

        }
        else 
        {
            WarningMassage2();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        ScrollCount.text = _scrolls.ToString();
        BookCount.text = _books.ToString();

        //Debug.Log(MainCamera.WorldToScreenPoint(FabricPos1.anchoredPosition));
                
        _camPosition1 = MainCamera.WorldToScreenPoint(FabricPos1.anchoredPosition);

        //Debug.Log(MainCamera.WorldToScreenPoint(FabricPos2.anchoredPosition));

        _camPosition2 = MainCamera.WorldToScreenPoint(FabricPos2.anchoredPosition);

        //camPosition.x *= (float)1080 / Screen.width;
        //camPosition.y *= (float)1920 / Screen.height;

        //ItemFly1.position = _camPosition1;

        //ItemFly2.position = _camPosition2;


    }
}
