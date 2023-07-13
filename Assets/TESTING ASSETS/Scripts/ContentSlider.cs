using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ContentSlider : MonoBehaviour
{
    [SerializeField] private Scrollbar scrollbar;

    [SerializeField] private VideoPlayer[] videoPlayers;

    //[SerializeField] private VideoPlayer video1;
    //[SerializeField] private VideoPlayer video2;
    //[SerializeField] private VideoPlayer video3;

    public float scrollPosition = 0;
    public float[] contentPosition;

    public int posisi = 0;

    private void Awake()
    {
        scrollbar = scrollbar.GetComponent<Scrollbar>();
    }

    // Update is called once per frame
    void Update()
    {
        // untuk menyesuaikan ukuran array dengan jumlah content
        contentPosition = new float[transform.childCount];

        // untuk mencari jarak
        float distance = 1f / (contentPosition.Length - 1f);

        // loop untuk menentukan posisi dari masing-masing content
        for (int i=0; i<contentPosition.Length; i++)
        {
            contentPosition[i] = distance * i;
        }

        // mengambil input value dari scrollbar ketika mouse sedang drag
        if (Input.GetMouseButton(0))
        {
            scrollPosition = scrollbar.value;
        }

        // ketika mouse dilepas
        else
        {
            for (int i = 0; i < contentPosition.Length; i++)
            {
                // kalau posisi scroll tidak pas dengan posisi content, posisi scroll akan disesuaikan ke posisi content
                if (scrollPosition < contentPosition[i] + (distance / 2) && scrollPosition > contentPosition[i] - (distance / 2))
                {
                    scrollbar.value = Mathf.Lerp(scrollbar.value, contentPosition[i], 0.15f);
                    posisi = i;
                }
            }
        }
    }
}
