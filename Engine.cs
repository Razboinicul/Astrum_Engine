using SDL2;
using System;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace Astrum;

public class AstrumWindow
{
    IntPtr window;
    IntPtr renderer;
    public bool running = true;
    public int camera_offset_x = 0;
    public int camera_offset_y = 0;

    /// <summary>
    /// Setup all of the SDL resources we'll need to display a window.
    /// </summary>
    public void Setup() 
    {
        // Initilizes SDL.
        if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO) < 0)
        {
            Console.WriteLine($"There was an issue initializing SDL. {SDL.SDL_GetError()}");
        }

        // Create a new window given a title, size, and passes it a flag indicating it should be shown.
        window = SDL.SDL_CreateWindow(
            "SDL .NET 6 Tutorial",
            SDL.SDL_WINDOWPOS_UNDEFINED, 
            SDL.SDL_WINDOWPOS_UNDEFINED, 
            640, 
            480, 
            SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);

        if (window == IntPtr.Zero)
        {
            Console.WriteLine($"There was an issue creating the window. {SDL.SDL_GetError()}");
        }

        // Creates a new SDL hardware renderer using the default graphics device with VSYNC enabled.
        renderer = SDL.SDL_CreateRenderer(
            window,
            -1,
            SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED |
            SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);

        if (renderer == IntPtr.Zero)
        {
            Console.WriteLine($"There was an issue creating the renderer. {SDL.SDL_GetError()}");
        }

        SDL.SDL_SetRenderDrawBlendMode(renderer, SDL.SDL_BlendMode.SDL_BLENDMODE_BLEND);
    }

    /// <summary>
    /// Checks to see if there are any events to be processed.
    /// </summary>
    public void PollEvents()
    {
        // Check to see if there are any events and continue to do so until the queue is empty.
        while (SDL.SDL_PollEvent(out SDL.SDL_Event e) == 1)
        {
            switch (e.type)
            {
                case SDL.SDL_EventType.SDL_QUIT:
                    running = false;
                    break;
                case SDL.SDL_EventType.SDL_KEYDOWN:
                    switch (e.key.keysym.sym)
                    {
                        case SDL.SDL_Keycode.SDLK_ESCAPE:
                            running = false;
                            break;
                        case SDL.SDL_Keycode.SDLK_LEFT:
                            camera_offset_x = camera_offset_x + 5;
                            break;
                        case SDL.SDL_Keycode.SDLK_RIGHT:
                            camera_offset_x = camera_offset_x - 5;
                            break;
                        case SDL.SDL_Keycode.SDLK_DOWN:
                            camera_offset_y = camera_offset_y - 5;
                            break;
                        case SDL.SDL_Keycode.SDLK_UP:
                            camera_offset_y = camera_offset_y + 5;
                            break;
                        default: break;
                    }
                    break;
            }
        }
    }

    /// <summary>
    /// Renders to the window.
    /// </summary>
    public void Render()
    {
        // Sets the color that the screen will be cleared with.
        SDL.SDL_SetRenderDrawColor(renderer, 135, 206, 235, 255);

        // Clears the current render surface.
        SDL.SDL_RenderClear(renderer);

        // Set the color to red before drawing our shape
        SDL.SDL_SetRenderDrawColor(renderer, 255, 0, 0, 255);

        // Draw a line from top left to bottom right
        //SDL.SDL_RenderDrawLine(renderer, 0+camera_offset_x, 0 + camera_offset_y, 640 + camera_offset_x, 480+camera_offset_y);

        // Draws a point at (20, 20) using the currently set color.
        //SDL.SDL_RenderDrawPoint(renderer, 20 + camera_offset_x, 20 + camera_offset_y);

        // Specify the coordinates for our rectangle we will be drawing.
        var rect = new SDL.SDL_Rect 
        { 
            x = 300 + camera_offset_x,
            y = 100 + camera_offset_y,
            w = 50,
            h = 50
        };

        // Draw a filled in rectangle.
        SDL.SDL_RenderFillRect(renderer, ref rect);

        // Switches out the currently presented render surface with the one we just did work on.
        SDL.SDL_RenderPresent(renderer);
    }

    /// <summary>
    /// Clean up the resources that were created.
    /// </summary>
    public void CleanUp()
    {
        SDL.SDL_DestroyRenderer(renderer);
        SDL.SDL_DestroyWindow(window);
        SDL.SDL_Quit();
    }
}