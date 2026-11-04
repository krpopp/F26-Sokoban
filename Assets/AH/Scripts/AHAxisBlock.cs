using UnityEngine;

namespace AHSokoban
{
    // 继承老师的 Block，只增加“横竖交替”的限制。
    // 同一物体不要再添加第二个 Block 组件。
    public class AHAxisBlock : Block
    {
        // true = 只能左右推；false = 只能前后推。
        public bool horizontal = true;
        public Transform axisMarker;

        protected override void Start()
        {
            base.Start();
            RefreshMarker();
        }

        public override bool CheckMove(Vector2 delta)
        {
            // 网格的 x/y 分别对应场景的 X/Z。方向不符合规则就拒绝。
            if (horizontal ? delta.y != 0 : delta.x != 0) return false;
            // 真正的阻挡检查和移动仍然交给老师的父类。
            if (!base.CheckMove(delta)) return false;
            // 成功启动一次移动，才切换下一次允许的方向。
            // 不放进 FinishMove，因为底座可能重复调用它。
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
