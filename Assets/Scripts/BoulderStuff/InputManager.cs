using UnityEngine;

namespace Input
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private InputController[] inputControllers = new InputController[1];
        public InputController[] InputControllers => inputControllers;
    }
}
