﻿//
//    MCSkin3D, a 3d skin management studio for Minecraft
//    Copyright (C) 2013 Altered Softworks & MCSkin3D Team
//
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

using System.Drawing;
using System.Drawing.Drawing2D;
using MB.Controls;
using MCSkin3D.lemon42;

namespace MCSkin3D
{
	public class SaturationSliderRenderer : SliderRenderer
	{
		public SaturationSliderRenderer(ColorSlider slider) :
			base(slider)
		{
		}

		public ColorManager CurrentColor { get; set; }

		public override void Render(Graphics g)
		{
			var colorRect = new Rectangle(0, (Slider.Height / 2) - 3, Slider.Width - 6, 4);

			Color c1 = new ColorManager.HSVColor(CurrentColor.HSV.H, 0, CurrentColor.HSV.V).ToColor();
			Color c2 = new ColorManager.HSVColor(CurrentColor.HSV.H, 100, CurrentColor.HSV.V).ToColor();

			var brush = new LinearGradientBrush(colorRect, c1, c2, LinearGradientMode.Horizontal);

			//Draw color
			g.FillRectangle(brush, colorRect);
			//Draw border
			g.DrawRectangle(Pens.Black, colorRect);

			DrawThumb(g);
		}
	}
}