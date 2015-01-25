# `Harness.Elements`
## The Controls...
```xml
<!-- h is assumed to be clr-namespace=Harness.Elements;assembly=Harness.Elements-->
```
### `Color`
It's a Color with a Key....

### `Palette`
A list of keyed colors.
```xaml
  <Page.Resources>
    <h:Palette x:Key="Colors">
      <h:Color Key="TextColor" Value="Black"/>
      <h:Color Key="BackgroundColor" Value="White"/>
      <h:Color Key="ButtonTextColor" Value="Blue"/>
    </h:Palette>
  </Page.Resources>
```

### The `ColorScheme` Behavior
Adds the colors in the assigned Palette as dynamic resources. unless the palette is otherwise named, it will be added with the name "Scheme" and the resource names takes the form Palette/Key

```xml
  <Label Text="Text" TextColor="{x:DynamicResource Scheme/TextColor}">
    <Label.Behaviors>
      <h:ColorScheme>
        <StaticResource Key="Colors"/>
      </h:ColorScheme>
    </Label.Behaviors>
  </Label>
```
Using this it is possible to define entire schemes of colors for your application, and easily change them at runtime by setting the `ColorScheme`s `Palette` property.

### `Attached` and `Attaching` objects
Attached objects are objects that attach to other objects. Much like Behaviors, except Attached objects are general purpose. The palette above attaches to the object the behavior was applied to, so it can manipulate it's resource dictionary.
AttachingObjects are objects which fascilitate attaching one or more objects to another object. The ColorScheme behavior as well as the `AttachingBehavior<T>` are `IAttachingObjects` and object attachment is redirected to a specified target.  

## X
The X class contains attached properties used in developing Harness.Elements that you might valuable in building your own controls and objects. 

### X.KeyProperty
This is the BindableProperty used in Color for it's Key property. X.Key provides a uniform way for us to treat Keys. So a X.Key is an X.Key is an X.Key. (And it isn't the inaccessible x:Key directive)

### X.NameProperty
X.Name is very much akin to the x:Name directive, one 2 differences:
- X.Name is an attached property whose value is availble at run-time. (Not that that matters with x:Name)
- Objects named with X.Name are available as dynamic resources for binding and other purposes.

