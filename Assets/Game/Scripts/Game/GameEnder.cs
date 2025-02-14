using UnityEngine;

public class GameEnder : MonoBehaviour
{
    private GameObject _winImage;
    private ObjectsCollector _objectsCollector;

    public void Init(GameObject winImage, ObjectsCollector objectsCollector)
    {
        _objectsCollector = objectsCollector;
        _winImage = winImage;
        _winImage.SetActive(false);

        _objectsCollector.OnCollectAll += GameWin;
    }
    private void OnDisable()
    {
        _objectsCollector.OnCollectAll -= GameWin;
    }

    private void GameWin()
    {
        _winImage.SetActive(true);
    }
}
