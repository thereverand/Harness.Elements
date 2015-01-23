# `Harness.Elements`

### `Color`
It's a Color with a Key....

### `Palette`
A list of keyed colors.

### The `ColorScheme` Behavior
Adds the colors in the assigned Palette as dynamic resources. unless the palette is otherwise named, it will be added with the name "Scheme" and the resource names takes the form Palette/Key

```xml
  <Label Text="Text" TextColor="{x:DynamicResource Scheme/TextColor}">
    <Label.Behaviors>
      <h:ColorScheme>
        <h:Palette>
          <h:Color Key="TextColor" Value="Black"/>
        </h:Palette>
      </h:ColorScheme>
    </Label.Behaviors>
  </Label>
```

### `Attached` and `Attaching` objects
Attached objects are objects that attach to objects in a given context. The palette above attaches to the object the behavior was applied to, so it can manipulate it's resource dictionary.
AttachingObjects are objects which fascilitate attaching one or more objects to another object. The ColorScheme behavior as well as the AttachingBehavior<T> are IAtachingObjects.  

