namespace twinklebear_dev_sdl2_tutorial.Helpers
{
    using SDL2;
    using System;

    public static class SdlDrawing
    {
        public static void RenderTexture(IntPtr texture, IntPtr renderer, int x, int y)
        {
            var dst = new SDL.SDL_Rect();

            dst.x = x;
            dst.y = y;

            uint format;
            int access;

            SDL.SDL_QueryTexture(texture, out format, out access, out dst.w, out dst.h);
            SDL.SDL_RenderCopy(renderer, texture, IntPtr.Zero, ref dst);
        }
    }
}