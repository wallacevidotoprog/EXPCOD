s;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Security;
using System.Windows.Forms;

namespace JHUI.Forms
{
	[ToolboxItem(false)]
	[DesignTimeVisible(true)]
	[Description("Provides a custom form class.")]
	public class JForm : Form, IJForm, IDisposable
	{
		private JColorStyle JStyle = JColorStyle.White;
		private JThemeStyle JTheme = JThemeStyle.Dark;
		private JStyleManager JStyleManager;
		private bool isHandleUseThemeColor;
		private Color JTopBorderColor = Color.White;
		private bool isCustomMoveHandle;
		private Image isMoveHandleImage;
		private Image XControlboxImageMaximizeHover = (Image)Resources.Minimize_Hover;
		private Image XControlboxImageMaximizeNormal = (Image)Resources.Minimize_Normal;
		private Image XControlboxMaximizeCloseDown = (Image)Resources.Minimize_Press;
		private Image XControlboxImageMinimizeHover = (Image)Resources.Minimize_Hover;
		private Image XControlboxImageMinimizeNormal = (Image)Resources.Minimize_Normal;
		private Image XControlboxMinimizeCloseDown = (Image)Resources.Minimize_Press;
		private Image XControlboxImageHideHover = (Image)Resources.XMinimize_Hover;
		private Image XControlboxImageHideNormal = (Image)Resources.XMinimize_Normal;
		private Image XControlboxHideCloseDown = (Image)Resources.XMinimize_Press;
		private Image XControlboxImageCloseHover = (Image)Resources.XClose_Hover;
		private Image XControlboxImageCloseNormal = (Image)Resources.XClose_Normal;
		private Image XCloseBtnDown = (Image)Resources.XClose_Pressed;
		private JForm.JHUIControlBoxButtons XJOrderLastButton = JForm.JHUIControlBoxButtons.Close;
		private JForm.JHUIControlBoxButtons XJOrderFirstButton = JForm.JHUIControlBoxButtons.Maximize;
		private JForm.JHUIControlBoxButtons XJOrderMiddleButton;
		private JFormTextAlign textAlign;
		private Color MBackColor;
		private bool _useCustomBackColor;
		private JFormBorderStyle formBorderStyle;
		private bool isMovable = true;
		private string thisText = "";
		private bool displayHeader = true;
		private bool isResizable = true;
		private Size ctrlsize = new Size(25, 25);
		private Size lastSize = new Size(25, 25);
		private bool thisControlBox = true;
		private JControlBoxLocation XCustomControlButtonsAlign = JControlBoxLocation.RIGHT;
		private JControlBoxType XJControlBoxType;
		private JFormShadowType shadowType = JFormShadowType.SystemShadow;
		private int ThisBorderWidth = 5;
		private int ThisBorderHeight = 5;
		private Bitmap _image;
		private Image backImage;
		private Padding backImagePadding;
		private int backMaxSize;
		private BackLocation backLocation;
		private bool _imageinvert;
		private Dictionary<JForm.JHUIControlBoxButtons, JForm.JFormButton> windowButtonList;
		private const int CS_DROPSHADOW = 131072;
		private const int WS_MINIMIZEBOX = 131072;
		private Form shadowForm;

		[Category("J Appearance")]
		[DefaultValue(true)]
		public bool PaintTopBorder { get; set; } = true;

		[Category("J Appearance")]
		public JColorStyle Style
		{
			get => this.StyleManager != null ? this.StyleManager.Style : this.JStyle;
			set => this.JStyle = value;
		}

		[Category("J Appearance")]
		public JThemeStyle Theme
		{
			get => this.StyleManager != null ? this.StyleManager.Theme : this.JTheme;
			set => this.JTheme = value;
		}

		[Browsable(false)]
		public JStyleManager StyleManager
		{
			get => this.JStyleManager;
			set => this.JStyleManager = value;
		}

		[Category("J Appearance")]
		public bool UseTemeColorForMoveHand
		{
			get => this.isHandleUseThemeColor;
			set => this.isHandleUseThemeColor = value;
		}

		[Category("J Appearance")]
		public Color TopBorderColor
		{
			get => this.JTopBorderColor;
			set
			{
				this.JTopBorderColor = value;
				this.Refresh();
			}
		}

		[Category("J Appearance")]
		public bool UseCustomMoveHandle
		{
			get => this.isCustomMoveHandle;
			set => this.isCustomMoveHandle = value;
		}

		[Category("J Appearance")]
		public Image MoveHandleImage
		{
			get => this.isMoveHandleImage;
			set
			{
				if (value != null)
					this.isMoveHandleImage = (Image)new Bitmap(value, new Size(20, 20));
				else
					this.isMoveHandleImage = value;
			}
		}

		[Browsable(true)]
		[Category("J ControlBox")]
		public Image JMaximizeMaximizedOver
		{
			get => this.XControlboxImageMaximizeHover;
			set => this.XControlboxImageMaximizeHover = value;
		}

		[Browsable(true)]
		[Category("J ControlBox")]
		public Image JMaximizeMaximizedNormal
		{
			get => this.XControlboxImageMaximizeNormal;
			set => this.XControlboxImageMaximizeNormal = value;
		}

		[Browsable(true)]
		[Category("J ControlBox")]
		public Image JMaximizeMaximizedDown
		{
			get => this.XControlboxMaximizeCloseDown;
			set => this.XControlboxMaximizeCloseDown = value;
		}

		[Browsable(true)]
		[Category("J ControlBox")]
		public Image JMaximizeNormalOver
		{
			get => this.XControlboxImageMinimizeHover;
			set => this.XControlboxImageMinimizeHover = value;
		}

		[Browsable(true)]
		[Category("J ControlBox")]
		public Image JMaximizeNormalNormal
		{
			get => this.XControlboxImageMinimizeNormal;
			set => this.XControlboxImageMinimizeNormal = value;
		}

		[Browsable(true)]
		[Category("J ControlBox")]
		public Image JMaximizeNormalDown
		{
			get => this.XControlboxMinimizeCloseDown;
			set => this.XControlboxMinimizeCloseDown = value;
		}

		[Browsable(true)]
		[Category("J ControlBox")]
		public Image JMinimizeOver
		{
			get => this.XControlboxImageHideHover;
			set => this.XControlboxImageHideHover = value;
		}

		[Browsable(true)]
		[Category("J ControlBox")]
		public Image JMinimizeNormal
		{
			get => this.XControlboxImageHideNormal;
			set => this.XControlboxImageHideNormal = value;
		}

		[Browsable(true)]
		[Category("J ControlBox")]
		public Image JMinimizeDown
		{
			get => this.XControlboxHideCloseDown;
			set => this.XControlboxHideCloseDown = value;
		}

		[Browsable(true)]
		[Category("J ControlBox")]
		public Image JCloseBtnOver
		{
			get => this.XControlboxImageCloseHover;
			set => this.XControlboxImageCloseHover = value;
		}

		[Browsable(true)]
		[Category("J ControlBox")]
		public Image JCloseBtnNormal
		{
			get => this.XControlboxImageCloseNormal;
			set => this.XControlboxImageCloseNormal = value;
		}

		[Browsable(true)]
		[Category("J ControlBox")]
		public Image JCloseBtnDown
		{
			get => this.XCloseBtnDown;
			set => this.XControlboxImageCloseNormal = value;
		}

		[Browsable(true)]
		[Category("J ControlBox")]
		public JForm.JHUIControlBoxButtons JOrderControlBoxButton3
		{
			get => this.XJOrderLastButton;
			set
			{
				if (this.XJOrderFirstButton == value)
					this.XJOrderFirstButton = this.XJOrderLastButton;
				if (this.XJOrderMiddleButton == value)
					this.XJOrderMiddleButton = this.XJOrderLastButton;
				this.XJOrderLastButton = value;
				this.UpdateWindowButtonPosition();
			}
		}

		[Browsable(true)]
		[Category("J ControlBox")]
		public JForm.JHUIControlBoxButtons JOrderControlBoxButton2
		{
			get => this.XJOrderFirstButton;
			set
			{
				if (this.XJOrderLastButton == value)
					this.XJOrderLastButton = this.XJOrderFirstButton;
				if (this.XJOrderMiddleButton == value)
					this.XJOrderMiddleButton = this.XJOrderFirstButton;
				this.XJOrderFirstButton = value;
				this.UpdateWindowButtonPosition();
			}
		}

		[Browsable(true)]
		[Category("J ControlBox")]
		public JForm.JHUIControlBoxButtons JOrderControlBoxButton1
		{
			get => this.XJOrderMiddleButton;
			set
			{
				if (this.XJOrderFirstButton == value)
					this.XJOrderFirstButton = this.XJOrderMiddleButton;
				if (this.XJOrderLastButton == value)
					this.XJOrderLastButton = this.XJOrderMiddleButton;
				this.XJOrderMiddleButton = value;
				this.UpdateWindowButtonPosition();
			}
		}

		[Browsable(true)]
		[Category("J Appearance")]
		public JFormTextAlign TextAlign
		{
			get => this.textAlign;
			set => this.textAlign = value;
		}

		[Browsable(true)]
		[Category("J Appearance")]
		public bool UseCustomBackColor
		{
			get => this._useCustomBackColor;
			set => this._useCustomBackColor = value;
		}

		public override Color BackColor
		{
			get => !this.UseCustomBackColor ? JPaint.BackColor.Form(this.Theme, (int)byte.MaxValue) : this.MBackColor;
			set => this.MBackColor = value;
		}

		[DefaultValue(JFormBorderStyle.None)]
		[Browsable(true)]
		[Category("J Appearance")]
		public JFormBorderStyle BorderStyle
		{
			get => this.formBorderStyle;
			set => this.formBorderStyle = value;
		}

