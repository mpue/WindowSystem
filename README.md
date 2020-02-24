# MonoGame WindowSystem

MonoGame WindowSystem is a slim and versatile windows system for Monogame.
Currently it's at a very early development state.

![Example application](https://raw.githubusercontent.com/mpue/WindowSystem/master/WindowSystem/Screenshots/shot1.png)

## Dark theme

![Example application](https://raw.githubusercontent.com/mpue/WindowSystem/master/WindowSystem/Screenshots/shot2.png)


#  Features
  - Windows like desktop functionality with icons, windows and widgets
  - Desktop widgets
  - Menu bars
  
# Currently working widgets

  - Button (with image)
  - Checkbox
  - Panel
  - Clock
  - Scrollbar
  - Window
  - Label
  - Menubar
  
# To be done
  - completing widgets
  - persistence
  - documentation
  - coding, coding, coding...
  
# Important

The library is not production ready yet! So use it at your own risk and don't file bugs yet, since I'm still at the very beginning.
  
# How do I use it?

Basically you get a WindowManager instance with 
  
    WindowManager.CreateInstance(graphics, Content, spriteBatch);
  
Now you can hook up the WindowManager to your Update and Draw calls with

    WindowManager.GetInstance().Update(gameTime)
  
Depending on what you want to do, call

    WindowManager.GetInstance().Draw(gameTime)
  
before or after your game draw calls.

For further use have a look at the WindowDemo application.
