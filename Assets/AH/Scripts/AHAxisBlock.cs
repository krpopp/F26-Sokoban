using UnityEngine;

namespace AHSokoban
{
    // A NEW Block subclass. Never attach this alongside another Block component.
    public class AHAxisBlock : Block
    {
        public bool horizontal = true;
        public Transform axisMarker;

        protected override void Start()
        {
            base.Start();
            RefreshMarker();
        }

        public override bool CheckMove(Vector2 delta)
        {
            if (horizontal ? delta.y != 0 : delta.x != 0) return false;
            if (!base.CheckMove(delta)) return false;
            // Flip only after a successful move. The supplied MoveLoop can call
            // FinishMove twice, so changing the axis there would cancel itself.
            horizontal = !horizontal;
            RefreshMarker();
            return true;
        }

        void RefreshMarker()
        {
            if (axisMarker != null)
                axisMarker.localRotation = Quaternion.Euler(0, horizontal ? 0 : 90, 0);
        }
    }
}
