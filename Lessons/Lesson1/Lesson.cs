namespace twinklebear_dev_sdl2_tutorial.Lessons.Lesson1
{
    using SDL2;
    using System;
    using twinklebear_dev_sdl2_tutorial.Helpers;

    public class Lesson : ILesson
    {
        public void Execute()
        {
            if(SDL.SDL_Init(SDL.SDL_INIT_VIDEO) != 0)
            {
                throw new Exception($"{nameof(SDL.SDL_Init)} Error: {SDL.SDL_GetError()}");
            }

            var window = SDL.SDL_CreateWindow("Hello World", 100, 100, 640, 480, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);
            if(window == IntPtr.Zero)
            {
                var msg = $"{nameof(SDL.SDL_CreateWindow)} Error: {SDL.SDL_GetError()}";

                SDL.SDL_Quit();

                throw new Exception(msg);
            }

            var renderer = SDL.SDL_CreateRenderer(
                window, -1,
                SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);

            if(renderer == IntPtr.Zero)
            {
                var msg = $"{nameof(SDL.SDL_CreateRenderer)} Error: {SDL.SDL_GetError()}";

                SDL.SDL_DestroyWindow(window);
                SDL.SDL_Quit();

                throw new Exception(msg);
            }

            var bmpPath = Resources.GetFilePath("lesson1/hello.bmp");
            var bmp = SDL.SDL_LoadBMP(bmpPath);

            if(bmp == IntPtr.Zero)
            {
                var msg = $"{nameof(SDL.SDL_LoadBMP)} Error: {SDL.SDL_GetError()}";

                SDL.SDL_DestroyRenderer(renderer);
                SDL.SDL_DestroyWindow(window);
                SDL.SDL_Quit();

                throw new Exception(msg);
            }

            var texture = SDL.SDL_CreateTextureFromSurface(renderer, bmp);
            SDL.SDL_FreeSurface(bmp);

            if(texture == null)
            {
                var msg = $"{nameof(SDL.SDL_CreateTextureFromSurface)} Error: {SDL.SDL_GetError()}";

                SDL.SDL_DestroyRenderer(renderer);
                SDL.SDL_DestroyWindow(window);
                SDL.SDL_Quit();

                throw new Exception(msg);
            }

            for(var i = 0; i < 3; i++)
            {
                SDL.SDL_RenderClear(renderer);
                SDL.SDL_RenderCopy(renderer, texture, IntPtr.Zero, IntPtr.Zero);
                SDL.SDL_RenderPresent(renderer);
                SDL.SDL_Delay(1000);
            }

            SDL.SDL_DestroyTexture(texture);
            SDL.SDL_DestroyRenderer(renderer);
            SDL.SDL_DestroyWindow(window);
            SDL.SDL_Quit();
        }
    }
}