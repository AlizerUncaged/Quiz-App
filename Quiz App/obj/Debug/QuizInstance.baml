   M S B A M L   `   `   `  ????C  ?Quiz_App, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:Quiz_AppQuiz_App  5          Quiz_App.QuizInstance   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      (localclr-namespace:Quiz_App     5      _ [MaterialDesignThemes.Wpf, Version=4.6.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      $??Stretch=?5      $??Stretch=?6,   .??5	      ? .+?5
   
   ??   ?? CornerRadius$  7??6   $??Center=?6"   $??Center=?6?   ???#FFFFFF._?5      ? $??Center=?6   $??Center=?60   .+?5      ? $??15q?6   .+?5      ?? .+?5      ?? ???#189F8B J? Opacity$
 0.80Q?69   $??0,0,0,15q?5   $   $??Center=?5   $   $
??Bold"?5   $   $	??300??66   $
??Wrap=?6E   $??16(?5   $   ??5   $   ??  ?? RelativeSource ??  ?? Mode$ FindAncestor=? ?? AncestorType   Quiz_App.QuizInstance"   ?? Path$ Quiz.QuestionSentence?5      ?? 5      ??5      ??  ?? $ FindAncestor=?"  $ Quiz.Answers???5      ?? 5   "   .R?5   &   ?? $??Center=?61   $??Stretch=?6L   5   '   5   #   ??5      ?? 5    "   .R?5!   &   ?? -   5        $	??7.5q?5!   -   $??Stretch=?6:   ???#FFFFFF ? Cursor$
 Hand??5"   B   $  7??5#   -    J? Effect 5$   *   3  -System.Windows.Media.Effects.DropShadowEffect  5%   .      Color$ Black??6?   	   Opacity$
	 0.15Q?6M   
   
BlurRadius$
 50Q?6\      	Direction$ 0Q?5&   ?      ShadowDepth$ 0Q?5'   ?   5(   /   ._?5*   *   ?? ???#42B4A5$
??Left=?6I   $??Center=?5+   4   $??10q?5,   4   $
 0.80Q?6@   $??16(?5-   4   $
??Bold"?5.   4   ??5/   4   ?? 50   +   51   '   52   #   54      55      56      58      59      ? 5:   
   .+?5;      ? -   5        $??QuizTime??5;      $??Bottom=?6*   $??15??6E   $??Stretch=?6Q   ???#FFFFFF???#FFE388$??0q?5=       ? Value$ 0Q?6.   5>      )  #MaterialDesignThemes.Wpf.DialogHost  5?      -   5        $??ResultDialogHost??5?   (      DialogContent 5@      ?? 5A      $  7??6   
???????._?5B      ? $??15q?6   .+?5C      ?? $??Center=?6)   $??Center=?6D   .+?5D   "   ?? -   5        $??
ResultText??5D   ,   $??0,0,0,10q?6>   $??Center=?6P   $
??Bold"?5E   ,   ???#179C87$??24(?6S   
Correct!5G   #   ?? 5H   "   -   5        $??PointsChanged??5H   ,   $??0,0,0,10q?6A   $??Center=?6S   ???#179C87$??16(?5I   A   
Correct!5K   #   ?? 5L   "   -   5        $
??Next??5L   >   6O   5M      5N      5O      5Q      5R      5S      5T      