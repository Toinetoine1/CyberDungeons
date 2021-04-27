using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private float _timer;
    // Start is called before the first frame update
    void Start()
    {
        _timer = 60f;
    }

    // Update is called once per frame
    void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer < 0)
        {
            _timer = 60f;
            FindObjectOfType<AudioManager>().Play("PlayerHeroSound");
        }
    }
}
