using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyGame;
public class LaserController : MonoBehaviour
{
    private List<Laser> myLaser;
    private float bpm;
    private MapLoader _mapLoader;
    // Start is called before the first frame update
    void Start()
    {

        bpm = 70f;
        _mapLoader = GetComponentInParent<MapLoader>();
        myLaser = _mapLoader.Map.Laser;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LoadLaser(int t)
    {
        for(int i=0;i<myLaser.Count;i++)
        {
            if (myLaser[i].appearTime == t)
            {
                StartCoroutine(DrawLaser(myLaser[i]));
                myLaser.RemoveAt(i);
            }
        }
    }
    public IEnumerator DrawLaser(Laser l)
    {
        if (l.side == 0)
        {
            _mapLoader.drawAlert(new Vector2Int(l.pos, 0));

            _mapLoader.drawAlert(new Vector2Int(l.pos, 12));
        }
        else if (l.side == 1)
        {
            _mapLoader.drawAlert(new Vector2Int(0, l.pos));

            _mapLoader.drawAlert(new Vector2Int(8, l.pos));

        }

        //这里判断一下方位，画出预警点
        for (int t = 0; t < 3; t++)
        {
            yield return new WaitForSeconds(60 / bpm);//等他三秒
        }
        if (l.side  == 0)//vertical
        {
            _mapLoader.drawLine(0, l.pos);
        }
        else
        {
            //horizontal
            _mapLoader.drawLine(1, l.pos);
        }
    }
}
