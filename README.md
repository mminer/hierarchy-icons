# Hierarchy Icons

See at a glance what components are attached to game objects in your scene with
this editor extension for Unity. Icons beside each item in the Hierarchy pane
make it easy to see where your cameras are, which game objects are lights, and
which objects have an audio source attached. Think of it as Gizmos for the
editor.

![Screenshot](http://matthewminer.com/images/hierarchy-icons.png)


## Installing

Add the package to your project via
[UPM](https://docs.unity3d.com/Manual/upm-ui.html) using the Git URL
https://github.com/mminer/hierarchy-icons.git. You can also clone the repository
and point UPM to your local copy.


## Using

Icons for components should automatically appear in the Hierarchy.  To turn off
individual icons, navigate to the Hierarchy Icons pane in Unity's preferences.


## Compatibility

Unity 2018.3 or later.


## Adding or Updating Icons

The icons come from an icon font, with each letter mapped to a glyph. [IcoMoon](https://icomoon.io/app) provides an easy way to create one of these. Select icons for each component, click “Generate Font”, assign a character to each glyph, then download the font and replace *HierarchyIcons.ttf*.

The mapping from component type to characters is in *IconMapping.cs*. To add a new entry, add this line to the `componentIcons` dictionary:

    { typeof(MyScript), 'x' },


## Credit

The icons are from [WebHostingHub Glyphs](http://www.webhostinghub.com/glyphs/).
Licensed under the SIL Open Font License.
