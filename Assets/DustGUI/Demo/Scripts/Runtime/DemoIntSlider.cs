using UnityEngine;

namespace DustDemo
{
    public class DemoIntSlider : MonoBehaviour
    {
        public int value0 = 10;
        [SerializeField] private int value1 = 15;
        [SerializeField] private int value2 = 50;
        public int value3;
        public int value4;
        public int value5;

        private void Update()
        {
            Debug.Log("Int Value1 = " + value1.ToString());
            Debug.Log("Int Value2 = " + value2.ToString());
        }
    }
}
