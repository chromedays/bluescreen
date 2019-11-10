using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform), typeof(GridLayoutGroup))]
public class StickManLifeBar : MonoBehaviour
{
    public GameObject LifePrefab;

    private RectTransform _rect;
    private GridLayoutGroup _grid;

    // Start is called before the first frame update
    void Start()
    {
        _rect = GetComponent<RectTransform>();
        _grid = GetComponent<GridLayoutGroup>();
        Game.Inst.StickManLifeBar = this;
    }

    public void InitLife(int lifeCount)
    {
        _rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, lifeCount * _grid.cellSize.x);
        for (int i = 0; i < lifeCount; ++i)
        {
            var life = Instantiate(LifePrefab);
            life.transform.SetParent(transform);
        }
        //_grid.SetLayoutHorizontal();
    }
}
