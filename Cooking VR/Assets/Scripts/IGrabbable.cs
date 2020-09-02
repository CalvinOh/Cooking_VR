using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Valve.VR.InteractionSystem;

namespace Assets.Scripts
{
    public interface IGrabbable
    {
        Interactable interactable { get; set; }
        bool wasGrabbed { get; set; }

        void InitGrabbable();
        void CheckGrabbed();
    }
}
