   M S B A M L   `   `   `  ????C  ?Quiz_App, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:Quiz_AppQuiz_App  5   	       Quiz_App.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:Quiz_App     5   	   _ [MaterialDesignThemes.Wpf, Version=4.6.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   -   5        ???#42B4A5  ? 
ResizeMode$  	CanResize=?5   /    ? Title$ 
MainWindow??5	   	   $	??480??6   $	??900??6)   '  !System.Windows.Shell.WindowChrome   WindowChrome 5        5   
      GlassFrameThickness$ -1q?6      CornerRadius$ 0??60      CaptionHeight$ 0Q?6A      UseAeroCaptionButtons .     ResizeBorderThickness$ 7q?5      .??5      ? .+?5   
   ? -   5        $??
ParentGrid??5       J? Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5      	   Color$	 Black??6#   
   Opacity$

 0.10Q?61      ShadowDepth$ 0Q?6@      
BlurRadius$ 50Q?6P   6b   5      ? 5   
   -   5        $??GameArea??5      .+?5      ?? ???#FFE288$	??Top=?6:   $
??Left=?6R   $??15q?6m    ?? Orientation$ 
Horizontal=?5      .+?5      ?? ??5      ??  ?? RelativeSource ??  ?? Mode$ FindAncestor=? ?? AncestorType   Quiz_App.MainWindow"   ?? Path$ Player.CurrentHealths? ?? Mode$ OneWay=???5      ?? 5      .R?5      ?? $ 
Horizontal=?6)   ???#EA4E4E$??Center=?5   )   5      ??5       ?? 5!      .R?5"      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Heart=?66   $??0,0,15,0q?6C   6W   5#      5%        5&      $??Center=?6*   $??0,0,5,0q?6E   $ Hexagram=?6V   6h   ?? 5'      -   5        $??PointsTextBlock??5'      ???#FFE288$??Center=?6H   $
??Bold"?6c   $??0,0,15,0q?5(      $??16(?5)      ?? 5*      -   5         ?Name$ CurrentPoints??5*      .?6/   10064   5+      5,      )  #MaterialDesignThemes.Wpf.DialogHost  5-      -   5        $??ResultDialogHost??5-   (      DialogContent 5.      ?? 5/       ?? CornerRadius$ 7??6   
???????._?50      ? $??15q?6   .+?51      ?? $??Center=?6)   $??Center=?6D   .+?52   "   ?? -   5        $??
ResultText??52   ,   $??0,0,0,10q?6>   $??Center=?6P   $
??Bold"?53   ,   ???#179C87$??24(?6S   
Quiz Done!55   #   ?? 56   "   -   5        $??PointsChanged??56   ,   $??0,0,0,10q?6A   $??Center=?6S   ???#179C87$??16(?57   A   Total Score59   #   ?? 5:   "   -	   5        $??OK??5:   <   6K   5;      5<      5=      5?      5@      ? 5A   
   .+?5B      ?? $??Right=?6    ? Cursor$
 Hand??65   $	??Top=?6C   $ 
Horizontal=?5C      $??7q?5D      .+?5E        -
   5        
?????? ???#0C2356$ Minimize=?5E   X   $??25??6h   $??25??5F   *   $??0,0,5,0q?66   5G        5H      -   5        
?????? ???#0C2356$ Close=?5H   X   $??25??6e   $??25??5I   *   5J      5K      5L      ? 5M   
   .+?5N      ?? -   5        $??Bottom=?5N   ,   $??Right=?6G   $??15q?6c   .??5O      ?? $ 
Horizontal=?6   .+?5P        $ 	ArrowLeft=?6.   $??0,0,5,0q?6?   6R   ?? 5Q      $
??Back??6    6.   5R      5S      5T      5U      5V      