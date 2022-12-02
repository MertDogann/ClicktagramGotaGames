using UnityEngine ;
using UnityEngine.UI ;
using DG.Tweening ;

public class SwitchToggle : MonoBehaviour {

    GameManager gameManager;
   [SerializeField] RectTransform uiHandleRectTransform ;
   [SerializeField] Color backgroundActiveColor ;
   [SerializeField] Color toggleOffColor ;

   Image backgroundImage, handleImage ;

   Color backgroundDefaultColor, handleDefaultColor ;

   Toggle toggle ;

   Vector2 handlePosition ;

   void Awake ( ) {
      gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>(); 
      toggle = GetComponent <Toggle> ( ) ;

      handlePosition = uiHandleRectTransform.anchoredPosition ;

      backgroundImage = uiHandleRectTransform.parent.GetComponent <Image> ( ) ;
      handleImage = uiHandleRectTransform.GetComponent <Image> ( ) ;
      backgroundImage.color = backgroundActiveColor;
      backgroundDefaultColor = backgroundImage.color ;

      toggle.onValueChanged.AddListener (OnSwitch) ;
        TagControl();

      
      if (toggle.isOn)
         OnSwitch (true) ;
   }

   void OnSwitch (bool on) {
      //uiHandleRectTransform.anchoredPosition = on ? handlePosition * -1 : handlePosition ; // no anim
      uiHandleRectTransform.DOAnchorPos (on ? handlePosition * -1 : handlePosition, .4f).SetEase (Ease.InOutBack) ;

      //backgroundImage.color = on ? backgroundActiveColor : backgroundDefaultColor ; // no anim
      backgroundImage.DOColor (on ? toggleOffColor   : backgroundDefaultColor, .6f) ;

        TagControl();
    }


    void TagControl()
    {
        if (gameObject.CompareTag("VibToggle"))
        {
            gameManager.vibration = !toggle.isOn;
        }
        else if (gameObject.CompareTag("SoundToggle"))
        {
            gameManager.transform.GetChild(5).GetComponent<AudioManager>().audioClick = !toggle.isOn;
        }
    }
   void OnDestroy ( ) {
      toggle.onValueChanged.RemoveListener (OnSwitch) ;
   }
}
