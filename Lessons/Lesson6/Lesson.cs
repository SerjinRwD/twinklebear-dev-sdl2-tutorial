namespace twinklebear_dev_sdl2_tutorial.Lessons.Lesson6
{
    using SDL2;
    using System;
    using twinklebear_dev_sdl2_tutorial.Helpers;

    public class Lesson : ILesson
    {
        const int WIDTH = 640;
        const int HEIGHT = 480;
        const int TILE_SIZE = 40;
        const string TITLE = "Lesson 6";

        public void Execute()
        {
            if(SDL.SDL_Init(SDL.SDL_INIT_VIDEO) != 0)
            {
                SdlLogger.Fatal(nameof(SDL.SDL_Init));
            }

            SDL_image.IMG_Init(SDL_image.IMG_InitFlags.IMG_INIT_PNG);

            if(SDL_ttf.TTF_Init() != 0)
            {
                SdlLogger.Fatal(nameof(SDL_ttf.TTF_Init));
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

            var font = SDL_ttf.TTF_OpenFont(Resources.GetFilePath("lesson6/long_pixel-7.ttf"), 16);

            if(font == IntPtr.Zero)
            {
                SdlLogger.Fatal(
                    nameof(Resources.LoadTextureFromImage),
                    () => ReleaseAndQuit(window, renderer, IntPtr.Zero, IntPtr.Zero));
            }

            var colorWhite = new SDL.SDL_Color();
            colorWhite.r = 255;
            colorWhite.g = 255;
            colorWhite.b = 255;

            var image = SdlDrawing.CreateTextureFromText(@"SDL2_TTF - крутота!", font, colorWhite, renderer);

            var quit = false;
            int x, y, iW = 100, iH = 100;

            x = WIDTH / 2 - iW / 2;
            y = HEIGHT / 2 - iH / 2;

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
                        switch(e.key.keysym.sym)
                        {
                            case SDL.SDL_Keycode.SDLK_ESCAPE:
                                quit = true;
                                break;
                        }
                    }
                }

                // rendering
                SDL.SDL_RenderClear(renderer);

                SdlDrawing.RenderTexture(image, renderer, x, y);

                SDL.SDL_RenderPresent(renderer);

            }
            
            ReleaseAndQuit(window, renderer, font, image);
        }

        private bool ReleaseAndQuit(IntPtr window, IntPtr renderer, IntPtr font, IntPtr image)
        {
            if(window != IntPtr.Zero)
            {
                SDL.SDL_DestroyWindow(window);
            }

            if(renderer != IntPtr.Zero)
            {
                SDL.SDL_DestroyRenderer(renderer);
            }

            if(font != IntPtr.Zero)
            {
                SDL_ttf.TTF_CloseFont(font);
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