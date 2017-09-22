﻿using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Application
{
    public class OpenTKApp : GameWindow
    {
        static int screenID;
        static RayTracer tracer;
        static bool terminated = false;
        protected override void OnLoad(EventArgs e)
        {
            // called upon app init
            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);
            ClientSize = new Size(1024, 512);
            tracer = new RayTracer();
            tracer.screen = new Surface(Width, Height);
            Sprite.target = tracer.screen;
            screenID = tracer.screen.GenTexture();
            tracer.Init();
            tracer.Render();
        }
        protected override void OnUnload(EventArgs e)
        {
            // called upon app close
            GL.DeleteTextures(1, ref screenID);
            Environment.Exit(0); // bypass wait for key on CTRL-F5
        }
        protected override void OnResize(EventArgs e)
        {
            // called upon window resize
            GL.Viewport(0, 0, Width, Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0);
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            // called once per frame; app logic
            var keyboard = OpenTK.Input.Keyboard.GetState();
            if (keyboard[OpenTK.Input.Key.Escape]) this.Exit();

            if (keyboard[Key.Space])
            { tracer.Render(); }

            //when you press a button to move the camera, the render screen will clear and will only start rendering after you press space.
            //this makes moving the camera way smoother.

            if (keyboard[Key.A])
            { tracer.renderCam.transform(-1, 0, 0); tracer.screen.Clear(0); }
            if (keyboard[Key.D])
            { tracer.renderCam.transform(1, 0, 0); tracer.screen.Clear(0); }
            if (keyboard[Key.W])
            { tracer.renderCam.transform(0, 0, 1); tracer.screen.Clear(0); }
            if (keyboard[Key.S])
            { tracer.renderCam.transform(0, 0, -1); tracer.screen.Clear(0); }
            if (keyboard[Key.Right])
            { tracer.renderCam.rotate(0, .1f); tracer.screen.Clear(0); }
            if (keyboard[Key.Left])
            { tracer.renderCam.rotate(0, -.1f); tracer.screen.Clear(0); }
            if (keyboard[Key.Up])
            { tracer.renderCam.rotate(.1f, 0); tracer.screen.Clear(0); }
            if (keyboard[Key.Down])
            { tracer.renderCam.rotate(-.1f, 0); tracer.screen.Clear(0); }
            //an additional if statement to prevent the camera from going underneath the floor plane
            if (keyboard[Key.ControlLeft])
                if (tracer.renderCam.position.Y < 0)
                { tracer.renderCam.transform(0, 1, 0); tracer.screen.Clear(0); }
            if (keyboard[Key.ShiftLeft])
            { tracer.renderCam.transform(0, -1, 0); tracer.screen.Clear(0); }
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.ClearColor(Color.Black);
            GL.Enable(EnableCap.Texture2D);
            GL.Disable(EnableCap.DepthTest);
            GL.Color3(1.0f, 1.0f, 1.0f);

            // called once per frame; render
            tracer.DebugOutput();
            if (terminated)
            {
                Exit();
                return;
            }

            // convert Game.screen to OpenGL texture
            GL.BindTexture(TextureTarget.Texture2D, screenID);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba,
                           tracer.screen.width, tracer.screen.height, 0,
                           OpenTK.Graphics.OpenGL.PixelFormat.Bgra,
                           PixelType.UnsignedByte, tracer.screen.pixels
                         );
            // clear window contents
            GL.Clear(ClearBufferMask.ColorBufferBit);
            // setup camera
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            // draw screen filling quad
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0.0f, 1.0f); GL.Vertex2(-1.0f, -1.0f);
            GL.TexCoord2(1.0f, 1.0f); GL.Vertex2(1.0f, -1.0f);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex2(1.0f, 1.0f);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(-1.0f, 1.0f);
            GL.End();
            // tell OpenTK we're done rendering

            GL.Enable(EnableCap.DepthTest);
            GL.Disable(EnableCap.Texture2D);
            GL.Clear(ClearBufferMask.DepthBufferBit);

            SwapBuffers();
        }
        public static void Main(string[] args)
        {
            // entry point
            using (OpenTKApp app = new OpenTKApp()) { app.Run(30.0, 0.0); }
        }
    }
}