using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentGenerator : MonoBehaviour
{
    public Transform FragmentParentObj;
    public GameObject FragmentPrefab;
    public Sprite[] FragmentSprites;
                                       
    public float FragRotMin = 0;
    public float FragRotMax = 360;

    public float FragMaxScale = 5;
    public float FragMinScale = 1;
                                                
    public Vector2 position;
    public float scaler;

    int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {                                               
    }

    public void CreateFragment(Vector2 position, float scaler)
    {
        if (scaler < FragMinScale)
            scaler = FragMinScale;
        if (scaler > FragMaxScale)
            scaler = FragMaxScale;
                      
        Quaternion randomRot = Quaternion.AngleAxis(Random.Range(FragRotMin, FragRotMax), Vector3.forward);
        GameObject fragment = Instantiate(FragmentPrefab, position, randomRot, FragmentParentObj);
        fragment.transform.localScale = new Vector3(scaler, scaler,1);
        fragment.GetComponent<SpriteMask>().sprite = FragmentSprites[Random.Range(0, FragmentSprites.Length)];
        ++count;
    }

}
