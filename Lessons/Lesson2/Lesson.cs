namespace twinklebear_dev_sdl2_tutorial.Lessons.Lesson2
{
    using SDL2;
    using System;
    using twinklebear_dev_sdl2_tutorial.Helpers;

    public class Lesson : ILesson
    {
        const int WIDTH = 640;
        const int HEIGHT = 480;

        const string TITLE = "Lesson 2";

        public void Execute()
        {
            if(SDL.SDL_Init(SDL.SDL_INIT_VIDEO) != 0)
            {
                SdlLogger.Fatal(nameof(SDL.SDL_Init));
            }

            var window = SDL.SDL_CreateWindow(TITLE, 0, 0, WIDTH, HEIGHT, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);

            if(window == IntPtr.Zero)
            {
                SdlLogger.Fatal(nameof(SDL.SDL_CreateWindow),
                () => ReleaseAndQuit(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero));
            }

            var renderer = SDL.SDL_CreateRenderer(
                window, -1,
                SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);

            if(renderer == IntPtr.Zero)
            {
                SdlLogger.Fatal(
                    nameof(SDL.SDL_CreateRenderer),
                    () => ReleaseAndQuit(window, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero));
            }

            var background = Resources.LoadTexture(Resources.GetFilePath(@"lesson2/background.bmp"), renderer);
            var image = Resources.LoadTexture(Resources.GetFilePath(@"lesson2/image.bmp"), renderer);
            
            if(background == IntPtr.Zero || image == IntPtr.Zero)
            {
                SdlLogger.Fatal(
                    nameof(Resources.LoadTexture),
                    () => ReleaseAndQuit(window, renderer, background, image));
            }

            SDL.SDL_RenderClear(renderer);
            
            uint format;
            int x, y, iW, iH, bW, bH, access;

            SDL.SDL_QueryTexture(background, out format, out access, out bW, out bH);
            SdlDrawing.RenderTexture(background, renderer, 0, 0);
            SdlDrawing.RenderTexture(background, renderer, bW, 0);
            SdlDrawing.RenderTexture(background, renderer, 0, bH);
            SdlDrawing.RenderTexture(background, renderer, bW, bH);

            SDL.SDL_QueryTexture(image, out format, out access, out iW, out iH);
            x = WIDTH / 2 - iW / 2;
            y = HEIGHT / 2 - iH / 2;

            SdlDrawing.RenderTexture(image, renderer, x, y);

            SDL.SDL_RenderPresent(renderer);
            SDL.SDL_Delay(5 * 1000);
        }

        private bool ReleaseAndQuit(IntPtr window, IntPtr renderer, IntPtr background, IntPtr image)
        {
            if(window != IntPtr.Zero)
            {
                SDL.SDL_DestroyWindow(window);
            }

            if(renderer != IntPtr.Zero)
            {
                SDL.SDL_DestroyRenderer(renderer);
            }

            if(background != IntPtr.Zero)
            {
                SDL.SDL_DestroyTexture(background);
            }

            if(image != IntPtr.Zero)
            {
                SDL.SDL_DestroyTexture(image);
            }

            SDL.SDL_Quit();

            return false;
        }
    }
}