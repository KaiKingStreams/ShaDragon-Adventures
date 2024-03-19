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
using System.Windows.Forms;
using Devcorp.Controls.Design;
using Paril.OpenGL;

namespace MCSkin3D
{
	public class DarkenLightenTool : BrushToolBase
	{
		public override bool MouseMoveOnSkin(ColorGrabber pixels, Skin skin, int x, int y)
		{
			return MouseMoveOnSkin(pixels, skin, x, y, GlobalSettings.DarkenLightenIncremental);
		}

		public override Color BlendColor(Color l, Color r)
		{
			bool ctrlIng = (Control.ModifierKeys & Keys.Shift) != 0;
			bool switchTools = (!Editor.MainForm.DarkenLightenOptions.Inverted && ctrlIng) ||
							   (Editor.MainForm.DarkenLightenOptions.Inverted && !ctrlIng);
			HSL hsl = ColorSpaceHelper.RGBtoHSL(r);
			float mod = l.A / 255.0f;

			if (switchTools)
				hsl.Luminance -= (GlobalSettings.DarkenLightenExposure * mod) / 5.0f;
			else
				hsl.Luminance += (GlobalSettings.DarkenLightenExposure * mod) / 5.0f;

			if (hsl.Luminance < 0)
				hsl.Luminance = 0;
			if (hsl.Luminance > 1)
				hsl.Luminance = 1;

			return Color.FromArgb(r.A, ColorSpaceHelper.HSLtoColor(hsl));
		}

		public override Color GetLeftColor()
		{
			return Color.FromArgb(255, 0, 0, 0);
		}

		public override string GetStatusLabelText()
		{
			return Editor.GetLanguageString("T_DARKENLIGHTEN");
		}
	}
}