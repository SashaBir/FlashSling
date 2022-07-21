using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class MovingRepeater : MonoBehaviour
{
    [SerializeField] private Vector2 _forward;
    [SerializeField] [Min(0)] private float _fromDuration;
    [SerializeField] [Min(0)] private float _toDuration;
    
    private Tween _tween;
    
    private void Awake() => Repeat();

    private async UniTaskVoid Repeat()
    {
        while (true)
        {
            var initial = transform.position;
            
            Vector3 target = initial + (Vector3)_forward;
            _tween = transform.DOMove(target, _toDuration).SetEase(Ease.Linear);
            await UniTask.WaitWhile(() => _tween.IsPlaying() == true);
            
            _tween = transform.DOMove(initial, _fromDuration).SetEase(Ease.Linear);
            await UniTask.WaitWhile(() => _tween.IsPlaying() == true);
        }
    }
}