		[Category("J Appearance")]
		public bool Movable
		{
			get => this.isMovable;
			set => this.isMovable = value;
		}

		public new string Text
		{
			get => this.thisText;
			set
			{
				this.thisText = value;
				base.Text = value;
				this.Refresh();
			}
		}

		public new Padding Padding
		{
			get => base.Padding;
			set
			{
				value.Top = Math.Max(value.Top, this.DisplayHeader ? 60 : 30);
				base.Padding = value;
			}
		}

		protected override Padding DefaultPadding => new Padding(20, this.DisplayHeader ? 60 : 20, 20, 20);

		[Category("J Appearance")]
		[DefaultValue(true)]
		public bool DisplayHeader
		{
			get => this.displayHeader;
			set
			{
				this.displayHeader = value;
				if (value != this.displayHeader)
				{
					Padding padding = base.Padding;
					padding.Top += value ? 30 : -30;
					base.Padding = padding;
				}
				this.UpdateWindowButtonPosition();
			}
		}

		[Category("J Appearance")]
		public bool Resizable
		{
			get => this.isResizable;
			set => this.isResizable = value;
		}

		[Category("J ControlBox")]
		public Size JControlBoxButonSize
		{
			get
			{
				if (this.JControlBoxType == JControlBoxType.DEFAULT)
				{
					this.ctrlsize = new Size(25, 25);
					try
					{
						this.UpdateWindowButtonPosition(true);
						this.UpdateWindowButtonPosition();
						this.Refresh();
					}
					catch
					{
					}
					return this.ctrlsize;
				}
				try
				{
					this.ctrlsize = this.lastSize;
					this.UpdateWindowButtonPosition(true);
					this.UpdateWindowButtonPosition();
					this.Refresh();
				}
				catch
				{
				}
				return this.ctrlsize;
			}
			set
			{
				this.lastSize = value;
				this.ctrlsize = value;
				try
				{
					this.UpdateWindowButtonPosition(true);
					this.UpdateWindowButtonPosition();
					this.Refresh();
				}
				catch
				{
				}
			}
		}

		[Browsable(false)]
		public new bool ControlBox
		{
			get => base.ControlBox;
			set
			{
				base.ControlBox = value;
				this.UpdateWindowButtonPosition(true);
			}
		}

		[Category("J ControlBox")]
		public bool JControlBoxShow
		{
			get => this.thisControlBox;
			set
			{
				base.ControlBox = value;
				this.thisControlBox = value;
				this.UpdateWindowButtonPosition(true);
			}
		}

		[Category("J ControlBox")]
		public JControlBoxLocation JControlBoxAlign
		{
			get => this.XCustomControlButtonsAlign;
			set
			{
				this.XCustomControlButtonsAlign = value;
				this.UpdateWindowButtonPosition();
				this.Refresh();
			}
		}

		[Category("J ControlBox")]
		public JControlBoxType JControlBoxType
		{
			get => this.XJControlBoxType;
			set
			{
				this.XJControlBoxType = value;
				this.UpdateWindowButtonPosition(true);
				this.UpdateWindowButtonPosition();
				this.Refresh();
			}
		}

		[Category("J Appearance")]
		[DefaultValue(JFormShadowType.Flat)]
		public JFormShadowType ShadowType
		{
			get => !this.IsMdiChild ? this.shadowType : JFormShadowType.None;
			set => this.shadowType = value;
		}

		[Browsable(false)]
		public new FormBorderStyle FormBorderStyle
		{
			get => base.FormBorderStyle;
			set => base.FormBorderStyle = value;
		}

		public new Form MdiParent
		{
			get => base.MdiParent;
			set
			{
				if (value != null)
				{
					this.RemoveShadow();
					this.shadowType = JFormShadowType.None;
				}
				base.MdiParent = value;
			}
		}

		[Category("J Appearance")]
		[DefaultValue(5)]
		public int PaddingTop
		{
			get => this.ThisBorderWidth;
			set
			{
				this.ThisBorderWidth = this.ThisBorderHeight = value;
				this.UpdateWindowButtonPosition();
			}
		}

		[Category("J Appearance")]
		[DefaultValue(null)]
		public Image BackImage
		{
			get => this.backImage;
			set
			{
				this.backImage = value;
				if (value != null)
					this._image = this.ApplyInvert(new Bitmap(value));
				this.Refresh();
			}
		}

		[Category("J Appearance")]
		public Padding BackImagePadding
		{
			get => this.backImagePadding;
			set
			{
				this.backImagePadding = value;
				this.Refresh();
			}
		}

		[Category("J Appearance")]
		public int BackMaxSize
		{
			get => this.backMaxSize;
			set
			{
				this.backMaxSize = value;
				this.Refresh();
			}
		}

		[Category("J Appearance")]
		[DefaultValue(BackLocation.TopLeft)]
		public BackLocation BackLocation
		{
			get => this.backLocation;
			set
			{
				this.backLocation = value;
				this.Refresh();
			}
		}

