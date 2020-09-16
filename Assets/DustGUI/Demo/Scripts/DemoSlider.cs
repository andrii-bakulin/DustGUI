using UnityEngine;

namespace DustDemo
{
    public class DemoSlider : MonoBehaviour
    {
        [SerializeField] private float value1 = 1.5f;
        [SerializeField] private float value2 = 5.0f;

        private void Update()
        {
            Debug.Log("Float Value1 = " + value1.ToString("F3"));
            Debug.Log("Float Value2 = " + value2.ToString("F3"));
        }
    }
}
