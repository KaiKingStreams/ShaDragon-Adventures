﻿using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Paril.OpenGL
{
	// Renderer-independent texture class
	public abstract class Texture : IDisposable
	{
		public abstract int Width { get; }
		public abstract int Height { get; }

		#region IDisposable Members

		public abstract void Dispose();

		#endregion

		public void Load(string fileName)
		{
			// re-write the file if it's indexed
			var b = new Bitmap(fileName);

			if (b.PixelFormat == PixelFormat.Format8bppIndexed)
			{
				var newBitmap = new Bitmap(b.Width, b.Height);

				using (Graphics g = Graphics.FromImage(newBitmap))
					g.DrawImage(b, 0, 0, b.Width, b.Height);

				b.Dispose();
				b = newBitmap;
			}

			Load(b);

			b.Dispose();
		}

		public abstract void Load(Bitmap image);

		public abstract void Upload<T>(T[] array, int width, int height) where T : struct;
		public abstract void Upload(IntPtr array, int width, int height);
		public abstract void Get<T>(T[] array) where T : struct;
		public abstract void Get(IntPtr array);

		public abstract void SetMipmapping(bool enable);
		public abstract void SetRepeat(bool enable);
		public abstract void Bind();
	}
}