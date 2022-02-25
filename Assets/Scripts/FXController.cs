using UnityEngine;

public class FXController : MonoBehaviour
{
    [SerializeField] private float _lifeTime;
    [SerializeField] private bool _destroyAfterLife = false;

    private float _timer;

    private void OnEnable()
    {
        _timer = _lifeTime;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            if (_destroyAfterLife)
                Destroy(gameObject);
            else
                gameObject.SetActive(false);
        }
    }
}
