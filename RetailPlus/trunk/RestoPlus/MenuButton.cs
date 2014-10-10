namespace AceSoft.RetailPlus.Client
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    class MenuButton : Button
    {
        Color gradientTop = Color.FromArgb(255, 153, 198, 241);
        Color gradientBottom = Color.FromArgb(255, 44, 85, 177);

        [Category("Appearance"), Description("The color to use for the top portion of the gradient fill of the component.")]
        public Color GradientTop
        {
            get
            {
                return this.gradientTop;
            }
            set
            {
                this.gradientTop = value;
                this.Invalidate();
            }
        }

        [Category("Appearance"), Description("The color to use for the bottom portion of the gradient fill of the component.")]
        public Color GradientBottom
        {
            get
            {
                return this.gradientBottom;
            }
            set
            {
                this.gradientBottom = value;
                this.Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            Graphics g = pevent.Graphics;
            // Fill the background
            using (SolidBrush backgroundBrush = new SolidBrush(this.BackColor))
            {
                g.FillRectangle(backgroundBrush, this.ClientRectangle);
            }
            // Paint the outer rounded rectangle
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle outerRect = new Rectangle(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - 1, ClientRectangle.Height - 1);
            using (GraphicsPath outerPath = RoundedRectangle(outerRect, 5, 0))
            {
                using (LinearGradientBrush outerBrush = new LinearGradientBrush(outerRect, gradientTop, gradientBottom, LinearGradientMode.Vertical))
                {
                    g.FillPath(outerBrush, outerPath);
                }
                using (Pen outlinePen = new Pen(gradientTop))
                {
                    g.DrawPath(outlinePen, outerPath);
                }
            }
            // Paint the highlight rounded rectangle
            Rectangle innerRect = new Rectangle(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - 1, ClientRectangle.Height / 2 - 1);
            using (GraphicsPath innerPath = RoundedRectangle(innerRect, 5, 2))
            {
                using (LinearGradientBrush innerBrush = new LinearGradientBrush(innerRect, Color.FromArgb(255, Color.White), Color.FromArgb(0, Color.White), LinearGradientMode.Vertical))
                {
                    g.FillPath(innerBrush, innerPath);
                }
            }

            // Paint the image
            Rectangle textRect = new Rectangle(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - 1, ClientRectangle.Height - 1);
            if (this.Image != null)
            {
                Rectangle imgRect = new Rectangle(ClientRectangle.X + ((ClientRectangle.Width - this.Image.Width) / 2), ClientRectangle.Y + ((ClientRectangle.Height - this.Image.Height) / 2) - 3, this.Image.Width, this.Image.Height);
                g.DrawImage(this.Image, imgRect);

                textRect = new Rectangle(ClientRectangle.X + 2, ClientRectangle.Y + +ClientRectangle.Height - 20, ClientRectangle.Width - 2, ClientRectangle.Height - this.Image.Height - 3);
            }

            // Paint the text
            TextRenderer.DrawText(g, this.Text, this.Font, textRect, this.ForeColor, Color.Transparent, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);
        }

        private GraphicsPath RoundedRectangle(Rectangle boundingRect, int cornerRadius, int margin)
        {
            GraphicsPath roundedRect = new GraphicsPath();
            roundedRect.AddArc(boundingRect.X + margin, boundingRect.Y + margin, cornerRadius * 2, cornerRadius * 2, 180, 90);
            roundedRect.AddArc(boundingRect.X + boundingRect.Width - margin - cornerRadius * 2, boundingRect.Y + margin, cornerRadius * 2, cornerRadius * 2, 270, 90);
            roundedRect.AddArc(boundingRect.X + boundingRect.Width - margin - cornerRadius * 2, boundingRect.Y + boundingRect.Height - margin - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);
            roundedRect.AddArc(boundingRect.X + margin, boundingRect.Y + boundingRect.Height - margin - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
            roundedRect.CloseFigure();
            return roundedRect;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            this.resizeLabel();
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            this.resizeLabel();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.resizeLabel();
        }

        #region Resize Label

        //private bool mGrowing;
        public void GrowLabel()
        {
            this.AutoSize = false;
        }

        private void resizeLabel()
        {

            //if (mGrowing) return;
            //try
            //{
            //    mGrowing = true;
            //    Size sz = new Size(this.Width, Int32.MaxValue);
            //    sz = TextRenderer.MeasureText(this.Text, this.Font, sz, TextFormatFlags.WordBreak);
            //    this.Height = sz.Height;
            //}
            //finally
            //{
            //    mGrowing = false;
            //}
        }

        #endregion
        
    }
}
