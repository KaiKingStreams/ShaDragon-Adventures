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

using System.Collections.Generic;
using MCSkin3D;
using OpenTK.Graphics.OpenGL4;

namespace Paril.OpenGL
{
	public abstract class Renderer
	{
		private readonly List<Mesh> _opaqueMeshes = new List<Mesh>();
		private readonly List<Mesh> _transparentMeshes = new List<Mesh>();

		public void Sort()
		{
			if (GlobalSettings.RenderBenchmark && Editor.IsRendering)
				Editor._sortTimer.Start();

			_transparentMeshes.Sort(
				(left, right) =>
				{
					float leftDist = (Editor.CameraPosition - left.Center).Length;
					float rightDist = (Editor.CameraPosition - right.Center).Length;

					return rightDist.CompareTo(leftDist);
				}
				);

			if (GlobalSettings.RenderBenchmark && Editor.IsRendering)
				Editor._sortTimer.Stop();
		}
		
		public void Render()
		{
			Sort();

			if (GlobalSettings.RenderBenchmark && Editor.IsRendering)
				Editor._batchTimer.Start();

			PreRender();

			GL.Enable(EnableCap.CullFace);
			GL.CullFace(CullFaceMode.Front);

			foreach (Mesh mesh in _opaqueMeshes)
				RenderMesh(mesh);

			GL.Enable(EnableCap.Blend);

			foreach (Mesh mesh in _transparentMeshes)
			{
				GL.CullFace(CullFaceMode.Back);
				RenderMesh(mesh);
				GL.CullFace(CullFaceMode.Front);
				RenderMesh(mesh);
			}

			GL.Disable(EnableCap.Blend);
			GL.Disable(EnableCap.CullFace);

			PostRender();

			if (GlobalSettings.RenderBenchmark && Editor.IsRendering)
				Editor._batchTimer.Stop();

			_opaqueMeshes.Clear();
			_transparentMeshes.Clear();
		}

		public void RenderWithoutTransparency()
		{
			GL.Disable(EnableCap.Blend);

			PreRender();
			
			GL.Enable(EnableCap.CullFace);
			GL.CullFace(CullFaceMode.Back);

			foreach (Mesh mesh in _opaqueMeshes)
				RenderMesh(mesh);

			foreach (Mesh mesh in _transparentMeshes)
			{
				GL.CullFace(CullFaceMode.Front);
				RenderMesh(mesh);
				GL.CullFace(CullFaceMode.Back);
				RenderMesh(mesh);
			}

			GL.Disable(EnableCap.CullFace);

			PostRender();

			_opaqueMeshes.Clear();
			_transparentMeshes.Clear();
		}

		public void AddMesh(Mesh mesh)
		{
			if (mesh.HasTransparency || mesh.DrawTransparent)
				_transparentMeshes.Add(mesh);
			else
				_opaqueMeshes.Add(mesh);
		}

		protected virtual void PreRender()
		{
		}

		protected virtual void PostRender()
		{
		}

		public abstract void RenderMesh(Mesh mesh);
		public virtual IMeshUserData CreateUserData(Mesh mesh) { return null; }

		// Some change occured in the mesh which requires an update to its userdata.
		// At the moment, only used to properly set the color array.
		public virtual void UpdateUserData(Mesh mesh) { }
	}
}