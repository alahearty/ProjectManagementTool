using System.Windows.Media;

namespace Gantt.ChartLib.Controls
{
    /// <summary>
    /// The base class for visual rendering
    /// </summary>
    public abstract class RenderingBase : DrawingVisual
    {
        /// <summary>
        /// Create/Recreates the visual element
        /// </summary>
        internal void InvalidateVisual()
        {
            using (var drawingContext = RenderOpen())
            {
                CreateVisual(drawingContext);
            }
        }

        /// <summary>
        /// Creates a visual representation of this element
        /// </summary>
        /// <param name="dc">Drawing context on which to draw</param>
        protected virtual void CreateVisual(DrawingContext dc) { }

        /// <summary>
        /// Indicates whether this curve has a visual parent
        /// </summary>
        public bool HasVisualParent { get => VisualParent != null; }
    }
}
