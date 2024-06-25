using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bardent
{
    public class CamSwitch : MonoBehaviour
    {
        public CinemachineVirtualCamera playerCam;
        public CinemachineVirtualCamera staticCam;
        // Start is called before the first frame update
        public void SwitchPriority()
        {
            staticCam.Priority = 11;
            playerCam.Priority = 10;
        }
    }
}
