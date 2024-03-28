using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.UI
{
    public class EmployeeUI: MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Image _image;


        public async UniTask AnimateWaiting(float time)
        {

            Sequence sequence = DOTween.Sequence()
                .Append(_canvasGroup.DOFade(1, 0.2f).From(0)) 
                .Join(_image.DOFillAmount(1, time - 0.4f).From(0)) 
                .AppendInterval(time - 0.4f) 
                .Append(_canvasGroup.DOFade(0, 0.2f)); 
            
            await sequence.AsyncWaitForCompletion();
        }

    }
}