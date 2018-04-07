namespace twinklebear_dev_sdl2_tutorial.Lessons.Lesson4
{
    using SDL2;
    using System;
    using twinklebear_dev_sdl2_tutorial.Helpers;

    public class Lesson : ILesson
    {
        const int WIDTH = 640;
        const int HEIGHT = 480;
        const int TILE_SIZE = 40;
        const string TITLE = "Lesson 4";

        public void Execute()
        {
            if(SDL.SDL_Init(SDL.SDL_INIT_VIDEO) != 0)
            {
                SdlLogger.Fatal(nameof(SDL.SDL_Init));
            }

            SDL_image.IMG_Init(SDL_image.IMG_InitFlags.IMG_INIT_PNG);

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

            var background = Resources.LoadTextureFromImage(Resources.GetFilePath(@"lesson3/background.png"), renderer);
            var image = Resources.LoadTextureFromImage(Resources.GetFilePath(@"lesson4/image.png"), renderer);
            
            if(background == IntPtr.Zero || image == IntPtr.Zero)
            {
                SdlLogger.Fatal(
                    nameof(Resources.LoadTextureFromImage),
                    () => ReleaseAndQuit(window, renderer, background, image));
            }


            var quit = false;
            var xTiles = WIDTH / TILE_SIZE;
            var yTiles = HEIGHT / TILE_SIZE;
            uint format;
            int x, y, iW, iH, access;
            var e = new SDL.SDL_Event();

            while(!quit)
            {
                // event handling
                while(SDL.SDL_PollEvent(out e) > 0)
                {
                    if(e.type == SDL.SDL_EventType.SDL_QUIT)
                    {
                        quit = true;
                    }

                    if(e.type == SDL.SDL_EventType.SDL_KEYDOWN)
                    {
                        quit = true;
                    }

                    if(e.type == SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN)
                    {
                        quit = true;
                    }
                }

                // rendering
                SDL.SDL_RenderClear(renderer);

                for(var i = 0; i < xTiles * yTiles; i++)
                {
                    x = (i % xTiles) * TILE_SIZE;
                    y = (i / xTiles) * TILE_SIZE;

                    SdlDrawing.RenderTexture(background, renderer, x, y, TILE_SIZE, TILE_SIZE);
                }

                SDL.SDL_QueryTexture(image, out format, out access, out iW, out iH);
                x = WIDTH / 2 - iW / 2;
                y = HEIGHT / 2 - iH / 2;

                SdlDrawing.RenderTexture(image, renderer, x, y);

                SDL.SDL_RenderPresent(renderer);

            }

            ReleaseAndQuit(window, renderer, background, image);
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

            SDL_image.IMG_Quit();
            SDL.SDL_Quit();

            return false;
        }
    }
}