		[Category("J Appearance")]
		[DefaultValue(true)]
		public bool ApplyImageInvert
		{
			get => this._imageinvert;
			set
			{
				this._imageinvert = value;
				this.Refresh();
			}
		}

		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);
			if (!this.DesignMode)
				return;
			LManager.Instance.CheckLicence();
		}

		public JForm()
		{
			this.MBackColor = JPaint.BackColor.Form(this.Theme, (int)byte.MaxValue);
			this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			this.FormBorderStyle = FormBorderStyle.None;
			this.StartPosition = FormStartPosition.CenterScreen;
			this.TransparencyKey = Color.Lavender;
			this.KeyPreview = true;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
				this.RemoveShadow();
			if (this.InvokeRequired)
				this.Invoke((Delegate)(() => base.Dispose(disposing)));
			else
				base.Dispose(disposing);
		}

		public Bitmap ApplyInvert(Bitmap bitmapImage)
		{
			for (int y = 0; y < bitmapImage.Height; ++y)
			{
				for (int x = 0; x < bitmapImage.Width; ++x)
				{
					Color pixel = bitmapImage.GetPixel(x, y);
					int a = (int)pixel.A;
					byte num1 = (byte)((uint)byte.MaxValue - (uint)pixel.R);
					byte num2 = (byte)((uint)byte.MaxValue - (uint)pixel.G);
					byte num3 = (byte)((uint)byte.MaxValue - (uint)pixel.B);
					if (num1 <= (byte)0)
						num1 = (byte)17;
					if (num2 <= (byte)0)
						num2 = (byte)17;
					if (num3 <= (byte)0)
						num3 = (byte)17;
					bitmapImage.SetPixel(x, y, Color.FromArgb((int)num1, (int)num2, (int)num3));
				}
			}
			return bitmapImage;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			Color backColor = this.BackColor;
			Color foreColor = JPaint.ForeColor.Title(this.Theme, (int)byte.MaxValue);
			e.Graphics.Clear(backColor);
			if (this.PaintTopBorder)
			{
				if (this.Style != JColorStyle.Custom)
				{
					using (SolidBrush styleBrush = JPaint.GetStyleBrush(this.Style))
					{
						Rectangle rect = new Rectangle(0, 0, this.Width, this.ThisBorderWidth);
						e.Graphics.FillRectangle((Brush)styleBrush, rect);
					}
				}
				else
				{
					using (SolidBrush solidBrush = new SolidBrush(this.TopBorderColor))
					{
						Rectangle rect = new Rectangle(0, 0, this.Width, this.ThisBorderWidth);
						e.Graphics.FillRectangle((Brush)solidBrush, rect);
					}
				}
			}
			if (this.BorderStyle != JFormBorderStyle.None)
			{
				using (Pen pen = new Pen(JPaint.BorderColor.Form(this.Theme, (int)byte.MaxValue)))
					e.Graphics.DrawLines(pen, new Point[4]
					{
			new Point(0, this.ThisBorderWidth),
			new Point(0, this.Height - 1),
			new Point(this.Width - 1, this.Height - 1),
			new Point(this.Width - 1, this.ThisBorderWidth)
					});
			}
			Rectangle clientRectangle;
			if (this.backImage != null && this.backMaxSize != 0)
			{
				Image image1 = JImage.ResizeImage(this.backImage, new Rectangle(0, 0, this.backMaxSize, this.backMaxSize));
				if (this._imageinvert)
					image1 = JImage.ResizeImage(this.Theme == JThemeStyle.Dark ? (Image)this._image : this.backImage, new Rectangle(0, 0, this.backMaxSize, this.backMaxSize));
				switch (this.backLocation)
				{
					case BackLocation.TopLeft:
						e.Graphics.DrawImage(image1, this.backImagePadding.Left, this.backImagePadding.Top);
						break;
					case BackLocation.TopRight:
						e.Graphics.DrawImage(image1, this.ClientRectangle.Right - (this.backImagePadding.Right + image1.Width), this.backImagePadding.Top);
						break;
					case BackLocation.BottomLeft:
						e.Graphics.DrawImage(image1, this.backImagePadding.Left, this.ClientRectangle.Bottom - (image1.Height + this.backImagePadding.Bottom));
						break;
					case BackLocation.BottomRight:
						Graphics graphics = e.Graphics;
						Image image2 = image1;
						int x = this.ClientRectangle.Right - (this.backImagePadding.Right + image1.Width);
						clientRectangle = this.ClientRectangle;
						int y = clientRectangle.Bottom - (image1.Height + this.backImagePadding.Bottom);
						graphics.DrawImage(image2, x, y);
						break;
				}
			}
			if (this.displayHeader)
			{
				Rectangle bounds;
				ref Rectangle local = ref bounds;
				clientRectangle = this.ClientRectangle;
				int width = clientRectangle.Width - 40;
				local = new Rectangle(20, 20, width, 40);
				TextFormatFlags flags = TextFormatFlags.EndEllipsis | this.GetTextFormatFlags();
				TextRenderer.DrawText((IDeviceContext)e.Graphics, this.Text, JFonts.Title, bounds, foreColor, flags);
			}
			if (!this.Resizable || this.SizeGripStyle != SizeGripStyle.Auto && this.SizeGripStyle != SizeGripStyle.Show)
				return;
			if (this.UseCustomMoveHandle)
			{
				if (this.MoveHandleImage != null)
				{
					Graphics graphics = e.Graphics;
					Image moveHandleImage = this.MoveHandleImage;
					clientRectangle = this.ClientRectangle;
					int x = clientRectangle.Width - this.MoveHandleImage.Width;
					clientRectangle = this.ClientRectangle;
					int y = clientRectangle.Height - this.MoveHandleImage.Height;
					Point point = new Point(x, y);
					graphics.DrawImage(moveHandleImage, point);
					int num = this.UseTemeColorForMoveHand ? 1 : 0;
				}
				else if (this.UseTemeColorForMoveHand)
				{
					using (SolidBrush styleBrush = JPaint.GetStyleBrush(this.Style))
					{
						Size size = new Size(2, 2);
						Graphics graphics = e.Graphics;
						SolidBrush solidBrush = styleBrush;
						Rectangle[] rects = new Rectangle[6];
						clientRectangle = this.ClientRectangle;
						int x1 = clientRectangle.Width - 6;
						clientRectangle = this.ClientRectangle;
						int y1 = clientRectangle.Height - 6;
						rects[0] = new Rectangle(new Point(x1, y1), size);
						clientRectangle = this.ClientRectangle;
						int x2 = clientRectangle.Width - 10;
						clientRectangle = this.ClientRectangle;
						int y2 = clientRectangle.Height - 10;
						rects[1] = new Rectangle(new Point(x2, y2), size);
						clientRectangle = this.ClientRectangle;
						int x3 = clientRectangle.Width - 10;
						clientRectangle = this.ClientRectangle;
						int y3 = clientRectangle.Height - 6;
						rects[2] = new Rectangle(new Point(x3, y3), size);
						clientRectangle = this.ClientRectangle;
						int x4 = clientRectangle.Width - 6;
						clientRectangle = this.ClientRectangle;
						int y4 = clientRectangle.Height - 10;
						rects[3] = new Rectangle(new Point(x4, y4), size);
						clientRectangle = this.ClientRectangle;
						int x5 = clientRectangle.Width - 14;
						clientRectangle = this.ClientRectangle;
						int y5 = clientRectangle.Height - 6;
						rects[4] = new Rectangle(new Point(x5, y5), size);
						clientRectangle = this.ClientRectangle;
						int x6 = clientRectangle.Width - 6;
						clientRectangle = this.ClientRectangle;
						int y6 = clientRectangle.Height - 14;
						rects[5] = new Rectangle(new Point(x6, y6), size);
						graphics.FillRectangles((Brush)solidBrush, rects);
					}
				}
				else
				{
					using (SolidBrush solidBrush1 = new SolidBrush(JPaint.ForeColor.Button.Disabled(this.Theme, (int)byte.MaxValue)))
					{
						Size size = new Size(2, 2);
						Graphics graphics = e.Graphics;
						SolidBrush solidBrush2 = solidBrush1;
						Rectangle[] rects = new Rectangle[6];
						clientRectangle = this.ClientRectangle;
						int x1 = clientRectangle.Width - 6;
						clientRectangle = this.ClientRectangle;
						int y1 = clientRectangle.Height - 6;
						rects[0] = new Rectangle(new Point(x1, y1), size);
						clientRectangle = this.ClientRectangle;
						int x2 = clientRectangle.Width - 10;
						clientRectangle = this.ClientRectangle;
						int y2 = clientRectangle.Height - 10;
						rects[1] = new Rectangle(new Point(x2, y2), size);
						clientRectangle = this.ClientRectangle;
						int x3 = clientRectangle.Width - 10;
						clientRectangle = this.ClientRectangle;
						int y3 = clientRectangle.Height - 6;
						rects[2] = new Rectangle(new Point(x3, y3), size);
						clientRectangle = this.ClientRectangle;
						int x4 = clientRectangle.Width - 6;
						clientRectangle = this.ClientRectangle;
						int y4 = clientRectangle.Height - 10;
						rects[3] = new Rectangle(new Point(x4, y4), size);
						clientRectangle = this.ClientRectangle;
						int x5 = clientRectangle.Width - 14;
						clientRectangle = this.ClientRectangle;
						int y5 = clientRectangle.Height - 6;
						rects[4] = new Rectangle(new Point(x5, y5), size);
						clientRectangle = this.ClientRectangle;
						int x6 = clientRectangle.Width - 6;
						clientRectangle = this.ClientRectangle;
						int y6 = clientRectangle.Height - 14;
						rects[5] = new Rectangle(new Point(x6, y6), size);
						graphics.FillRectangles((Brush)solidBrush2, rects);
					}
				}
			}
			else if (this.UseTemeColorForMoveHand)
			{
				using (SolidBrush styleBrush = JPaint.GetStyleBrush(this.Style))
				{
					Size size = new Size(2, 2);
					Graphics graphics = e.Graphics;
					SolidBrush solidBrush = styleBrush;
					Rectangle[] rects = new Rectangle[6];
					clientRectangle = this.ClientRectangle;
					int x1 = clientRectangle.Width - 6;
					clientRectangle = this.ClientRectangle;
					int y1 = clientRectangle.Height - 6;
					rects[0] = new Rectangle(new Point(x1, y1), size);
					clientRectangle = this.ClientRectangle;
					int x2 = clientRectangle.Width - 10;
					clientRectangle = this.ClientRectangle;
					int y2 = clientRectangle.Height - 10;
					rects[1] = new Rectangle(new Point(x2, y2), size);
					clientRectangle = this.ClientRectangle;
					int x3 = clientRectangle.Width - 10;
					clientRectangle = this.ClientRectangle;
					int y3 = clientRectangle.Height - 6;
					rects[2] = new Rectangle(new Point(x3, y3), size);
					clientRectangle = this.ClientRectangle;
					int x4 = clientRectangle.Width - 6;
					clientRectangle = this.ClientRectangle;
					int y4 = clientRectangle.Height - 10;
					rects[3] = new Rectangle(new Point(x4, y4), size);
					clientRectangle = this.ClientRectangle;
					int x5 = clientRectangle.Width - 14;
					clientRectangle = this.ClientRectangle;
					int y5 = clientRectangle.Height - 6;
					rects[4] = new Rectangle(new Point(x5, y5), size);
					clientRectangle = this.ClientRectangle;
					int x6 = clientRectangle.Width - 6;
					clientRectangle = this.ClientRectangle;
					int y6 = clientRectangle.Height - 14;
					rects[5] = new Rectangle(new Point(x6, y6), size);
					graphics.FillRectangles((Brush)solidBrush, rects);
				}
			}
			else
			{
				using (SolidBrush solidBrush1 = new SolidBrush(JPaint.ForeColor.Button.Disabled(this.Theme, (int)byte.MaxValue)))
				{
					Size size = new Size(2, 2);
					Graphics graphics = e.Graphics;
					SolidBrush solidBrush2 = solidBrush1;
					Rectangle[] rects = new Rectangle[6];
					clientRectangle = this.ClientRectangle;
					int x1 = clientRectangle.Width - 6;
					clientRectangle = this.ClientRectangle;
					int y1 = clientRectangle.Height - 6;
					rects[0] = new Rectangle(new Point(x1, y1), size);
					clientRectangle = this.ClientRectangle;
					int x2 = clientRectangle.Width - 10;
					clientRectangle = this.ClientRectangle;
					int y2 = clientRectangle.Height - 10;
					rects[1] = new Rectangle(new Point(x2, y2), size);
					clientRectangle = this.ClientRectangle;
					int x3 = clientRectangle.Width - 10;
					clientRectangle = this.ClientRectangle;
					int y3 = clientRectangle.Height - 6;
					rects[2] = new Rectangle(new Point(x3, y3), size);
					clientRectangle = this.ClientRectangle;
					int x4 = clientRectangle.Width - 6;
					clientRectangle = this.ClientRectangle;
					int y4 = clientRectangle.Height - 10;
					rects[3] = new Rectangle(new Point(x4, y4), size);
					clientRectangle = this.ClientRectangle;
					int x5 = clientRectangle.Width - 14;
					clientRectangle = this.ClientRectangle;
					int y5 = clientRectangle.Height - 6;
					rects[4] = new Rectangle(new Point(x5, y5), size);
					clientRectangle = this.ClientRectangle;
					int x6 = clientRectangle.Width - 6;
					clientRectangle = this.ClientRectangle;
					int y6 = clientRectangle.Height - 14;
					rects[5] = new Rectangle(new Point(x6, y6), size);
					graphics.FillRectangles((Brush)solidBrush2, rects);
				}
			}
		}

		private TextFormatFlags GetTextFormatFlags()
		{
			switch (this.TextAlign)
			{
				case JFormTextAlign.Left:
					return TextFormatFlags.Default;
				case JFormTextAlign.Center:
					return TextFormatFlags.HorizontalCenter;
				case JFormTextAlign.Right:
					return TextFormatFlags.Right;
				default:
					throw new InvalidOperationException();
			}
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			if (!(this is JTaskWindow))
				JTaskWindow.ForceClose();
			base.OnClosing(e);
		}

		protected override void OnClosed(EventArgs e)
		{
			if (this.Owner != null)
				this.Owner = (Form)null;
			this.RemoveShadow();
			base.OnClosed(e);
		}

		[SecuritySafeCritical]
		public bool FocusMe() => WinApi.SetForegroundWindow(this.Handle);

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (this.DesignMode)
			{
				this.RemoveCloseButton();
				if (this.JControlBoxShow)
				{
					this.AddWindowButton(JForm.JHUIControlBoxButtons.Close);
					if (this.MaximizeBox)
						this.AddWindowButton(JForm.JHUIControlBoxButtons.Maximize);
					if (this.MinimizeBox)
						this.AddWindowButton(JForm.JHUIControlBoxButtons.Minimize);
					this.UpdateWindowButtonPosition();
				}
				this.CreateShadow();
			}
			else
			{
				switch (this.StartPosition)
				{
					case FormStartPosition.CenterScreen:
						if (this.IsMdiChild)
						{
							this.CenterToParent();
							break;
						}
						this.CenterToScreen();
						break;
					case FormStartPosition.CenterParent:
						this.CenterToParent();
						break;
				}
				this.RemoveCloseButton();
				if (this.JControlBoxShow)
				{
					this.AddWindowButton(JForm.JHUIControlBoxButtons.Close);
					if (this.MaximizeBox)
						this.AddWindowButton(JForm.JHUIControlBoxButtons.Maximize);
					if (this.MinimizeBox)
						this.AddWindowButton(JForm.JHUIControlBoxButtons.Minimize);
					this.UpdateWindowButtonPosition();
				}
				this.CreateShadow();
			}
		}

		protected override void OnActivated(EventArgs e)
		{
			base.OnActivated(e);
			if (this.shadowType != JFormShadowType.AeroShadow || !JForm.IsAeroThemeEnabled() || !JForm.IsDropShadowSupported())
				return;
			int attrValue = 2;
			DwmApi.DwmSetWindowAttribute(this.Handle, 2, ref attrValue, 4);
			DwmApi.MARGINS marInset = new DwmApi.MARGINS()
			{
				cyBottomHeight = 1,
				cxLeftWidth = 0,
				cxRightWidth = 0,
				cyTopHeight = 0
			};
			DwmApi.DwmExtendFrameIntoClientArea(this.Handle, ref marInset);
		}

		protected override void OnEnabledChanged(EventArgs e)
		{
			base.OnEnabledChanged(e);
			this.Invalidate();
		}

		protected override void OnResizeEnd(EventArgs e)
		{
			base.OnResizeEnd(e);
			this.UpdateWindowButtonPosition();
		}

		protected override void OnResize(EventArgs e)
		{
			this.UpdateWindowButtonPosition();
			base.OnResize(e);
		}

		public event EventHandler OnWindowMaximized = (_param1, _param2) => { };

		public event EventHandler OnWindowMinimized = (_param1, _param2) => { };

		protected override void WndProc(ref Message m)
		{
			if (this.DesignMode)
			{
				base.WndProc(ref m);
			}
			else
			{
				switch (m.Msg)
				{
					case 132:
						WinApi.HitTest hitTest = this.HitTestNCA(m.HWnd, m.WParam, m.LParam);
						if (hitTest != WinApi.HitTest.HTCLIENT)
						{
							m.Result = (IntPtr)(int)hitTest;
							return;
						}
						break;
					case 163:
					case 515:
						if (!this.MaximizeBox)
							return;
						break;
					case 274:
						switch (m.WParam.ToInt32() & 65520)
						{
							case 61456:
								if (!this.Movable)
									return;
								break;
							case 61472:
								EventHandler onWindowMinimized = this.OnWindowMinimized;
								if (onWindowMinimized != null)
								{
									onWindowMinimized((object)this, new EventArgs());
									break;
								}
								break;
							case 61488:
								EventHandler onWindowMaximized = this.OnWindowMaximized;
								if (onWindowMaximized != null)
								{
									onWindowMaximized((object)this, new EventArgs());
									break;
								}
								break;
						}
						break;
				}
				base.WndProc(ref m);
				switch (m.Msg)
				{
					case 5:
						if (this.windowButtonList == null)
							break;
						JForm.JFormButton jformButton;
						this.windowButtonList.TryGetValue(JForm.JHUIControlBoxButtons.Maximize, out jformButton);
						if (jformButton == null)
							break;
						if (this.WindowState == FormWindowState.Normal)
						{
							if (this.shadowForm != null)
								this.shadowForm.Visible = true;
							jformButton.ShowMinimazedNormal();
						}
						if (this.WindowState != FormWindowState.Maximized)
							break;
						jformButton.ShowMaximaded();
						break;
					case 36:
						this.OnGetMinMaxInfo(m.HWnd, m.LParam);
						break;
				}
			}
		}

		[SecuritySafeCritical]
		private unsafe void OnGetMinMaxInfo(IntPtr hwnd, IntPtr lParam)
		{
			WinApi.MINMAXINFO* minmaxinfoPtr = (WinApi.MINMAXINFO*)(void*)lParam;
			Screen screen = Screen.FromHandle(hwnd);
			Rectangle rectangle;
			if (this.Parent != null)
			{
				ref WinApi.POINT local1 = ref minmaxinfoPtr->ptMaxSize;
				Size size = this.Parent.ClientRectangle.Size;
				int width = size.Width;
				local1.x = width;
				ref WinApi.POINT local2 = ref minmaxinfoPtr->ptMaxSize;
				size = this.Parent.ClientRectangle.Size;
				int height = size.Height;
				local2.y = height;
			}
			else
			{
				ref WinApi.POINT local1 = ref minmaxinfoPtr->ptMaxSize;
				rectangle = screen.WorkingArea;
				int width = rectangle.Width;
				local1.x = width;
				ref WinApi.POINT local2 = ref minmaxinfoPtr->ptMaxSize;
				rectangle = screen.WorkingArea;
				int height = rectangle.Height;
				local2.y = height;
			}
			ref WinApi.POINT local3 = ref minmaxinfoPtr->ptMaxPosition;
			rectangle = screen.WorkingArea;
			int left1 = rectangle.Left;
			rectangle = screen.Bounds;
			int left2 = rectangle.Left;
			int num1 = Math.Abs(left1 - left2);
			local3.x = num1;
			ref WinApi.POINT local4 = ref minmaxinfoPtr->ptMaxPosition;
			rectangle = screen.WorkingArea;
			int top1 = rectangle.Top;
			rectangle = screen.Bounds;
			int top2 = rectangle.Top;
			int num2 = Math.Abs(top1 - top2);
			local4.y = num2;
		}

		private WinApi.HitTest HitTestNCA(IntPtr hwnd, IntPtr wparam, IntPtr lparam)
		{
			Point pt = new Point((int)(short)(int)lparam, (int)(short)((int)lparam >> 16));
			Padding padding = this.Padding;
			int right = padding.Right;
			padding = this.Padding;
			int bottom = padding.Bottom;
			int num = Math.Max(right, bottom);
			if (this.Resizable && this.RectangleToScreen(new Rectangle(this.ClientRectangle.Width - num, this.ClientRectangle.Height - num, num, num)).Contains(pt))
				return WinApi.HitTest.HTBOTTOMRIGHT;
			return this.PaintTopBorder && this.RectangleToScreen(new Rectangle(this.ThisBorderWidth, this.ThisBorderWidth, this.ClientRectangle.Width - 2 * this.ThisBorderWidth, 50)).Contains(pt) ? WinApi.HitTest.HTCAPTION : WinApi.HitTest.HTCLIENT;
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (e.Button != MouseButtons.Left || !this.Movable || this.WindowState == FormWindowState.Maximized)
				return;
			if (this.PaintTopBorder)
			{
				if (this.Width - this.ThisBorderWidth <= e.Location.X || e.Location.X <= this.ThisBorderWidth || e.Location.Y <= this.ThisBorderWidth)
					return;
				this.MoveControl();
			}
			else
				this.MoveControl();
		}

		[SecuritySafeCritical]
		private void MoveControl()
		{
			WinApi.ReleaseCapture();
			WinApi.SendMessage(this.Handle, 161, 2, 0);
		}

		[SecuritySafeCritical]
		private static bool IsAeroThemeEnabled()
		{
			if (Environment.OSVersion.Version.Major <= 5)
				return false;
			bool pfEnabled;
			DwmApi.DwmIsCompositionEnabled(out pfEnabled);
			return pfEnabled;
		}

		private static bool IsDropShadowSupported() => Environment.OSVersion.Version.Major > 5 && SystemInformation.IsDropShadowEnabled;

		private void AddWindowButton(JForm.JHUIControlBoxButtons button)
		{
			if (this.windowButtonList == null)
				this.windowButtonList = new Dictionary<JForm.JHUIControlBoxButtons, JForm.JFormButton>();
			if (this.windowButtonList.ContainsKey(button))
				return;
			JForm.JFormButton jformButton = (JForm.JFormButton)null;
			if (this.DesignMode)
				this.WindowState = FormWindowState.Normal;
			switch (button)
			{
				case JForm.JHUIControlBoxButtons.Minimize:
					jformButton = new JForm.JFormButton(new ButtonImage(this.JMinimizeOver, this.JMinimizeNormal, this.JMinimizeDown), new ButtonImage((Image)null, (Image)null, (Image)null), this.JControlBoxType, button, this.WindowState);
					break;
				case JForm.JHUIControlBoxButtons.Maximize:
					jformButton = new JForm.JFormButton(new ButtonImage(this.JMaximizeNormalOver, this.JMaximizeNormalNormal, this.JMaximizeNormalDown), new ButtonImage(this.JMaximizeMaximizedOver, this.JMaximizeMaximizedNormal, this.JMaximizeMaximizedDown), this.JControlBoxType, button, this.WindowState);
					break;
				case JForm.JHUIControlBoxButtons.Close:
					jformButton = new JForm.JFormButton(new ButtonImage(this.JCloseBtnOver, this.JCloseBtnNormal, this.JCloseBtnDown), new ButtonImage((Image)null, (Image)null, (Image)null), this.JControlBoxType, button, this.WindowState);
					break;
			}
			jformButton.Style = this.Style;
			jformButton.Theme = this.Theme;
			jformButton.Tag = (object)button;
			switch (this.JControlBoxType)
			{
				case JControlBoxType.DEFAULT:
					jformButton.Size = this.ctrlsize;
					break;
				case JControlBoxType.CUSTOMIZABLE:
					jformButton.Size = this.ctrlsize;
					break;
			}
			if (this.JControlBoxAlign == JControlBoxLocation.LEFT)
				jformButton.Anchor = AnchorStyles.Top | AnchorStyles.Left;
			else if (this.JControlBoxAlign == JControlBoxLocation.RIGHT)
				jformButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			jformButton.TabStop = false;
			jformButton.Click += new EventHandler(this.WindowButton_Click);
			this.Controls.Add((Control)jformButton);
			jformButton.Refresh();
			jformButton.Invalidate();
			this.windowButtonList.Add(button, jformButton);
		}

		private void WindowButton_Click(object sender, EventArgs e)
		{
			if (!(sender is JForm.JFormButton jformButton))
				return;
			switch ((JForm.JHUIControlBoxButtons)jformButton.Tag)
			{
				case JForm.JHUIControlBoxButtons.Minimize:
					this.WindowState = FormWindowState.Minimized;
					EventHandler onWindowMinimized = this.OnWindowMinimized;
					if (onWindowMinimized == null)
						break;
					onWindowMinimized((object)this, new EventArgs());
					break;
				case JForm.JHUIControlBoxButtons.Maximize:
					if (this.WindowState == FormWindowState.Normal)
					{
						this.WindowState = FormWindowState.Maximized;
						jformButton.ShowMaximaded();
						EventHandler onWindowMaximized = this.OnWindowMaximized;
						if (onWindowMaximized == null)
							break;
						onWindowMaximized((object)this, new EventArgs());
						break;
					}
					this.WindowState = FormWindowState.Normal;
					jformButton.ShowMinimazedNormal();
					break;
				case JForm.JHUIControlBoxButtons.Close:
					this.Close();
					break;
			}
		}

		private void UpdateWindowButtonPosition(bool isRefresh)
		{
			if (this.windowButtonList == null)
				return;
			if (!this.JControlBoxShow | isRefresh)
			{
				foreach (Control control in this.windowButtonList.Values)
					this.Controls.Remove(control);
				this.windowButtonList.Clear();
			}
			else
			{
				this.RemoveCloseButton();
				if (this.JControlBoxShow && this.windowButtonList.Count == 0)
				{
					this.AddWindowButton(JForm.JHUIControlBoxButtons.Close);
					if (this.MaximizeBox)
						this.AddWindowButton(JForm.JHUIControlBoxButtons.Maximize);
					if (this.MinimizeBox)
						this.AddWindowButton(JForm.JHUIControlBoxButtons.Minimize);
				}
				Dictionary<int, JForm.JHUIControlBoxButtons> dictionary = new Dictionary<int, JForm.JHUIControlBoxButtons>(3)
		{
		  {
			0,
			this.JOrderControlBoxButton3
		  },
		  {
			1,
			this.JOrderControlBoxButton2
		  },
		  {
			2,
			this.JOrderControlBoxButton1
		  }
		};
				if (this.JControlBoxAlign == JControlBoxLocation.LEFT)
					dictionary = new Dictionary<int, JForm.JHUIControlBoxButtons>(3)
		  {
			{
			  0,
			  this.JOrderControlBoxButton1
			},
			{
			  1,
			  this.JOrderControlBoxButton2
			},
			{
			  2,
			  this.JOrderControlBoxButton3
			}
		  };
				Point point = Point.Empty;
				int thisBorderWidth = this.ThisBorderWidth;
				if (this.DesignMode)
				{
					point = new Point(this.Size.Width - thisBorderWidth - this.ctrlsize.Width, this.ThisBorderHeight);
					switch (this.JControlBoxAlign)
					{
						case JControlBoxLocation.LEFT:
							point = new Point(thisBorderWidth, this.ThisBorderHeight);
							break;
						case JControlBoxLocation.RIGHT:
							point = new Point(this.Size.Width - thisBorderWidth - this.ctrlsize.Width, this.ThisBorderHeight);
							break;
					}
				}
				else
				{
					point = new Point(this.ClientRectangle.Width - thisBorderWidth - this.ctrlsize.Width, this.ThisBorderHeight);
					switch (this.JControlBoxAlign)
					{
						case JControlBoxLocation.LEFT:
							point = new Point(thisBorderWidth, this.ThisBorderHeight);
							break;
						case JControlBoxLocation.RIGHT:
							point = new Point(this.ClientRectangle.Width - thisBorderWidth - this.ctrlsize.Width, this.ThisBorderHeight);
							break;
					}
				}
				if (this.JControlBoxAlign == JControlBoxLocation.RIGHT)
				{
					int x = point.X - this.ctrlsize.Width;
					JForm.JFormButton jformButton = (JForm.JFormButton)null;
					if (this.windowButtonList.Count == 1)
					{
						foreach (KeyValuePair<JForm.JHUIControlBoxButtons, JForm.JFormButton> windowButton in this.windowButtonList)
							windowButton.Value.Location = point;
					}
					else
					{
						foreach (KeyValuePair<int, JForm.JHUIControlBoxButtons> keyValuePair in dictionary)
						{
							bool flag = this.windowButtonList.ContainsKey(keyValuePair.Value);
							if (jformButton == null & flag)
							{
								jformButton = this.windowButtonList[keyValuePair.Value];
								jformButton.Location = point;
							}
							else if (jformButton != null && flag)
							{
								this.windowButtonList[keyValuePair.Value].Location = new Point(x, thisBorderWidth);
								x -= this.ctrlsize.Width;
							}
						}
					}
				}
				else
				{
					int x = point.X + this.ctrlsize.Width;
					JForm.JFormButton jformButton = (JForm.JFormButton)null;
					if (this.windowButtonList.Count == 1)
					{
						foreach (KeyValuePair<JForm.JHUIControlBoxButtons, JForm.JFormButton> windowButton in this.windowButtonList)
							windowButton.Value.Location = point;
					}
					else
					{
						foreach (KeyValuePair<int, JForm.JHUIControlBoxButtons> keyValuePair in dictionary)
						{
							bool flag = this.windowButtonList.ContainsKey(keyValuePair.Value);
							if (jformButton == null & flag)
							{
								jformButton = this.windowButtonList[keyValuePair.Value];
								jformButton.Location = point;
							}
							else if (jformButton != null && flag)
							{
								this.windowButtonList[keyValuePair.Value].Location = new Point(x, thisBorderWidth);
								x += this.ctrlsize.Width;
							}
						}
					}
				}
				this.Refresh();
			}
		}

		private void UpdateWindowButtonPosition()
		{
			if (this.windowButtonList == null)
				return;
			if (!this.JControlBoxShow)
			{
				foreach (Control control in this.windowButtonList.Values)
					this.Controls.Remove(control);
				this.windowButtonList.Clear();
			}
			else
			{
				this.RemoveCloseButton();
				if (this.JControlBoxShow && this.windowButtonList.Count == 0)
				{
					this.AddWindowButton(JForm.JHUIControlBoxButtons.Close);
					if (this.MaximizeBox)
						this.AddWindowButton(JForm.JHUIControlBoxButtons.Maximize);
					if (this.MinimizeBox)
						this.AddWindowButton(JForm.JHUIControlBoxButtons.Minimize);
				}
				Dictionary<int, JForm.JHUIControlBoxButtons> dictionary = new Dictionary<int, JForm.JHUIControlBoxButtons>(3)
		{
		  {
			0,
			this.JOrderControlBoxButton3
		  },
		  {
			1,
			this.JOrderControlBoxButton2
		  },
		  {
			2,
			this.JOrderControlBoxButton1
		  }
		};
				if (this.JControlBoxAlign == JControlBoxLocation.LEFT)
					dictionary = new Dictionary<int, JForm.JHUIControlBoxButtons>(3)
		  {
			{
			  0,
			  this.JOrderControlBoxButton1
			},
			{
			  1,
			  this.JOrderControlBoxButton2
			},
			{
			  2,
			  this.JOrderControlBoxButton3
			}
		  };
				int thisBorderWidth = this.ThisBorderWidth;
				Point point = Point.Empty;
				if (this.DesignMode)
				{
					point = new Point(this.Size.Width - thisBorderWidth - this.ctrlsize.Width, this.ThisBorderHeight);
					switch (this.JControlBoxAlign)
					{
						case JControlBoxLocation.LEFT:
							point = new Point(thisBorderWidth, this.ThisBorderHeight);
							break;
						case JControlBoxLocation.RIGHT:
							point = new Point(this.Size.Width - thisBorderWidth - this.ctrlsize.Width, this.ThisBorderHeight);
							break;
					}
				}
				else
				{
					point = new Point(this.ClientRectangle.Width - thisBorderWidth - this.ctrlsize.Width, this.ThisBorderHeight);
					switch (this.JControlBoxAlign)
					{
						case JControlBoxLocation.LEFT:
							point = new Point(thisBorderWidth, this.ThisBorderHeight);
							break;
						case JControlBoxLocation.RIGHT:
							point = new Point(this.ClientRectangle.Width - thisBorderWidth - this.ctrlsize.Width, this.ThisBorderHeight);
							break;
					}
				}
				if (this.JControlBoxAlign == JControlBoxLocation.RIGHT)
				{
					int x = point.X - this.ctrlsize.Width;
					JForm.JFormButton jformButton = (JForm.JFormButton)null;
					if (this.windowButtonList.Count == 1)
					{
						foreach (KeyValuePair<JForm.JHUIControlBoxButtons, JForm.JFormButton> windowButton in this.windowButtonList)
							windowButton.Value.Location = point;
					}
					else
					{
						foreach (KeyValuePair<int, JForm.JHUIControlBoxButtons> keyValuePair in dictionary)
						{
							bool flag = this.windowButtonList.ContainsKey(keyValuePair.Value);
							if (jformButton == null & flag)
							{
								jformButton = this.windowButtonList[keyValuePair.Value];
								jformButton.Location = point;
							}
							else if (jformButton != null && flag)
							{
								this.windowButtonList[keyValuePair.Value].Location = new Point(x, thisBorderWidth);
								x -= this.ctrlsize.Width;
							}
						}
					}
				}
				else
				{
					int x = point.X + this.ctrlsize.Width;
					JForm.JFormButton jformButton = (JForm.JFormButton)null;
					if (this.windowButtonList.Count == 1)
					{
						foreach (KeyValuePair<JForm.JHUIControlBoxButtons, JForm.JFormButton> windowButton in this.windowButtonList)
							windowButton.Value.Location = point;
					}
					else
					{
						foreach (KeyValuePair<int, JForm.JHUIControlBoxButtons> keyValuePair in dictionary)
						{
							bool flag = this.windowButtonList.ContainsKey(keyValuePair.Value);
							if (jformButton == null & flag)
							{
								jformButton = this.windowButtonList[keyValuePair.Value];
								jformButton.Location = point;
							}
							else if (jformButton != null && flag)
							{
								this.windowButtonList[keyValuePair.Value].Location = new Point(x, thisBorderWidth);
								x += this.ctrlsize.Width;
							}
						}
					}
				}
				this.Refresh();
			}
		}

		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.Style |= 131072;
				if (this.ShadowType == JFormShadowType.SystemShadow)
					createParams.ClassStyle |= 131072;
				return createParams;
			}
		}

		private void CreateShadow()
		{
			switch (this.ShadowType)
			{
				case JFormShadowType.Flat:
					this.shadowForm = (Form)new JForm.JFlatDropShadow((Form)this);
					break;
				case JFormShadowType.DropShadow:
					this.shadowForm = (Form)new JForm.JRealisticDropShadow((Form)this);
					break;
			}
		}

		private void RemoveShadow()
		{
			if (this.shadowForm == null || this.shadowForm.IsDisposed)
				return;
			this.shadowForm.Visible = false;
			this.Owner = this.shadowForm.Owner;
			this.shadowForm.Owner = (Form)null;
			this.shadowForm.Dispose();
			this.shadowForm = (Form)null;
		}

		[SecuritySafeCritical]
		public void RemoveCloseButton()
		{
			IntPtr systemMenu = WinApi.GetSystemMenu(this.Handle, false);
			if (systemMenu == IntPtr.Zero)
				return;
			int menuItemCount = WinApi.GetMenuItemCount(systemMenu);
			if (menuItemCount <= 0)
				return;
			WinApi.RemoveMenu(systemMenu, (uint)(menuItemCount - 1), 5120U);
			WinApi.RemoveMenu(systemMenu, (uint)(menuItemCount - 2), 5120U);
			WinApi.DrawMenuBar(this.Handle);
		}

		private Rectangle MeasureText(
		  Graphics g,
		  Rectangle clientRectangle,
		  Font font,
		  string text,
		  TextFormatFlags flags)
		{
			Size proposedSize = new Size(int.MaxValue, int.MinValue);
			Size size = TextRenderer.MeasureText((IDeviceContext)g, text, font, proposedSize, flags);
			return new Rectangle(clientRectangle.X, clientRectangle.Y, size.Width, size.Height);
		}

		public enum JHUIControlBoxButtons
		{
			Minimize,
			Maximize,
			Close,
		}

		private class JFormButton : System.Windows.Forms.Button, IJControl
		{
			private int m_Alpha = (int)byte.MaxValue;
			private JColorStyle JStyle = JColorStyle.White;
			private JThemeStyle JTheme = JThemeStyle.Dark;
			private JStyleManager JStyleManager;
			private bool useCustomBackColor;
			private bool useCustomForeColor;
			private bool useStyleColors;
			private bool isHovered;
			private bool isPressed;
			private ButtonImage ButtonImage1;
			private ButtonImage ButtonImage2;
			private ButtonImage usedButton;
			public Image usedImage = (Image)Resources.XMinimize_Press;
			public JControlBoxType jControlBoxType;
			public JForm.JHUIControlBoxButtons button = JForm.JHUIControlBoxButtons.Close;
			public FormWindowState WindowState;

			[Category("J Appearance")]
			[DefaultValue(255)]
			public int Alpha
			{
				get => this.m_Alpha;
				set => this.m_Alpha = value;
			}

			[Category("J Appearance")]
			public event EventHandler<JPaintEventArgs> CustomPaintBackground;

			protected virtual void OnCustomPaintBackground(JPaintEventArgs e)
			{
				if (!this.GetStyle(ControlStyles.UserPaint) || this.CustomPaintBackground == null)
					return;
				this.CustomPaintBackground((object)this, e);
			}

			[Category("J Appearance")]
			public event EventHandler<JPaintEventArgs> CustomPaint;

			protected virtual void OnCustomPaint(JPaintEventArgs e)
			{
				if (!this.GetStyle(ControlStyles.UserPaint) || this.CustomPaint == null)
					return;
				this.CustomPaint((object)this, e);
			}

			[Category("J Appearance")]
			public event EventHandler<JPaintEventArgs> CustomPaintForeground;

			protected virtual void OnCustomPaintForeground(JPaintEventArgs e)
			{
				if (!this.GetStyle(ControlStyles.UserPaint) || this.CustomPaintForeground == null)
					return;
				this.CustomPaintForeground((object)this, e);
			}

			[Category("J Appearance")]
			[DefaultValue(JColorStyle.Default)]
			public JColorStyle Style
			{
				get
				{
					if (this.DesignMode || this.JStyle != JColorStyle.Default)
						return this.JStyle;
					if (this.StyleManager != null && this.JStyle == JColorStyle.Default)
						return this.StyleManager.Style;
					return this.StyleManager == null && this.JStyle == JColorStyle.Default ? JColorStyle.White : this.JStyle;
				}
				set => this.JStyle = value;
			}

			[Category("J Appearance")]
			[DefaultValue(JThemeStyle.Default)]
			public JThemeStyle Theme
			{
				get
				{
					if (this.DesignMode || this.JTheme != JThemeStyle.Default)
						return this.JTheme;
					if (this.StyleManager != null && this.JTheme == JThemeStyle.Default)
						return this.StyleManager.Theme;
					return this.StyleManager == null && this.JTheme == JThemeStyle.Default ? JThemeStyle.Dark : this.JTheme;
				}
				set => this.JTheme = value;
			}

			[Browsable(false)]
			[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
			public JStyleManager StyleManager
			{
				get => this.JStyleManager;
				set => this.JStyleManager = value;
			}

			[DefaultValue(false)]
			[Category("J Appearance")]
			public bool UseCustomBackColor
			{
				get => this.useCustomBackColor;
				set => this.useCustomBackColor = value;
			}

			[DefaultValue(false)]
			[Category("J Appearance")]
			public bool UseCustomForeColor
			{
				get => this.useCustomForeColor;
				set => this.useCustomForeColor = value;
			}

			[DefaultValue(false)]
			[Category("J Appearance")]
			public bool UseStyleColors
			{
				get => this.useStyleColors;
				set => this.useStyleColors = value;
			}

			[Browsable(false)]
			[Category("J Behaviour")]
			[DefaultValue(false)]
			public bool UseSelectable
			{
				get => this.GetStyle(ControlStyles.Selectable);
				set => this.SetStyle(ControlStyles.Selectable, value);
			}

			public JFormButton(
			  ButtonImage ButtonImage1,
			  ButtonImage ButtonImage2,
			  JControlBoxType jControlBoxType,
			  JForm.JHUIControlBoxButtons ButtonType,
			  FormWindowState WindowState)
			{
				this.ButtonImage1 = ButtonImage1;
				this.ButtonImage2 = ButtonImage2;
				this.WindowState = WindowState;
				this.button = ButtonType;
				this.jControlBoxType = jControlBoxType;
				switch (this.jControlBoxType)
				{
					case JControlBoxType.DEFAULT:
						this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
						if (this.button == JForm.JHUIControlBoxButtons.Close)
						{
							this.Text = "r";
							break;
						}
						if (this.button == JForm.JHUIControlBoxButtons.Minimize)
						{
							this.Text = "0";
							break;
						}
						if (this.button != JForm.JHUIControlBoxButtons.Maximize)
							break;
						if (WindowState == FormWindowState.Normal)
						{
							this.Text = "1";
							break;
						}
						this.Text = "2";
						break;
					case JControlBoxType.CUSTOMIZABLE:
						this.usedButton = WindowState == FormWindowState.Normal ? ButtonImage1 : ButtonImage2;
						this.usedImage = this.usedButton.Normal;
						this.BackgroundImageLayout = ImageLayout.Stretch;
						this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
						this.Text = "";
						this.UseCustomBackColor = true;
						this.FlatAppearance.BorderSize = 0;
						this.OnMouseUp(new MouseEventArgs(MouseButtons.None, 0, 0, 0, 0));
						break;
				}
			}

			protected override void OnPaint(PaintEventArgs e)
			{
				if (this.jControlBoxType != JControlBoxType.DEFAULT)
				{
					Color backColor = this.Parent.BackColor;
					e.Graphics.Clear(backColor);
					e.Graphics.DrawImage(this.usedImage, this.ClientRectangle, 0, 0, this.usedImage.Width, this.usedImage.Height, GraphicsUnit.Pixel);
				}
				else
				{
					JThemeStyle theme = this.Theme;
					Color color = this.Parent == null ? JPaint.BackColor.Form(theme, this.Alpha) : this.Parent.BackColor;
					Color foreColor;
					if (this.isHovered && !this.isPressed && this.Enabled)
					{
						foreColor = JPaint.ForeColor.Button.Normal(theme, this.Alpha);
						color = JPaint.BackColor.Button.Normal(theme, this.Alpha);
					}
					else if (this.isHovered && this.isPressed && this.Enabled)
					{
						foreColor = JPaint.ForeColor.Button.Press(theme, this.Alpha);
						color = JPaint.GetStyleColor(this.Style, this.Alpha);
					}
					else if (!this.Enabled)
					{
						foreColor = JPaint.ForeColor.Button.Disabled(theme, this.Alpha);
						color = JPaint.BackColor.Button.Disabled(theme, this.Alpha);
					}
					else
						foreColor = JPaint.ForeColor.Button.Normal(theme, this.Alpha);
					e.Graphics.Clear(color);
					Font font = new Font("Webdings", 9.25f);
					TextRenderer.DrawText((IDeviceContext)e.Graphics, this.Text, font, this.ClientRectangle, foreColor, color, TextFormatFlags.EndEllipsis | TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
				}
			}

			protected override void OnMouseEnter(EventArgs e)
			{
				this.isHovered = true;
				if (this.jControlBoxType == JControlBoxType.CUSTOMIZABLE)
				{
					this.usedImage = this.usedButton.Hover;
					this.Refresh();
				}
				else
					this.Invalidate();
				base.OnMouseEnter(e);
			}

			protected override void OnMouseDown(MouseEventArgs e)
			{
				if (e.Button == MouseButtons.Left)
				{
					if (this.jControlBoxType == JControlBoxType.CUSTOMIZABLE)
						this.usedImage = this.usedButton.Press;
					this.isPressed = true;
				}
				else
					this.Invalidate();
				base.OnMouseDown(e);
			}

			protected override void OnMouseUp(MouseEventArgs e)
			{
				this.isPressed = false;
				if (this.jControlBoxType == JControlBoxType.CUSTOMIZABLE)
					this.usedImage = !this.isHovered ? this.usedButton.Normal : this.usedButton.Hover;
				else
					this.Invalidate();
				base.OnMouseUp(e);
			}

			protected override void OnMouseLeave(EventArgs e)
			{
				this.isHovered = false;
				this.isHovered = false;
				if (this.jControlBoxType == JControlBoxType.CUSTOMIZABLE)
					this.usedImage = this.usedButton.Normal;
				else
					this.Invalidate();
				base.OnMouseLeave(e);
			}

			public void ShowMaximaded()
			{
				if (this.jControlBoxType == JControlBoxType.DEFAULT)
				{
					this.WindowState = FormWindowState.Maximized;
					this.Text = "2";
				}
				if (this.jControlBoxType != JControlBoxType.CUSTOMIZABLE)
					return;
				this.usedButton = this.ButtonImage2;
			}

			public void ShowMinimazedNormal()
			{
				if (this.jControlBoxType == JControlBoxType.DEFAULT)
				{
					this.WindowState = FormWindowState.Normal;
					this.Text = "1";
				}
				if (this.jControlBoxType != JControlBoxType.CUSTOMIZABLE)
					return;
				this.usedButton = this.ButtonImage1;
			}
		}

		protected abstract class JShadowBase : Form
		{
			private readonly int shadowSize;
			private readonly int wsExStyle;
			private bool isBringingToFront;
			private long lastResizedOn;
			protected const int WS_EX_TRANSPARENT = 32;
			protected const int WS_EX_LAYERED = 524288;
			protected const int WS_EX_NOACTIVATE = 134217728;
			private const int TICKS_PER_MS = 10000;
			private const long RESIZE_REDRAW_INTERVAL = 10000000;

			protected Form TargetForm { get; private set; }

			protected JShadowBase(Form targetForm, int shadowSize, int wsExStyle)
			{
				this.TargetForm = targetForm;
				this.shadowSize = shadowSize;
				this.wsExStyle = wsExStyle;
				this.TargetForm.Activated += new EventHandler(this.OnTargetFormActivated);
				this.TargetForm.ResizeBegin += new EventHandler(this.OnTargetFormResizeBegin);
				this.TargetForm.ResizeEnd += new EventHandler(this.OnTargetFormResizeEnd);
				this.TargetForm.VisibleChanged += new EventHandler(this.OnTargetFormVisibleChanged);
				this.TargetForm.SizeChanged += new EventHandler(this.OnTargetFormSizeChanged);
				this.TargetForm.Move += new EventHandler(this.OnTargetFormMove);
				this.TargetForm.Resize += new EventHandler(this.OnTargetFormResize);
				if (this.TargetForm.Owner != null)
					this.Owner = this.TargetForm.Owner;
				this.TargetForm.Owner = (Form)this;
				this.MaximizeBox = false;
				this.MinimizeBox = false;
				this.ShowInTaskbar = false;
				this.ShowIcon = false;
				this.FormBorderStyle = FormBorderStyle.None;
				this.Bounds = this.GetShadowBounds();
			}

			protected override CreateParams CreateParams
			{
				get
				{
					CreateParams createParams = base.CreateParams;
					createParams.ExStyle |= this.wsExStyle;
					return createParams;
				}
			}

			private Rectangle GetShadowBounds()
			{
				Rectangle bounds = this.TargetForm.Bounds;
				bounds.Inflate(this.shadowSize, this.shadowSize);
				return bounds;
			}

			protected abstract void PaintShadow();

			protected abstract void ClearShadow();

			protected override void OnDeactivate(EventArgs e)
			{
				base.OnDeactivate(e);
				this.isBringingToFront = true;
			}

			private void OnTargetFormActivated(object sender, EventArgs e)
			{
				if (this.Visible)
					this.Update();
				if (this.isBringingToFront)
				{
					this.Visible = true;
					this.isBringingToFront = false;
				}
				else
					this.BringToFront();
			}

			private void OnTargetFormVisibleChanged(object sender, EventArgs e)
			{
				this.Visible = this.TargetForm.Visible && this.TargetForm.WindowState != FormWindowState.Minimized;
				this.Update();
			}

			private bool IsResizing => this.lastResizedOn > 0L;

			private void OnTargetFormResizeBegin(object sender, EventArgs e) => this.lastResizedOn = DateTime.Now.Ticks;

			private void OnTargetFormMove(object sender, EventArgs e)
			{
				if (!this.TargetForm.Visible || this.TargetForm.WindowState != FormWindowState.Normal)
					this.Visible = false;
				else
					this.Bounds = this.GetShadowBounds();
			}

			private void OnTargetFormResize(object sender, EventArgs e) => this.ClearShadow();

			private void OnTargetFormSizeChanged(object sender, EventArgs e)
			{
				this.Bounds = this.GetShadowBounds();
				if (this.IsResizing)
					return;
				this.PaintShadowIfVisible();
			}

			private void OnTargetFormResizeEnd(object sender, EventArgs e)
			{
				this.lastResizedOn = 0L;
				this.PaintShadowIfVisible();
			}

			private void PaintShadowIfVisible()
			{
				if (!this.TargetForm.Visible || this.TargetForm.WindowState == FormWindowState.Minimized)
					return;
				this.PaintShadow();
			}
		}

		protected class JAeroDropShadow : JForm.JShadowBase
		{
			public JAeroDropShadow(Form targetForm)
			  : base(targetForm, 0, 134217760)
			  => this.FormBorderStyle = FormBorderStyle.SizableToolWindow;

			protected override void SetBoundsCore(
			  int x,
			  int y,
			  int width,
			  int height,
			  BoundsSpecified specified)
			{
				if (specified == BoundsSpecified.Size)
					return;
				base.SetBoundsCore(x, y, width, height, specified);
			}

			protected override void PaintShadow() => this.Visible = true;

			protected override void ClearShadow()
			{
			}
		}

		protected class JFlatDropShadow : JForm.JShadowBase
		{
			private Point Offset = new Point(-6, -6);

			public JFlatDropShadow(Form targetForm)
			  : base(targetForm, 6, 134742048)
			{
			}

			protected override void OnLoad(EventArgs e)
			{
				base.OnLoad(e);
				this.PaintShadow();
			}

			protected override void OnPaint(PaintEventArgs e)
			{
				this.Visible = true;
				this.PaintShadow();
			}

			protected override void PaintShadow()
			{
				using (Bitmap bitmap = this.DrawBlurBorder())
					this.SetBitmap(bitmap, byte.MaxValue);
			}

			protected override void ClearShadow()
			{
				Bitmap bitmap = new Bitmap(this.Width, this.Height, PixelFormat.Format32bppArgb);
				Graphics graphics = Graphics.FromImage((Image)bitmap);
				graphics.Clear(Color.Transparent);
				graphics.Flush();
				graphics.Dispose();
				this.SetBitmap(bitmap, byte.MaxValue);
				bitmap.Dispose();
			}

			[SecuritySafeCritical]
			private void SetBitmap(Bitmap bitmap, byte opacity)
			{
				if (bitmap.PixelFormat != PixelFormat.Format32bppArgb)
					throw new ApplicationException("The bitmap must be 32ppp with alpha-channel.");
				IntPtr dc = WinApi.GetDC(IntPtr.Zero);
				IntPtr compatibleDc = WinApi.CreateCompatibleDC(dc);
				IntPtr hObject1 = IntPtr.Zero;
				IntPtr hObject2 = IntPtr.Zero;
				try
				{
					hObject1 = bitmap.GetHbitmap(Color.FromArgb(0));
					hObject2 = WinApi.SelectObject(compatibleDc, hObject1);
					WinApi.SIZE psize = new WinApi.SIZE(bitmap.Width, bitmap.Height);
					WinApi.POINT pprSrc = new WinApi.POINT(0, 0);
					WinApi.POINT pptDst = new WinApi.POINT(this.Left, this.Top);
					int num = (int)WinApi.UpdateLayeredWindow(this.Handle, dc, ref pptDst, ref psize, compatibleDc, ref pprSrc, 0, ref new WinApi.BLENDFUNCTION()
					{
						BlendOp = (byte)0,
						BlendFlags = (byte)0,
						SourceConstantAlpha = opacity,
						AlphaFormat = (byte)1
					}, 2);
				}
				finally
				{
					WinApi.ReleaseDC(IntPtr.Zero, dc);
					if (hObject1 != IntPtr.Zero)
					{
						WinApi.SelectObject(compatibleDc, hObject2);
						int num = (int)WinApi.DeleteObject(hObject1);
					}
					int num1 = (int)WinApi.DeleteDC(compatibleDc);
				}
			}

			private Bitmap DrawBlurBorder()
			{
				Color black = Color.Black;
				Rectangle clientRectangle = this.ClientRectangle;
				int width = clientRectangle.Width;
				clientRectangle = this.ClientRectangle;
				int height = clientRectangle.Height;
				Rectangle shadowCanvasArea = new Rectangle(0, 0, width, height);
				return (Bitmap)this.DrawOutsetShadow(black, shadowCanvasArea);
			}

			private Image DrawOutsetShadow(Color color, Rectangle shadowCanvasArea)
			{
				Rectangle rect1 = shadowCanvasArea;
				Rectangle rect2 = new Rectangle(shadowCanvasArea.X + (-this.Offset.X - 1), shadowCanvasArea.Y + (-this.Offset.Y - 1), shadowCanvasArea.Width - (-this.Offset.X * 2 - 1), shadowCanvasArea.Height - (-this.Offset.Y * 2 - 1));
				Bitmap bitmap = new Bitmap(rect1.Width, rect1.Height, PixelFormat.Format32bppArgb);
				Graphics graphics = Graphics.FromImage((Image)bitmap);
				graphics.SmoothingMode = SmoothingMode.AntiAlias;
				graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				using (Brush brush = (Brush)new SolidBrush(Color.FromArgb(30, Color.Black)))
					graphics.FillRectangle(brush, rect1);
				using (Brush brush = (Brush)new SolidBrush(Color.FromArgb(60, Color.Black)))
					graphics.FillRectangle(brush, rect2);
				graphics.Flush();
				graphics.Dispose();
				return (Image)bitmap;
			}
		}

		protected class JRealisticDropShadow : JForm.JShadowBase
		{
			public JRealisticDropShadow(Form targetForm)
			  : base(targetForm, 15, 134742048)
			{
			}

			protected override void OnLoad(EventArgs e)
			{
				base.OnLoad(e);
				this.PaintShadow();
			}

			protected override void OnPaint(PaintEventArgs e)
			{
				this.Visible = true;
				this.PaintShadow();
			}

			protected override void PaintShadow()
			{
				using (Bitmap bitmap = this.DrawBlurBorder())
					this.SetBitmap(bitmap, byte.MaxValue);
			}

			protected override void ClearShadow()
			{
				Bitmap bitmap = new Bitmap(this.Width, this.Height, PixelFormat.Format32bppArgb);
				Graphics graphics = Graphics.FromImage((Image)bitmap);
				graphics.Clear(Color.Transparent);
				graphics.Flush();
				graphics.Dispose();
				this.SetBitmap(bitmap, byte.MaxValue);
				bitmap.Dispose();
			}

			[SecuritySafeCritical]
			private void SetBitmap(Bitmap bitmap, byte opacity)
			{
				if (bitmap.PixelFormat != PixelFormat.Format32bppArgb)
					throw new ApplicationException("The bitmap must be 32ppp with alpha-channel.");
				IntPtr dc = WinApi.GetDC(IntPtr.Zero);
				IntPtr compatibleDc = WinApi.CreateCompatibleDC(dc);
				IntPtr hObject1 = IntPtr.Zero;
				IntPtr hObject2 = IntPtr.Zero;
				try
				{
					hObject1 = bitmap.GetHbitmap(Color.FromArgb(0));
					hObject2 = WinApi.SelectObject(compatibleDc, hObject1);
					WinApi.SIZE psize = new WinApi.SIZE(bitmap.Width, bitmap.Height);
					WinApi.POINT pprSrc = new WinApi.POINT(0, 0);
					WinApi.POINT pptDst = new WinApi.POINT(this.Left, this.Top);
					WinApi.BLENDFUNCTION pblend = new WinApi.BLENDFUNCTION()
					{
						BlendOp = 0,
						BlendFlags = 0,
						SourceConstantAlpha = opacity,
						AlphaFormat = 1
					};
					int num = (int)WinApi.UpdateLayeredWindow(this.Handle, dc, ref pptDst, ref psize, compatibleDc, ref pprSrc, 0, ref pblend, 2);
				}
				finally
				{
					WinApi.ReleaseDC(IntPtr.Zero, dc);
					if (hObject1 != IntPtr.Zero)
					{
						WinApi.SelectObject(compatibleDc, hObject2);
						int num = (int)WinApi.DeleteObject(hObject1);
					}
					int num1 = (int)WinApi.DeleteDC(compatibleDc);
				}
			}

			private Bitmap DrawBlurBorder()
			{
				Color black = Color.Black;
				Rectangle clientRectangle = this.ClientRectangle;
				int width = clientRectangle.Width;
				clientRectangle = this.ClientRectangle;
				int height = clientRectangle.Height;
				Rectangle shadowCanvasArea = new Rectangle(1, 1, width, height);
				return (Bitmap)this.DrawOutsetShadow(0, 0, 40, 1, black, shadowCanvasArea);
			}

			private Image DrawOutsetShadow(
			  int hShadow,
			  int vShadow,
			  int blur,
			  int spread,
			  Color color,
			  Rectangle shadowCanvasArea)
			{
				Rectangle rectangle1 = shadowCanvasArea;
				Rectangle rect = shadowCanvasArea;
				rect.Offset(hShadow, vShadow);
				rect.Inflate(-blur, -blur);
				rectangle1.Inflate(spread, spread);
				rectangle1.Offset(hShadow, vShadow);
				Rectangle rectangle2 = rectangle1;
				Bitmap bitmap = new Bitmap(rectangle2.Width, rectangle2.Height, PixelFormat.Format32bppArgb);
				Graphics g = Graphics.FromImage((Image)bitmap);
				g.SmoothingMode = SmoothingMode.AntiAlias;
				g.InterpolationMode = InterpolationMode.HighQualityBicubic;
				int cornerRadius = 0;
				do
				{
					double num = (double)(rectangle1.Height - rect.Height) / (double)(blur * 2 + spread * 2);
					Color fillColor = Color.FromArgb((int)(200.0 * (num * num)), color);
					Rectangle bounds = rect;
					bounds.Offset(-rectangle2.Left, -rectangle2.Top);
					this.DrawRoundedRectangle(g, bounds, cornerRadius, Pens.Transparent, fillColor);
					rect.Inflate(1, 1);
					cornerRadius = (int)((double)blur * (1.0 - num * num));
				}
				while (rectangle1.Contains(rect));
				g.Flush();
				g.Dispose();
				return (Image)bitmap;
			}

			private void DrawRoundedRectangle(
			  Graphics g,
			  Rectangle bounds,
			  int cornerRadius,
			  Pen drawPen,
			  Color fillColor)
			{
				int int32 = Convert.ToInt32(Math.Ceiling((double)drawPen.Width));
				bounds = Rectangle.Inflate(bounds, -int32, -int32);
				GraphicsPath path = new GraphicsPath();
				if (cornerRadius > 0)
				{
					path.AddArc(bounds.X, bounds.Y, cornerRadius, cornerRadius, 180f, 90f);
					path.AddArc(bounds.X + bounds.Width - cornerRadius, bounds.Y, cornerRadius, cornerRadius, 270f, 90f);
					path.AddArc(bounds.X + bounds.Width - cornerRadius, bounds.Y + bounds.Height - cornerRadius, cornerRadius, cornerRadius, 0.0f, 90f);
					path.AddArc(bounds.X, bounds.Y + bounds.Height - cornerRadius, cornerRadius, cornerRadius, 90f, 90f);
				}
				else
					path.AddRectangle(bounds);
				path.CloseAllFigures();
				if (cornerRadius > 5)
				{
					using (SolidBrush solidBrush = new SolidBrush(fillColor))
						g.FillPath((Brush)solidBrush, path);
				}
				if (drawPen == Pens.Transparent)
					return;
				using (Pen pen = new Pen(drawPen.Color))
				{
					pen.EndCap = pen.StartCap = LineCap.Round;
					g.DrawPath(pen, path);
				}
			}
		}
	}
}
